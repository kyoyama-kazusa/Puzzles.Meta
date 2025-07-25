namespace System;

public partial class SequenceExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ReadOnlyMemory{T}"/>.
	/// </summary>
	extension<T>(ReadOnlyMemory<T> @this)
	{
#if false
		/// <summary>
		/// Gets the element at the specified index.
		/// </summary>
		/// <param name="index">The desired index.</param>
		/// <returns>The reference to the element at the specified index.</returns>
		public ref readonly T this[int index] => ref @this.Span[index];

		/// <inheritdoc cref="get_Item{T}(ReadOnlyMemory{T}, int)"/>
		public ref readonly T this[Index index] => ref @this.Span[index];
#endif


		/// <summary>
		/// Fetch the element at the specified index inside a <see cref="ReadOnlyMemory{T}"/>.
		/// </summary>
		/// <param name="index">The desired index.</param>
		/// <returns>The reference to the element at the specified index.</returns>
		public ref readonly T ElementAt(int index) => ref @this.Span[index];

		/// <inheritdoc cref="ElementAt{T}(ReadOnlyMemory{T}, int)"/>
		public ref readonly T ElementAt(Index index) => ref @this.Span[index];

		/// <summary>
		/// Creates a <see cref="ReadOnlyMemoryEnumerator{T}"/> instance that can be consumed by a <see langword="foreach"/> loop.
		/// </summary>
		/// <returns>A <see cref="ReadOnlyMemoryEnumerator{T}"/> instance.</returns>
		public ReadOnlyMemoryEnumerator<T> GetEnumerator() => new(@this);
	}
}
