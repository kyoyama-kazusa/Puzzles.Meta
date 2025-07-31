namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents an attribute type that can help developers declare types simpler and easier on generating particular code.
/// For example, automatically implementing <see cref="IEquatable{T}"/>.
/// </summary>
/// <param name="flags"><inheritdoc cref="Flags" path="/summary"/></param>
/// <remarks>
/// For example, we have defined a record-like type <c>MyColor</c> declared like this:
/// <code><![CDATA[
/// public readonly struct MyColor(byte a, byte r, byte g, byte b) : IEquatable<MyColor>
/// {
///     public byte A { get; } = a;
///     public byte R { get; } = r;
///     public byte G { get; } = g;
///     public byte B { get; } = b;
///     private int RawValue => A << 24 | R << 16 | G << 8 | B;
/// 
///     public override bool Equals([NotNullWhen(true)] object? other)
///         => other is MyColor comparer && Equals(comparer);
/// 
///     public bool Equals(MyColor other) => RawValue == other.RawValue;
/// 
///     public override int GetHashCode() => RawValue;
/// }
/// ]]></code>
/// By using <see cref="TypeImplAttribute"/>, the code can be simplified to this:
/// <code><![CDATA[
/// [TypeImpl(TypeImplFlags.Equals | TypeImplFlags.GetHashCode | TypeImplFlags.Equatable)]
/// public readonly partial struct MyColor([Property] byte a, [Property] byte r, [Property] byte g, [Property] byte b) : IEquatable<MyColor>
/// {
///     [HashCodeMember]
///     [EquatableMember]
///     private int RawValue => A << 24 | R << 16 | G << 8 | B;
/// }
/// ]]></code>
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class TypeImplAttribute(TypeImplFlags flags) : Attribute
{
	/// <summary>
	/// Indicates whether source generators will generate source code with modifiers <see langword="ref readonly"/>
	/// or <see langword="in"/> onto parameters if the parameter type is the current type.
	/// </summary>
	/// <remarks>
	/// The value is <see langword="false"/> by default.
	/// </remarks>
	public bool IsLargeStructure { get; init; } = false;

	/// <summary>
	/// Indicates the flags whose corresponding member will be generated.
	/// </summary>
	public TypeImplFlags Flags { get; } = flags;
}
