using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.NullableAnnotation;
using static Microsoft.CodeAnalysis.SpecialType;
using static SolutionVersion;
using NamedArgs = System.Collections.Immutable.ImmutableArray<System.Collections.Generic.KeyValuePair<string, Microsoft.CodeAnalysis.TypedConstant>>;

namespace Puzzles.SourceGeneration;

/// <summary>
/// Represents a source generator type that runs multiple different usage of source output services on compiling code.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
	/// <inheritdoc/>
	public void Initialize(IncrementalGeneratorInitializationContext context)
		// Type implementation.
		=> context.RegisterSourceOutput(
			context.SyntaxProvider
				.ForAttributeWithMetadataName(
					"System.Diagnostics.CodeAnalysis.TypeImplAttribute",
					IsPartialTypePredicate,
					FileLocalHandler.Transform
				)
				.Collect(),
			FileLocalHandler.Output
		);


	/// <summary>
	/// Determine whether the specified type declaration syntax node contains a <see langword="partial"/> modifier.
	/// </summary>
	/// <typeparam name="TSyntaxNode">The type of the declaration syntax node.</typeparam>
	/// <param name="node">The node to be determined.</param>
	/// <param name="_"/>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool IsPartialTypePredicate<TSyntaxNode>(TSyntaxNode node, CancellationToken _) where TSyntaxNode : SyntaxNode
		=> node is BaseTypeDeclarationSyntax { Modifiers: var modifiers and not [] } && modifiers.Any(SyntaxKind.PartialKeyword);

	/// <summary>
	/// Determine whether the value is not <see langword="null"/>.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool NotNullPredicate<T>(T value) => value is not null;

	/// <summary>
	/// Try to get the internal value without nullability checking.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value with <c>?</c> token being annotated, but not <see langword="null"/> currently.</param>
	/// <param name="_"/>
	/// <returns>The value.</returns>
	public static T NotNullSelector<T>(T? value, CancellationToken _) where T : class => value!;
}

/// <summary>
/// The file-local type that generates modal source code on some commonly-used members.
/// </summary>
file static class FileLocalHandler
{
	private const string ValueTaskFullTypeName = "global::System.Threading.Tasks.ValueTask";

	private const string ParameterTargetAttributeTypeName = "System.Diagnostics.CodeAnalysis.ParameterTargetAttribute";

	private const string HashCodeMemberAttributeTypeName = "System.Diagnostics.CodeAnalysis.HashCodeMemberAttribute";

	private const string StringMemberAttributeTypeName = "System.Diagnostics.CodeAnalysis.StringMemberAttribute";

	private const string EquatableMemberAttributeTypeName = "System.Diagnostics.CodeAnalysis.EquatableMemberAttribute";

	private const string DisposableMemberAttributeTypeName = "System.Diagnostics.CodeAnalysis.DisposableMemberAttribute";

	private const string EquatableTypeName = "System.IEquatable`1";

	private const string FormattableTypeName = "System.IFormattable";

	private const string FormatProviderTypeName = "System.IFormatProvider";

	private const string EqualityOperatorsTypeName = "System.Numerics.IEqualityOperators`3";

	private const string DisposableTypeName = "System.IDisposable";

	private const string AsyncDisposableTypeName = "System.IAsyncDisposable";

	private const string IsLargeStructurePropertyName = "IsLargeStructure";

	private const string OtherModifiersOnEqualsPropertyName = "OtherModifiersOnEquals";

	private const string OtherModifiersOnToStringPropertyName = "OtherModifiersOnToString";

	private const string EmitThisCastToInterfacePropertyName = "EmitThisCastToInterface";

	private const string EqualsBehaviorPropertyName = "EqualsBehavior";

	private const string ToStringBehaviorPropertyName = "ToStringBehavior";

	private const string GetHashCodeBehaviorPropertyName = "GetHashCodeBehavior";

	private const string OperandNullabilityPreferPropertyName = "OperandNullabilityPrefer";

	private const string OtherModifiersOnEquatableEqualsPropertyName = "OtherModifiersOnEquatableEquals";

	private const string EquatableLargeStructModifierPropertyName = "EquatableLargeStructModifier";

	private const string OtherModifiersOnDisposableDisposePropertyName = "OtherModifiersOnDisposableDispose";

	private const string OtherModifiersOnAsyncDisposableDisposeAsyncPropertyName = "OtherModifiersOnAsyncDisposableDisposeAsync";

	private const string ExplicitlyImplsDisposablePropertyName = "ExplicitlyImplsDisposable";

	private const string ExplicitlyImplsAsyncDisposablePropertyName = "ExplicitlyImplsAsyncDisposable";


	private static readonly Func<GeneratorAttributeSyntaxContext, CancellationToken, string?>[] Methods = [
		ObjectEquals,
		ObjectGetHashCode,
		ObjectToString,
		EqualityOperators,
		ComparisonOperators,
		TrueAndFalseOperators,
		LogicalNotOperator,
		Equatable,
		DisposableAndAsyncDisposable
	];


	public static IEnumerable<string> Transform(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
		=>
		from method in Methods
		let source = method(gasc, cancellationToken)
		where source is not null
		select source;

	public static void Output(SourceProductionContext spc, ImmutableArray<IEnumerable<string>> value)
		=> spc.AddSource(
			"TypeImpl.g.cs",
			$"""
			{Banner.AutoGenerated}

			#nullable enable
			
			{string.Join("\r\n\r\n", from element in value from nested in element select nested)}
			"""
		);

	private static string? ObjectEquals(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					TypeKind: var kind and (TypeKind.Struct or TypeKind.Class),
					Name: var typeName,
					IsRecord: false, // Records cannot manually overrides 'Equals' method.
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					TypeParameters: var typeParameters,
					ContainingNamespace: var @namespace,
					ContainingType: null // Must be top-level type.
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Object_Equals))
		{
			return null;
		}

		var isLargeStructure = attribute.GetNamedArgument(IsLargeStructurePropertyName, false);
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var behavior = attribute.GetNamedArgument(EqualsBehaviorPropertyName, 0) switch
		{
			0 => (isRefStruct, kind) switch
			{
				(true, _) => EqualsBehavior.ReturnFalse,
				(_, TypeKind.Struct) => EqualsBehavior.IsCast,
				(_, TypeKind.Class) => EqualsBehavior.AsCast,
				_ => throw new InvalidOperationException("Invalid state.")
			},
			1 => EqualsBehavior.Throw,
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiers = attribute.GetNamedArgument<string>(OtherModifiersOnEqualsPropertyName) switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var typeKindString = kind switch
		{
			TypeKind.Class => "class",
			TypeKind.Struct => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		var inKeyword = isLargeStructure ? "in " : string.Empty;
		var expressionString = behavior switch
		{
			EqualsBehavior.ReturnFalse => "false",
			EqualsBehavior.IsCast => $"obj is {fullTypeNameString} comparer && Equals({inKeyword}comparer)",
			EqualsBehavior.AsCast => $"Equals(obj as {fullTypeNameString})",
			EqualsBehavior.Throw => """throw new global::System.NotSupportedException("This method is not supported or disallowed by author.")""",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var attributesMarked = isRefStruct
			? behavior == EqualsBehavior.ReturnFalse
				? """
				[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				"""
				: """
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				"""
			: """
			[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			""";
		var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
		var isDeprecated = attributesMarked.Contains("ObsoleteAttribute");
		var suppress0809 = isDeprecated ? "#pragma warning disable CS0809\r\n\t" : "\t";
		var enable0809 = isDeprecated ? "#pragma warning restore CS0809\r\n\t" : string.Empty;
		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_Equals.g.cs"
			{{suppress0809}}partial {{typeKindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="object.Equals(object?)"/>
					{{attributesMarked}}
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public {{otherModifiersString}}override {{readOnlyModifier}}bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] object? obj)
						=> {{expressionString}};
				}
			#line default
			{{enable0809}}}
			""";
	}

	private static string? ObjectGetHashCode(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					TypeParameters: var typeParameters,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct),
					IsRecord: var isRecord,
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					ContainingType: null
				} type,
				TargetNode: TypeDeclarationSyntax { ParameterList: var parameterList }
					and (RecordDeclarationSyntax or ClassDeclarationSyntax or StructDeclarationSyntax),
				SemanticModel: { Compilation: var compilation } semanticModel
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Object_GetHashCode))
		{
			return null;
		}

		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeParametersString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeParametersString}";

		var paramTargetAttributeTypeNameSymbol = compilation.GetTypeByMetadataName(ParameterTargetAttributeTypeName);
		if (paramTargetAttributeTypeNameSymbol is null)
		{
			return null;
		}

		var hashCodeMemberAttributeSymbol = compilation.GetTypeByMetadataName(HashCodeMemberAttributeTypeName);
		if (hashCodeMemberAttributeSymbol is null)
		{
			return null;
		}

		var referencedMembers = PrimaryConstructor.MapMemberNames(
			type,
			semanticModel,
			parameterList,
			paramTargetAttributeTypeNameSymbol,
			a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, hashCodeMemberAttributeSymbol),
			static symbol => symbol switch
			{
				IFieldSymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IFieldSymbol { Type.TypeKind: TypeKind.Enum } => false,
				IPropertySymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IPropertySymbol { Type.TypeKind: TypeKind.Enum } => false,
				IParameterSymbol { Type.SpecialType: System_Byte or System_SByte or System_Int16 or System_UInt16 or System_Int32 } => true,
				IParameterSymbol { Type.TypeKind: TypeKind.Enum } => false,
				_ => default(bool?)
			},
			cancellationToken
		);

		var behavior = (isRefStruct, attribute) switch
		{
			(true, _) => GetHashCodeBehavior.Throw,
			_ => attribute.GetNamedArgument<int>(GetHashCodeBehaviorPropertyName) switch
			{
				0 => referencedMembers switch
				{
					[] => GetHashCodeBehavior.ReturnNegativeOne,
					[(_, true)] => GetHashCodeBehavior.Direct,
					[(_, false)] => GetHashCodeBehavior.EnumExplicitCast,
					{ Length: > 8 } => GetHashCodeBehavior.HashCodeAdd,
					_ => GetHashCodeBehavior.Specified
				},
				1 => GetHashCodeBehavior.Throw,
				_ => throw new InvalidOperationException("Invalid state.")
			}
		};
		var kindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(true, TypeKind.Struct) => "record struct",
			(_, TypeKind.Class) => "class",
			(_, TypeKind.Struct) => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiers = attribute.GetNamedArgument<string>("OtherModifiersOnGetHashCode") switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		var codeBlock = behavior switch
		{
			GetHashCodeBehavior.ReturnNegativeOne => @"	=> -1;",
			GetHashCodeBehavior.Direct => $@"	=> {referencedMembers[0].Name};",
			GetHashCodeBehavior.EnumExplicitCast => $@"	=> (int){referencedMembers[0].Name};",
			GetHashCodeBehavior.Specified
				=> $@"	=> global::System.HashCode.Combine({string.Join(", ", from pair in referencedMembers select pair.Name)});",
			GetHashCodeBehavior.Throw
				=> @"	=> throw new global::System.NotSupportedException(""This method is not supported or disallowed by author."");",
			GetHashCodeBehavior.HashCodeAdd
				=> $$"""
					{
						var hashCode = new global::System.HashCode();
						{{string.Join("\r\n\r\n", from member in referencedMembers select $"hashCode.Add({member.Name});")}}
						return hashCode.ToHashCode();
					}
				""",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var attributesMarked = (isRefStruct, behavior) switch
		{
			(true, not GetHashCodeBehavior.ReturnNegativeOne) or (_, GetHashCodeBehavior.Throw)
				=> """
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				""",
			(true, _)
				=> """
				[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				""",
			_
				=> """
				[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				"""
		};
		var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
		var isDeprecated = attributesMarked.Contains("ObsoleteAttribute");
		var suppress0809 = isDeprecated
			? "#pragma warning disable CS0809\r\n\t"
			: "\t";
		var enable0809 = isDeprecated
			? "#pragma warning restore CS0809\r\n\t"
			: "\t";
		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_GetHashCode.g.cs"
			{{suppress0809}}partial {{kindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="object.GetHashCode"/>
					{{attributesMarked}}
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public {{otherModifiersString}}override {{readOnlyModifier}}int GetHashCode()
					{{codeBlock}}
			#line default
			{{enable0809}}}
			}
			""";
	}

	private static string? ObjectToString(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					TypeParameters: var typeParameters,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct),
					IsRecord: var isRecord,
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					ContainingType: null
				} type,
				TargetNode: TypeDeclarationSyntax { ParameterList: var parameterList }
					and (RecordDeclarationSyntax or ClassDeclarationSyntax or StructDeclarationSyntax),
				SemanticModel: { Compilation: var compilation } semanticModel
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Object_ToString))
		{
			return null;
		}

		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var needCastingToInterface = attribute.GetNamedArgument(EmitThisCastToInterfacePropertyName, false);
		var typeParametersString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeParametersString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		if (compilation.GetTypeByMetadataName(FormattableTypeName) is not { } formattableTypeSymbol)
		{
			return null;
		}

		var paramTargetAttributeTypeNameSymbol = compilation.GetTypeByMetadataName(ParameterTargetAttributeTypeName);
		if (paramTargetAttributeTypeNameSymbol is null)
		{
			return null;
		}

		var stringMemberAttributeSymbol = compilation.GetTypeByMetadataName(StringMemberAttributeTypeName);
		if (stringMemberAttributeSymbol is null)
		{
			return null;
		}

		var formatProviderSymbol = compilation.GetTypeByMetadataName(FormatProviderTypeName);
		if (formatProviderSymbol is null)
		{
			return null;
		}

		var referencedMembers = PrimaryConstructor.MapMemberNames(
			type,
			semanticModel,
			parameterList,
			paramTargetAttributeTypeNameSymbol,
			a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, stringMemberAttributeSymbol),
			symbol => (string?)symbol.GetAttributes().First(stringMemberAttributeMatcher).ConstructorArguments[0].Value ?? symbol.Name,
			cancellationToken
		);

		var behavior = attribute.GetNamedArgument<int>(ToStringBehaviorPropertyName) switch
		{
			0 => (isRefStruct, referencedMembers) switch
			{
				(true, _) => ToStringBehavior.Throw,
				_ when hasImpledFormattable(type) => ToStringBehavior.CallOverload,
				(_, []) => ToStringBehavior.RecordLike,
				(_, { Length: 1 }) => ToStringBehavior.Specified,
				_ => ToStringBehavior.RecordLike
			},
			1 => ToStringBehavior.CallOverload,
			2 when referencedMembers.Length == 1 => ToStringBehavior.Specified,
			3 when referencedMembers.Length != 0 => ToStringBehavior.RecordLike,
			4 => ToStringBehavior.Throw,
			_ => ToStringBehavior.ReturnTypeName
		};
		var kindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(true, TypeKind.Struct) => "record struct",
			(_, TypeKind.Class) => "class",
			(_, TypeKind.Struct) => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiers = attribute.GetNamedArgument<string>(OtherModifiersOnToStringPropertyName) switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		var typeMethods = type.GetMembers().OfType<IMethodSymbol>().ToArray();
		var expression = behavior switch
		{
			ToStringBehavior.CallOverload when typeMethods.Any(
				static method => method is
				{
					Name: "ToString",
					IsImplicitlyDeclared: false,
					TypeParameters: [],
					Parameters: [{ Type.SpecialType: System_String, NullableAnnotation: Annotated }],
					ReturnType.SpecialType: System_String
				}
			) => "ToString(default(string))",
			ToStringBehavior.CallOverload when typeMethods.Any(
				method => method is
				{
					Name: "ToString",
					IsImplicitlyDeclared: false,
					TypeParameters: [],
					Parameters: [{ Type: var t, NullableAnnotation: Annotated }],
					ReturnType.SpecialType: System_String
				} && SymbolEqualityComparer.Default.Equals(t, formatProviderSymbol)
			) => "ToString(default(global::System.IFormatProvider))",
			ToStringBehavior.CallOverload => needCastingToInterface switch
			{
				true => "((global::System.IFormattable)this).ToString(null, null)",
				_ => "ToString(default(string), default(global::System.IFormatProvider))"
			},
			ToStringBehavior.ReturnTypeName => fullTypeNameString,
			ToStringBehavior.Specified => referencedMembers[0].Name,
			ToStringBehavior.Throw => """throw new global::System.NotSupportedException("This method is not supported or disallowed by author.")""",
			ToStringBehavior.RecordLike
				=> $$$"""
				$"{{{typeName}}} {{ {{{string.Join(", ", f(referencedMembers))}}} }}"
				""",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var attributesMarked = isRefStruct && behavior is ToStringBehavior.Throw or ToStringBehavior.ReturnTypeName
			? behavior == ToStringBehavior.ReturnTypeName
				? """
				[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				"""
				: """
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]
						[global::System.ObsoleteAttribute("Calling this method is unexpected because author disallow you call this method on purpose.", true)]
				"""
			: """
			[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			""";
		var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
		var isDeprecated = attributesMarked.Contains("ObsoleteAttribute");
		var suppress0809 = isDeprecated ? "#pragma warning disable CS0809\r\n\t" : "\t";
		var enable0809 = isDeprecated ? "#pragma warning restore CS0809\r\n\t" : "\t";
		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_ToString.g.cs"
			{{suppress0809}}partial {{kindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="object.ToString"/>
					{{attributesMarked}}
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public {{otherModifiersString}}override {{readOnlyModifier}}string ToString()
						=> {{expression}};
			#line default
			{{enable0809}}}
			}
			""";


		static IEnumerable<string> f((string Name, string ExtraData)[] referencedMembers)
			=>
			from referencedMember in referencedMembers
			let displayName = referencedMember.ExtraData
			let name = referencedMember.Name
			select $$"""{{displayName ?? $$"""{nameof({{name}})}"""}} = {{{name}}}""";

		bool hasImpledFormattable(INamedTypeSymbol type)
			=> type.AllInterfaces.Contains(formattableTypeSymbol, SymbolEqualityComparer.Default);

		bool stringMemberAttributeMatcher(AttributeData a)
			=> SymbolEqualityComparer.Default.Equals(a.AttributeClass, stringMemberAttributeSymbol);
	}

	private static string? EqualityOperators(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					ContainingType: null,
					IsRecord: var isRecord,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct or TypeKind.Interface),
					TypeParameters: var typeParameters,
					IsRefLikeType: var isRefStruct
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.EqualityOperators))
		{
			return null;
		}

		if (kind == TypeKind.Interface && (
			typeParameters is not [{ ConstraintTypes: var constraintTypes }, ..]
			|| !constraintTypes.Contains(type, SymbolEqualityComparer.Default)
		))
		{
			// If the type is an interface, we should check for whether its first type parameter is a self type parameter,
			// which means it should implement its containing interface type.
			return null;
		}

		var isLargeStructure = attribute.GetNamedArgument<bool>(IsLargeStructurePropertyName);
		var behavior = (isRecord, kind, isLargeStructure) switch
		{
			(true, TypeKind.Class, _) => EqualityOperatorsBehavior.DoNothing,
			(_, TypeKind.Class, _) => EqualityOperatorsBehavior.Default,
			(true, TypeKind.Struct, true) => EqualityOperatorsBehavior.WithScopedInButDeprecated,
			(true, TypeKind.Struct, _) => EqualityOperatorsBehavior.DefaultButDeprecated,
			(_, TypeKind.Struct, true) => EqualityOperatorsBehavior.WithScopedIn,
			(_, TypeKind.Struct, _) => EqualityOperatorsBehavior.Default,
			_ => throw new InvalidOperationException("Invalid state.")
		};
		if (behavior == EqualityOperatorsBehavior.DoNothing)
		{
			return null;
		}

		var typeKindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(_, TypeKind.Class) => "class",
			(true, TypeKind.Struct) => "record struct",
			(_, TypeKind.Struct) => "struct",
			(_, TypeKind.Interface) => "interface",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		const string nullableToken = "?";
		var nullabilityToken = (attribute.GetNamedArgument<int>(OperandNullabilityPreferPropertyName), kind) switch
		{
			(0, TypeKind.Class) or (2, _) => nullableToken,
			(0, TypeKind.Struct) or (1, _) => string.Empty,
			(0, TypeKind.Interface) => typeParameters[0] switch
			{
				{ HasNotNullConstraint: true } => nullableToken, // Unknown T.
				{ HasUnmanagedTypeConstraint: true } or { HasValueTypeConstraint: true } => string.Empty,
				{ HasReferenceTypeConstraint: true } => nullableToken,
				{ ReferenceTypeConstraintNullableAnnotation: Annotated } => nullableToken, // Reference type inferred.
				{ ConstraintNullableAnnotations: var annotations } when annotations.Contains(Annotated) => nullableToken, // Reference type inferred.
				_ => string.Empty
			},
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var attributesMarked = behavior switch
		{
			EqualityOperatorsBehavior.WithScopedInButDeprecated or EqualityOperatorsBehavior.DefaultButDeprecated
				=> """
				[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.ObsoleteAttribute("This operator is not recommended to be defined in a record struct, because it'll be auto-generated a pair of equality operators by compiler, without any modifiers modified two parameters.", false)]
				""",
			_
				=> """
				[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				"""
		};
		var inKeyword = isLargeStructure ? "in " : string.Empty;
		var (i1, i2) = nullabilityToken switch
		{
			nullableToken => (
				$$"""(left, right) switch { (null, null) => true, ({ } a, { } b) => a.Equals({{inKeyword}}b), _ => false }""",
				"!(left == right)"
			),
			_ => ($"left.Equals({inKeyword}right)", $"!(left == right)")
		};

		var explicitImplementation = string.Empty;
		var equalityOperatorsType = compilation.GetTypeByMetadataName("System.Numerics.IEqualityOperators`3")!
			.Construct(type, type, compilation.GetSpecialType(System_Boolean));
		if (behavior is EqualityOperatorsBehavior.WithScopedIn or EqualityOperatorsBehavior.WithScopedInButDeprecated
			&& type.AllInterfaces.Contains(equalityOperatorsType, SymbolEqualityComparer.Default))
		{
			explicitImplementation =
				$$"""
				/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IEqualityOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator ==({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left == right;

						/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IEqualityOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator !=({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left != right;
				""";
		}

		var operatorDeclaration = behavior switch
		{
			EqualityOperatorsBehavior.Default or EqualityOperatorsBehavior.DefaultButDeprecated
				=> $$"""
				/// <inheritdoc cref="global::System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Equality(TSelf, TOther)"/>
						{{attributesMarked}}
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator ==({{fullTypeNameString}}{{nullabilityToken}} left, {{fullTypeNameString}}{{nullabilityToken}} right)
							=> {{i1}};

						/// <inheritdoc cref="global::System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Inequality(TSelf, TOther)"/>
						{{attributesMarked}}
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator !=({{fullTypeNameString}}{{nullabilityToken}} left, {{fullTypeNameString}}{{nullabilityToken}} right)
							=> {{i2}};
				""",
			EqualityOperatorsBehavior.WithScopedIn or EqualityOperatorsBehavior.WithScopedInButDeprecated
				=> $$"""
				/// <inheritdoc cref="global::System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Equality(TSelf, TOther)"/>
						{{attributesMarked}}
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator ==(in {{fullTypeNameString}}{{nullabilityToken}} left, in {{fullTypeNameString}}{{nullabilityToken}} right)
							=> {{i1}};

						/// <inheritdoc cref="global::System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Inequality(TSelf, TOther)"/>
						{{attributesMarked}}
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator !=(in {{fullTypeNameString}}{{nullabilityToken}} left, in {{fullTypeNameString}}{{nullabilityToken}} right)
							=> {{i2}};

						{{explicitImplementation}}
				""",
			_ => null
		};
		if (operatorDeclaration is null)
		{
			return null;
		}

		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_EqualityOperators.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					{{operatorDeclaration}}
				}
			#line default
			}
			""";
	}

	private static string? ComparisonOperators(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					ContainingType: null,
					IsRecord: var isRecord,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct or TypeKind.Interface),
					TypeParameters: var typeParameters,
					IsRefLikeType: var isRefStruct
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.ComparisonOperators))
		{
			return null;
		}

		var isLargeStructure = attribute.GetNamedArgument<bool>(IsLargeStructurePropertyName);
		var behavior = (isRecord, kind, isLargeStructure) switch
		{
			(true, TypeKind.Class, _) => ComparisonOperatorsBehavior.DoNothing,
			(_, TypeKind.Class, _) => ComparisonOperatorsBehavior.Default,
			(true, TypeKind.Struct, true) => ComparisonOperatorsBehavior.WithScopedInButDeprecated,
			(true, TypeKind.Struct, _) => ComparisonOperatorsBehavior.DefaultButDeprecated,
			(_, TypeKind.Struct, true) => ComparisonOperatorsBehavior.WithScopedIn,
			(_, TypeKind.Struct, _) => ComparisonOperatorsBehavior.Default,
			_ => throw new InvalidOperationException("Invalid state.")
		};
		if (behavior == ComparisonOperatorsBehavior.DoNothing)
		{
			return null;
		}

		var typeKindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(_, TypeKind.Class) => "class",
			(true, TypeKind.Struct) => "record struct",
			(_, TypeKind.Struct) => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var inKeyword = isLargeStructure ? "in " : string.Empty;
		var (i1, i2, i3, i4) = (
			$"left.CompareTo({inKeyword}right) > 0",
			$"left.CompareTo({inKeyword}right) < 0",
			$"left.CompareTo({inKeyword}right) >= 0",
			$"left.CompareTo({inKeyword}right) <= 0"
		);

		var explicitImplementation = string.Empty;
		var equalityOperatorsType = compilation.GetTypeByMetadataName("System.Numerics.IComparisonOperators`3")!
			.Construct(type, type, compilation.GetSpecialType(System_Boolean));
		if (behavior is ComparisonOperatorsBehavior.WithScopedIn or ComparisonOperatorsBehavior.WithScopedInButDeprecated
			&& type.AllInterfaces.Contains(equalityOperatorsType, SymbolEqualityComparer.Default))
		{
			explicitImplementation =
				$$"""
				/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IComparisonOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator >({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left > right;

						/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IComparisonOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator <({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left < right;

						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IComparisonOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator >=({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left >= right;

						/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.Numerics.IComparisonOperators<{{fullTypeNameString}}, {{fullTypeNameString}}, bool>.operator <=({{fullTypeNameString}} left, {{fullTypeNameString}} right) => left <= right;
				""";
		}

		var operatorDeclaration = behavior switch
		{
			ComparisonOperatorsBehavior.Default or ComparisonOperatorsBehavior.DefaultButDeprecated
				=> $$"""
				/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator >({{fullTypeNameString}} left, {{fullTypeNameString}} right)
							=> {{i1}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator <({{fullTypeNameString}} left, {{fullTypeNameString}} right)
							=> {{i2}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator >=({{fullTypeNameString}} left, {{fullTypeNameString}} right)
							=> {{i3}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator <=({{fullTypeNameString}} left, {{fullTypeNameString}} right)
							=> {{i4}};
				""",
			ComparisonOperatorsBehavior.WithScopedIn or ComparisonOperatorsBehavior.WithScopedInButDeprecated
				=> $$"""
				/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator >(in {{fullTypeNameString}} left, in {{fullTypeNameString}} right)
							=> {{i1}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator <(in {{fullTypeNameString}} left, in {{fullTypeNameString}} right)
							=> {{i2}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator >=(in {{fullTypeNameString}} left, in {{fullTypeNameString}} right)
							=> {{i3}};

						/// <inheritdoc cref="global::System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)"/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						public static bool operator <=(in {{fullTypeNameString}} left, in {{fullTypeNameString}} right)
							=> {{i4}};

						{{explicitImplementation}}
				""",
			_ => null
		};
		if (operatorDeclaration is null)
		{
			return null;
		}

		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_ComparisonOperators.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					{{operatorDeclaration}}
				}
			#line default
			}
			""";
	}

	private static string? TrueAndFalseOperators(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					ContainingType: null,
					IsRecord: var isRecord,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct),
					TypeParameters: var typeParameters
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.TrueAndFalseOperators))
		{
			return null;
		}

		var propertySymbols = type.GetMembers().OfType<IPropertySymbol>();
		var mode = propertySymbols.Any(static m => propertyPredicate(m, nameof(LengthOrCountPropertyKind.Length)))
			? LengthOrCountPropertyKind.Length
			: propertySymbols.Any(static m => propertyPredicate(m, nameof(LengthOrCountPropertyKind.Count)))
				? LengthOrCountPropertyKind.Count
				: LengthOrCountPropertyKind.Unknown;
		if (mode == LengthOrCountPropertyKind.Unknown)
		{
			return null;
		}

		var modeString = mode.ToString();
		var isLargeStructure = attribute.GetNamedArgument<bool>(IsLargeStructurePropertyName);
		var modifierString = isLargeStructure ? "in " : string.Empty;
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var typeKindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(_, TypeKind.Class) => "class",
			(true, TypeKind.Struct) => "record struct",
			_ => "struct"
		};

		var logicalOperatorsType = compilation.GetTypeByMetadataName("System.ILogicalOperators`1")!.Construct(type);
		var explicitImplementation = string.Empty;
		if (type.AllInterfaces.Contains(logicalOperatorsType, SymbolEqualityComparer.Default))
		{
			explicitImplementation =
				$$"""
				/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.ILogicalOperators<{{fullTypeNameString}}>.operator true({{fullTypeNameString}} value) => value.{{modeString}} != 0;

						/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.ILogicalOperators<{{fullTypeNameString}}>.operator false({{fullTypeNameString}} value) => value.{{modeString}} == 0;
				""";
		}

		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_TrueAndFalseOperators.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="global::System.ILogicalOperators{TSelf}.op_True(TSelf)"/>
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
					public static bool operator true({{modifierString}}{{fullTypeNameString}} value)
						=> value.{{modeString}} != 0;

					/// <inheritdoc cref="global::System.ILogicalOperators{TSelf}.op_False(TSelf)"/>
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
					public static bool operator false({{modifierString}}{{fullTypeNameString}} value)
						=> value.{{modeString}} == 0;

					{{explicitImplementation}}
				}
			#line default
			}
			""";


		static bool propertyPredicate(IPropertySymbol m, string propertyName)
			=> m is { Name: var p, Type.SpecialType: System_Int32, IsIndexer: false } && p == propertyName;
	}

	private static string? LogicalNotOperator(GeneratorAttributeSyntaxContext gasc, CancellationToken _)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					Name: var typeName,
					ContainingNamespace: var @namespace,
					ContainingType: null,
					IsRecord: var isRecord,
					TypeKind: var kind and (TypeKind.Class or TypeKind.Struct),
					TypeParameters: var typeParameters
				} type,
				SemanticModel.Compilation: var compilation
			})
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.LogicalNotOperator))
		{
			return null;
		}

		var propertySymbols = type.GetMembers().OfType<IPropertySymbol>();
		var mode = propertySymbols.Any(static m => propertyPredicate(m, nameof(LengthOrCountPropertyKind.Length)))
			? LengthOrCountPropertyKind.Length
			: propertySymbols.Any(static m => propertyPredicate(m, nameof(LengthOrCountPropertyKind.Count)))
				? LengthOrCountPropertyKind.Count
				: LengthOrCountPropertyKind.Unknown;
		if (mode == LengthOrCountPropertyKind.Unknown)
		{
			return null;
		}

		var modeString = mode.ToString();
		var isLargeStructure = attribute.GetNamedArgument<bool>(IsLargeStructurePropertyName);
		var modifierString = isLargeStructure ? "in " : string.Empty;
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var typeKindString = (isRecord, kind) switch
		{
			(true, TypeKind.Class) => "record",
			(_, TypeKind.Class) => "class",
			(true, TypeKind.Struct) => "record struct",
			_ => "struct"
		};

		var logicalOperatorsType = compilation.GetTypeByMetadataName("System.ILogicalOperators`1")!.Construct(type);
		var explicitImplementation = string.Empty;
		if (type.AllInterfaces.Contains(logicalOperatorsType, SymbolEqualityComparer.Default))
		{
			explicitImplementation =
				$$"""
				/// <inheritdoc/>
						[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						static bool global::System.ILogicalOperators<{{fullTypeNameString}}>.operator !({{fullTypeNameString}} value) => value.{{modeString}} == 0;
				""";
		}

		return $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_LogicalNotOperator.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="global::System.ILogicalOperators{TSelf}.op_LogicalNot(TSelf)"/>
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
					public static bool operator !({{modifierString}}{{fullTypeNameString}} value)
						=> value.{{modeString}} == 0;

					{{explicitImplementation}}
				}
			#line default
			}
			""";


		static bool propertyPredicate(IPropertySymbol m, string propertyName)
			=> m is { Name: var p, Type.SpecialType: System_Int32, IsIndexer: false } && p == propertyName;
	}

	private static string? Equatable(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					TypeKind: var kind and (TypeKind.Struct or TypeKind.Class),
					Name: var typeName,
					IsRecord: var isRecord,
					IsReadOnly: var isReadOnly,
					IsRefLikeType: var isRefStruct,
					TypeParameters: var typeParameters,
					ContainingNamespace: var @namespace,
					ContainingType: null, // Must be top-level type.
					AllInterfaces: var allInterfaces
				} type,
				TargetNode: TypeDeclarationSyntax { ParameterList: var parameterList }
					and (RecordDeclarationSyntax or ClassDeclarationSyntax or StructDeclarationSyntax),
				SemanticModel: { Compilation: var compilation } semanticModel
			})
		{
			return null;
		}

		var paramTargetAttributeTypeNameSymbol = compilation.GetTypeByMetadataName(ParameterTargetAttributeTypeName);
		if (paramTargetAttributeTypeNameSymbol is null)
		{
			return null;
		}

		var equatableMemberAttributeSymbol = compilation.GetTypeByMetadataName(EquatableMemberAttributeTypeName);
		if (equatableMemberAttributeSymbol is null)
		{
			return null;
		}

		if (!((TypeImplFlag)ctorArg).HasFlag(TypeImplFlag.Equatable))
		{
			return null;
		}

		var spanType = compilation.GetTypeByMetadataName("System.Span`1")!.ConstructUnboundGenericType();
		var readOnlySpanType = compilation.GetTypeByMetadataName("System.ReadOnlySpan`1")!.ConstructUnboundGenericType();

		var unboundEquatableInterfaceType = compilation.GetTypeByMetadataName(EquatableTypeName)!;
		var equalityOperatorsInterfaceType = compilation.GetTypeByMetadataName(EqualityOperatorsTypeName)!;
		var equatableInterfaceType = unboundEquatableInterfaceType.Construct(type);
		var inlineArrayAttributeType = compilation.GetTypeByMetadataName("System.Runtime.CompilerServices.InlineArrayAttribute");
		var baseType = default(INamedTypeSymbol);
		if (!allInterfaces.Any(a => SymbolEqualityComparer.Default.Equals(a, equatableInterfaceType)))
		{
			if (kind == TypeKind.Class)
			{
				for (var currentType = type.BaseType; currentType is not null; currentType = currentType.BaseType)
				{
					var equatableInterfaceTypeCurrent = unboundEquatableInterfaceType.Construct(currentType);
					if (currentType.AllInterfaces.Any(a => SymbolEqualityComparer.Default.Equals(a, equatableInterfaceTypeCurrent)))
					{
						baseType = currentType;
						break;
					}
				}
			}
			if (baseType is null)
			{
				return null;
			}
		}

		var isLargeStructure = attribute.GetNamedArgument(IsLargeStructurePropertyName, false);
		var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
		var otherModifiers = attribute.GetNamedArgument<string>(OtherModifiersOnEquatableEqualsPropertyName) switch
		{
			{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
			_ => []
		};
		var typeArgumentsString = typeParameters is []
			? string.Empty
			: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
		var typeNameString = $"{typeName}{typeArgumentsString}";
		var fullTypeNameString = $"global::{namespaceString}.{typeNameString}";
		var typeKindString = (kind, isRecord) switch
		{
			(TypeKind.Class, true) => "record",
			(TypeKind.Class, _) => "class",
			(TypeKind.Struct, true) => "record struct",
			(TypeKind.Struct, _) => "struct",
			_ => throw new InvalidOperationException("Invalid state.")
		};
		var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
		var inKeyword = isLargeStructure ? "in " : string.Empty;

		var referencedMembers = PrimaryConstructor.MapMemberNames(
			type,
			semanticModel,
			parameterList,
			paramTargetAttributeTypeNameSymbol,
			a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, equatableMemberAttributeSymbol),
			symbol =>
			{
				return symbol switch
				{
					IFieldSymbol { Name: var name, Type: var fieldType } => getComparisonString(fieldType, name),
					IPropertySymbol { Name: var name, Type: var propertyType } => getComparisonString(propertyType, name),
					_ => null
				};


				string getComparisonString(ITypeSymbol type, string name)
				{
					var p = equalityOperatorsInterfaceType.Construct(type, type, compilation.GetSpecialType(System_Boolean));
					var q = unboundEquatableInterfaceType.Construct(type);
					var r = compilation.GetTypeByMetadataName("System.Collections.BitArray");
					switch (type)
					{
						case { SpecialType: System_Object }:
						{
							return $"ReferenceEquals({name}, other.{name})";
						}
						case { TypeKind: TypeKind.Enum }:
						case { SpecialType: > System_Enum and <= System_UIntPtr }:
						case { TypeKind: TypeKind.Pointer or TypeKind.FunctionPointer }:
						case { AllInterfaces: var allInterfaces } when implsEqualityOperators(allInterfaces):
						{
							return $"{name} == other.{name}";
						}
						case { AllInterfaces: var allInterfaces } when implsEquatable(allInterfaces):
						{
							return $"{name}.Equals(other.{name})";
						}
						case var _ when isInlineArray():
						{
							return $"{name}[..].SequenceEqual(other.{name}[..])";
						}
						case var _ when isReadOnlySpanOrSpan() || isBitArray():
						{
							return $"{name}.SequenceEqual(other.{name})";
						}
						default:
						{
							var fullName = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
							return $"global::System.Collections.Generic.EqualityComparer<{fullName}>.Default.Equals({name}, other.{name})";
						}
					}


					bool implsEqualityOperators(ImmutableArray<INamedTypeSymbol> allInterfaces)
						=> allInterfaces.Any(a => SymbolEqualityComparer.Default.Equals(a, p));

					bool implsEquatable(ImmutableArray<INamedTypeSymbol> allInterfaces)
						=> allInterfaces.Any(a => SymbolEqualityComparer.Default.Equals(a, q));

					bool isInlineArray() => type.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, inlineArrayAttributeType));

					bool isBitArray() => SymbolEqualityComparer.Default.Equals(type, r);

					bool isReadOnlySpanOrSpan()
					{
						if (type is not INamedTypeSymbol { IsGenericType: true } s)
						{
							return false;
						}

						var casted = s.ConstructUnboundGenericType();
						return SymbolEqualityComparer.Default.Equals(casted, readOnlySpanType)
							|| SymbolEqualityComparer.Default.Equals(casted, spanType);
					}
				}
			},
			cancellationToken
		);

		var nullableToken = kind == TypeKind.Struct ? string.Empty : "?";
		var otherIsNullCheckString = string.IsNullOrEmpty(nullableToken) ? string.Empty : "other is not null && ";
		var expressionString = type.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, inlineArrayAttributeType))
			? "this[..].SequenceEqual(other[..])"
			: string.Join(" && ", from pair in referencedMembers where pair.ExtraData is not null select pair.ExtraData);
		var readOnlyModifier = kind == TypeKind.Struct && !isReadOnly ? "readonly " : string.Empty;
		var paramMarkup = string.IsNullOrEmpty(nullableToken) ? string.Empty : "[global::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute(true)] ";
		var largeStructModifier = attribute.GetNamedArgument(EquatableLargeStructModifierPropertyName, "in");
		return string.IsNullOrEmpty(inKeyword)
			? $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_EquatableEquals.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					/// <inheritdoc/>
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public {{otherModifiersString}}{{readOnlyModifier}}bool Equals({{paramMarkup}}{{fullTypeNameString}}{{nullableToken}} other)
						=> {{otherIsNullCheckString}}{{expressionString}};
				}
			#line default
			}
			"""
			: $$"""
			namespace {{namespaceString}}
			{
			#line 1 "{{typeNameString}}_EquatableEquals.g.cs"
				partial {{typeKindString}} {{typeNameString}}
				{
					/// <inheritdoc cref="global::System.IEquatable{T}.Equals(T)"/>
					[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
					[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
					public {{otherModifiersString}}{{readOnlyModifier}}bool Equals({{largeStructModifier}} {{fullTypeNameString}} other)
						=> {{expressionString}};

					/// <inheritdoc/>
					{{readOnlyModifier}}bool global::System.IEquatable<{{fullTypeNameString}}>.Equals({{paramMarkup}}{{fullTypeNameString}}{{nullableToken}} other)
						=> Equals(other);
				}
			#line default
			}
			""";
	}

	private static string? DisposableAndAsyncDisposable(GeneratorAttributeSyntaxContext gasc, CancellationToken cancellationToken)
	{
		if (gasc is not
			{
				Attributes: [{ ConstructorArguments: [{ Value: int ctorArg }] } attribute],
				TargetSymbol: INamedTypeSymbol
				{
					TypeKind: var kind and (TypeKind.Struct or TypeKind.Class),
					Name: var typeName,
					IsRecord: var isRecord,
					TypeParameters: var typeParameters,
					ContainingNamespace: var @namespace,
					ContainingType: null, // Must be top-level type.
					AllInterfaces: var allInterfaces
				} type,
				TargetNode: TypeDeclarationSyntax { ParameterList: var parameterList }
					and (RecordDeclarationSyntax or ClassDeclarationSyntax or StructDeclarationSyntax),
				SemanticModel: { Compilation: var compilation } semanticModel
			})
		{
			return null;
		}

		var paramTargetAttributeTypeNameSymbol = compilation.GetTypeByMetadataName(ParameterTargetAttributeTypeName);
		if (paramTargetAttributeTypeNameSymbol is null)
		{
			return null;
		}

		var disposableMemberAttributeSymbol = compilation.GetTypeByMetadataName(DisposableMemberAttributeTypeName);
		if (disposableMemberAttributeSymbol is null)
		{
			return null;
		}

		var alreadyGeneratedIsDisposedField = false;
		var d1 = o(
			TypeImplFlag.Disposable,
			compilation.GetTypeByMetadataName(DisposableTypeName)!,
			OtherModifiersOnDisposableDisposePropertyName,
			nameof(TypeImplFlag.Disposable),
			"Dispose",
			"void",
			ExplicitlyImplsDisposablePropertyName,
			ref alreadyGeneratedIsDisposedField
		);
		var d2 = o(
			TypeImplFlag.AsyncDisposable,
			compilation.GetTypeByMetadataName(AsyncDisposableTypeName)!,
			OtherModifiersOnAsyncDisposableDisposeAsyncPropertyName,
			nameof(TypeImplFlag.AsyncDisposable),
			"DisposeAsync",
			ValueTaskFullTypeName,
			ExplicitlyImplsAsyncDisposablePropertyName,
			ref alreadyGeneratedIsDisposedField
		);
		return (d1, d2) switch
		{
			(not null, not null) => $"{d1}\r\n\r\n{d2}",
			(not null, null) => d1,
			(null, not null) => d2,
			_ => null
		};


		string? o(
			TypeImplFlag checkFlag,
			INamedTypeSymbol disposableSymbol,
			string otherModifiersPropertyName,
			string lineDirectiveInterfaceDisplayName,
			string outputMethodName,
			string outputMethodReturnType,
			string explicitlyImplsDisposablePropertyName,
			ref bool alreadyGeneratedIsDisposedField
		)
		{
			if (!((TypeImplFlag)ctorArg).HasFlag(checkFlag))
			{
				return null;
			}

			if (!allInterfaces.Any(a => SymbolEqualityComparer.Default.Equals(a, disposableSymbol)))
			{
				return null;
			}

			var namespaceString = @namespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)["global::".Length..];
			var otherModifiers = attribute.GetNamedArgument<string>(otherModifiersPropertyName) switch
			{
				{ } str => str.Split([' '], StringSplitOptions.RemoveEmptyEntries),
				_ => []
			};
			var typeArgumentsString = typeParameters is []
				? string.Empty
				: $"<{string.Join(", ", from typeParameter in typeParameters select typeParameter.Name)}>";
			var typeNameString = $"{typeName}{typeArgumentsString}";
			var typeKindString = (kind, isRecord) switch
			{
				(TypeKind.Class, true) => "record",
				(TypeKind.Class, _) => "class",
				(TypeKind.Struct, true) => "record struct",
				(TypeKind.Struct, _) => "struct",
				_ => throw new InvalidOperationException("Invalid state.")
			};
			var otherModifiersString = otherModifiers.Length == 0 ? string.Empty : $"{string.Join(" ", otherModifiers)} ";
			var referencedMembers = PrimaryConstructor.MapMemberNames(
				type,
				semanticModel,
				parameterList,
				paramTargetAttributeTypeNameSymbol,
				a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, disposableMemberAttributeSymbol),
				symbol =>
				{
					var awaitKeyword = outputMethodReturnType == ValueTaskFullTypeName ? "await " : string.Empty;
					var questionMarkToken = symbol switch
					{
						IFieldSymbol { Type.NullableAnnotation: var a } => a,
						IPropertySymbol { Type.NullableAnnotation: var a } => a,
						_ => NotAnnotated
					} == Annotated ? "?" : string.Empty;
					return !string.IsNullOrEmpty(awaitKeyword) && questionMarkToken == "?"
						? $$"""
						if ({{symbol.Name}} is not null)
									{
										await {{symbol.Name}}.{{outputMethodName}}();
									}
						"""
						: $"{awaitKeyword}{symbol.Name}{questionMarkToken}.{outputMethodName}();";
				},
				cancellationToken
			);

			var explicitlyImplsType = attribute.GetNamedArgument(explicitlyImplsDisposablePropertyName, false);
			var includesDispose = (checkFlag & TypeImplFlag.Disposable) != 0;
			var includesDisposeAsync = (checkFlag & TypeImplFlag.AsyncDisposable) != 0;
			var seeCrefPart = (includesDispose, includesDisposeAsync) switch
			{
				(true, true) => """<see cref="Dispose"/> and <see cref="DisposeAsync"/>""",
				(true, _) => """<see cref="Dispose"/>""",
				(_, true) => """<see cref="DisposeAsync"/>""",
				_ => string.Empty
			};
			var seealsoCrefPart = (includesDispose, includesDisposeAsync) switch
			{
				(true, true) => """
						/// <seealso cref="Dispose"/>
						/// <seealso cref="DisposeAsync"/>
				""",
				(true, _) => """
						/// <seealso cref="Dispose"/>
				""",
				(_, true) => """
						/// <seealso cref="DisposeAsync"/>
				""",
				_ => string.Empty
			};
			var isDisposedField = explicitlyImplsType
				? "// Field '_isDisposed' won't be generated in explicitly interface implementation environment."
				: alreadyGeneratedIsDisposedField
					? "// Field '_isDisposed' has already been generated."
					: $$"""
					/// <summary>
							/// Indicates whether the object had already been disposed before {{seeCrefPart}} was called.
							/// If this field holds <see langword="false"/> value, {{seeCrefPart}} will throw an
							/// <see cref="global::System.ObjectDisposedException"/> to report the error.
							/// </summary>
					{{seealsoCrefPart}}
							/// <seealso cref="global::System.ObjectDisposedException"/>
							[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
							[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
							private bool _isDisposed;
					""";
			var asyncKeyword = outputMethodReturnType == ValueTaskFullTypeName ? "async " : string.Empty;
			var methodDeclarationLine = explicitlyImplsType
				? $$"""{{otherModifiersString}}{{asyncKeyword}}{{outputMethodReturnType}} global::System.I{{lineDirectiveInterfaceDisplayName}}.{{outputMethodName}}()"""
				: $$"""public {{otherModifiersString}}{{asyncKeyword}}{{outputMethodReturnType}} {{outputMethodName}}()""";
			var extraComment = explicitlyImplsType ? "// " : string.Empty;
			var result = $$"""
				namespace {{namespaceString}}
				{
				#line 1 "{{typeNameString}}_{{lineDirectiveInterfaceDisplayName}}.g.cs"
					partial {{typeKindString}} {{typeNameString}}
					{
						{{isDisposedField}}


						/// <inheritdoc/>
						/// <exception cref="ObjectDisposedException">Throws when the object had already been disposed.</exception>
						[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{{typeof(FileLocalHandler).FullName}}", "{{Value}}")]
						[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
						{{methodDeclarationLine}}
						{
							// Check whether the object has already been disposed.
							{{extraComment}}global::System.ObjectDisposedException.ThrowIf(_isDisposed, this);

							// Release related resources by calling 'Dispose' or 'DisposeAsync' methods.
							{{string.Join("\r\n\t\t\t", from pair in referencedMembers select pair.ExtraData)}}

							// Sets field '_isDiposed' to true in order to prevent further usages on duplicate disposal.
							{{extraComment}}_isDisposed = true;
						}
					}
				#line default
				}
				""";

			alreadyGeneratedIsDisposedField = true;
			return result;
		}
	}
}

/// <summary>
/// Indicates the implementation flags.
/// </summary>
[Flags]
file enum TypeImplFlag
{
	Object_Equals = 1 << 0,
	Object_GetHashCode = 1 << 1,
	Object_ToString = 1 << 2,
	EqualityOperators = 1 << 3,
	ComparisonOperators = 1 << 4,
	TrueAndFalseOperators = 1 << 5,
	LogicalNotOperator = 1 << 6,
	Equatable = 1 << 7,
	Disposable = 1 << 8,
	AsyncDisposable = 1 << 9
}

/// <summary>
/// Indicates the property kind of length or count property.
/// </summary>
file enum LengthOrCountPropertyKind
{
	Unknown,
	Length,
	Count
}

/// <summary>
/// Represents a behavior for generating <see cref="object.Equals(object)"/> method.
/// </summary>
/// <seealso cref="object.Equals(object)"/>
file enum EqualsBehavior
{
	ReturnFalse,
	IsCast,
	AsCast,
	Throw
}

/// <summary>
/// Represents a behavior for generating <see cref="object.GetHashCode"/> method.
/// </summary>
/// <seealso cref="object.GetHashCode"/>
file enum GetHashCodeBehavior
{
	ReturnNegativeOne,
	Direct,
	EnumExplicitCast,
	Specified,
	Throw,
	HashCodeAdd
}

/// <summary>
/// Represents a behavior for generating <see cref="object.ToString"/> method.
/// </summary>
/// <seealso cref="object.ToString"/>
file enum ToStringBehavior
{
	Throw,
	ReturnTypeName,
	CallOverload,
	Specified,
	RecordLike
}

/// <summary>
/// Represents a behavior for generating equality operators.
/// </summary>
file enum EqualityOperatorsBehavior
{
	DoNothing,
	Default,
	DefaultButDeprecated,
	WithScopedIn,
	WithScopedInButDeprecated
}

/// <summary>
/// Represents a behavior for generating comparison operators.
/// </summary>
file enum ComparisonOperatorsBehavior
{
	DoNothing,
	Default,
	DefaultButDeprecated,
	WithScopedIn,
	WithScopedInButDeprecated,
}

/// <summary>
/// Provides with primary constructor operations.
/// </summary>
file static class PrimaryConstructor
{
	/// <summary>
	/// Try to map member names.
	/// </summary>
	/// <typeparam name="TExtraData">The type of projected result after argument <paramref name="extraDataSelector"/> handled.</typeparam>
	/// <param name="this">The type symbol.</param>
	/// <param name="model">The semantic model instance.</param>
	/// <param name="parameterList">The parameter list.</param>
	/// <param name="targetAttributeSymbol">The primary constructor parameter type symbol.</param>
	/// <param name="attributeMatcher">Indicates the primary constructor attribute matcher.</param>
	/// <param name="extraDataSelector">Extra data selector.</param>
	/// <param name="cancellationToken">The cancellation token that can cancel the operation.</param>
	/// <returns>A list of values (member names and extra data).</returns>
	public static (string Name, TExtraData ExtraData)[] MapMemberNames<TExtraData>(
		INamedTypeSymbol @this,
		SemanticModel model,
		ParameterListSyntax? parameterList,
		INamedTypeSymbol targetAttributeSymbol,
		Func<AttributeData, bool> attributeMatcher,
		Func<ISymbol, TExtraData> extraDataSelector,
		CancellationToken cancellationToken
	)
	{
		const string Property = nameof(Property), Field = nameof(Field);
		var baseMembers =
			from member in @this.GetAllMembers()
			where member is IFieldSymbol or IPropertySymbol && member.GetAttributes().Any(attributeMatcher)
			select (member.Name, extraDataSelector(member));
		return parameterList is null
			? [.. baseMembers]
			: [
				..
				from parameter in parameterList.Parameters
				select model.GetDeclaredSymbol(parameter, cancellationToken) into parameterSymbol
				where !parameterSymbol.Type.IsRefLikeType // Ref structs cannot participate in the hashing.
				let attributesData = parameterSymbol.GetAttributes()
				where attributesData.Any(attributeMatcher)
				let targetAttributeData = attributesData.FirstOrDefault(m)
				let name = targetAttributeData.AttributeClass?.Name
				let parameterKind = name switch { $"{Field}Attribute" => Field, $"{Property}Attribute" => Property, _ => null }
				where parameterKind is Property or Field
				let memberConversion = parameterKind switch { Property => ">@", _ => "_<@" }
				let namedArguments = targetAttributeData.NamedArguments
				let parameterName = parameterSymbol.Name
				let referencedMemberName = getTargetMemberName(namedArguments, parameterName, memberConversion)
				select (referencedMemberName, extraDataSelector(parameterSymbol)),
				.. baseMembers
			];


		static string getTargetMemberName(NamedArgs namedArgs, string parameterName, string defaultPattern)
			=> namedArgs.TryGetValueOrDefault<string>("GeneratedMemberName", out var customizedFieldName)
			&& customizedFieldName is not null
				? customizedFieldName
				: namedArgs.TryGetValueOrDefault<string>("NamingRule", out var namingRule) && namingRule is not null
					? namingRule.InternalHandle(parameterName)
					: defaultPattern.InternalHandle(parameterName);

		bool m(AttributeData a) => SymbolEqualityComparer.Default.Equals(a.AttributeClass?.BaseType, targetAttributeSymbol);
	}
}

/// <include file='../../global-doc-comments.xml' path='g/csharp11/feature[@name="file-local"]/target[@name="class" and @when="extension"]'/>
file static class Extensions
{
	/// <summary>
	/// Internal handle the naming rule, converting it into a valid identifier via specified parameter name.
	/// </summary>
	/// <param name="this">The naming rule.</param>
	/// <param name="parameterName">The parameter name.</param>
	/// <returns>The final identifier.</returns>
	public static string InternalHandle(this string @this, string parameterName)
		=> @this
			.Replace("<@", parameterName.ToCamelCasing())
			.Replace(">@", parameterName.ToPascalCasing())
			.Replace("@", parameterName);

	/// <summary>
	/// Try to convert the specified identifier into pascal casing.
	/// </summary>
	public static string ToPascalCasing(this string @this) => $"{char.ToUpper(@this[0])}{@this[1..]}";

	/// <summary>
	/// Try to convert the specified identifier into camel casing.
	/// </summary>
	public static string ToCamelCasing(this string @this) => $"{char.ToLower(@this[0])}{@this[1..]}";

	/// <summary>
	/// Try to convert the specified identifier into camel casing, with a leading underscore character.
	/// </summary>
	public static string ToUnderscoreCamelCasing(this string @this) => $"_{@this.ToCamelCasing()}";
}
