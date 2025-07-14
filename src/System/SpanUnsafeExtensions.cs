namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Span{T}"/>.
/// </summary>
public static class SpanUnsafeExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Span{T}"/>.
	/// </summary>
	extension<T>(Span<T> span)
	{
		/// <summary>
		/// Converts the current instance as a read-only span.
		/// </summary>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> instance.</returns>
		public ReadOnlySpan<T> AsReadOnly() => ReadOnlySpan<T>.Create(in span[0], span.Length);


		/// <summary>
		/// Creates a <see cref="Span{T}"/> instance.
		/// </summary>
		/// <param name="reference">The reference to the first element.</param>
		/// <param name="length">The length.</param>
		/// <returns><see cref="Span{T}"/> instance.</returns>
		public static unsafe Span<T> Create(ref T reference, int length) => new(Unsafe.AsPointer(ref reference), length);
	}
}
