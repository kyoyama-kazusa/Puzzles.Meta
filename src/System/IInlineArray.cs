namespace System;

/// <summary>
/// Represents a type that supports members which are generated automatically if the type is inline array,
/// marked <see cref="InlineArrayAttribute"/>.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TElement">The type of each element.</typeparam>
/// <seealso cref="InlineArrayAttribute"/>
public interface IInlineArray<TSelf, TElement> where TSelf : struct, IInlineArray<TSelf, TElement>, allows ref struct
{
	/// <summary>
	/// Indicates the elements. For inline arrays, the value is equivalent to <c><see langword="this"/>[..]</c>.
	/// </summary>
	[UnscopedRef]
	public abstract ReadOnlySpan<TElement> Elements { get; }


	/// <summary>
	/// Indicates the length of inline array configured in <see cref="InlineArrayAttribute"/>.
	/// </summary>
	public static abstract int InlineArrayLength { get; }


	/// <summary>
	/// Returns the reference to the element at the specified index.
	/// </summary>
	/// <param name="index">The desired index.</param>
	/// <returns>The reference to the element.</returns>
	/// <exception cref="IndexOutOfRangeException">Throws when the index is out of range.</exception>
	[UnscopedRef]
	public abstract ref TElement this[int index] { get; }


	/// <summary>
	/// Returns the reference that represents the current instance.
	/// Generally the method returns <c><see langword="ref"/> <see langword="this"/>[0]</c>.
	/// </summary>
	/// <returns>The reference that represents the current instance.</returns>
	[UnscopedRef]
	public ref TElement GetPinnableReference();

	/// <summary>
	/// Cast the current instance into a <see cref="Span{T}"/> instance.
	/// </summary>
	/// <returns>The <see cref="Span{T}"/> instance.</returns>
	[UnscopedRef]
	public abstract Span<TElement> AsSpan();

	/// <summary>
	/// Cast the current instance into a <see cref="ReadOnlySpan{T}"/> instance.
	/// </summary>
	/// <returns>The <see cref="ReadOnlySpan{T}"/> instance.</returns>
	[UnscopedRef]
	public abstract ReadOnlySpan<TElement> AsReadOnlySpan();
}
