namespace Puzzles.SourceGeneration;

/// <summary>
/// Represents a source generator type that runs multiple different usage of source output services on compiling code.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
	/// <inheritdoc/>
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		// Primary constructor.
		context.RegisterSourceOutput(
			context.SyntaxProvider
				.CreateSyntaxProvider(
					static (n, _) => n is TypeDeclarationSyntax { Modifiers: var m and not [] } && m.Any(SyntaxKind.PartialKeyword),
					PrimaryConstructorMemberHandler.Transform
				)
				.Where(NotNullPredicate)
				.Select(NotNullSelector)
				.Collect(),
			PrimaryConstructorMemberHandler.Output
		);

		// Type implementation.
		context.RegisterSourceOutput(
			context.SyntaxProvider
				.ForAttributeWithMetadataName(
					"System.Diagnostics.CodeAnalysis.TypeImplAttribute",
					IsPartialTypePredicate,
					TypeImplHandler.Transform
				)
				.Collect(),
			TypeImplHandler.Output
		);
	}


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
