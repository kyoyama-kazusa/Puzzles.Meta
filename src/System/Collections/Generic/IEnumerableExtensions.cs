namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="IEnumerable{T}"/> instances.
/// </summary>
/// <seealso cref="IEnumerable{T}"/>
public static class IEnumerableExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="IEnumerable{T}"/>.
	/// </summary>
	extension<T>(IEnumerable<T> @this)
	{
#if INCLUDES_IS_EMPTY_PROPERTY && false
		/// <summary>
		/// Indicates whether the sequence has no elements.
		/// </summary>
		public bool IsEmpty => @this.Any();
#endif

#if INCLUDES_LENGTH_PROPERTY && false
		/// <summary>
		/// Indicates the length of the sequence.
		/// </summary>
		public int Length => @this.Count();
#endif


#if false
		/// <summary>
		/// Gets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The element.</returns>
		public T this[int index] => @this.ElementAt(index);

		/// <inheritdoc cref="get_Item{T}(IEnumerable{T}, int)"/>
		public T this[Index index] => @this.ElementAt(index);

		/// <summary>
		/// Gets the slice of the specified range.
		/// </summary>
		/// <param name="range">The range.</param>
		/// <returns>The slice values.</returns>
		public IEnumerable<T> this[Range range]
		{
			get
			{
				var (offset, length) = range.GetOffsetAndLength(@this.Length);
				return @this.Slice(offset, length);
			}
		}
#endif


		/// <summary>
		/// Slices the current sequence.
		/// </summary>
		/// <param name="start">The start index.</param>
		/// <param name="length">The desired length.</param>
		/// <returns>A new sequence start at index <paramref name="start"/>.</returns>
		/// <exception cref="InvalidOperationException">Throws when the sequence has no valid elements to be iterated.</exception>
		public IEnumerable<T> Slice(int start, int length)
		{
			var skiped = @this.Skip(start);
			if (!skiped.Any())
			{
				throw new InvalidOperationException();
			}

			using var enumerator = skiped.GetEnumerator();
			for (var i = 0; i < length; i++)
			{
				enumerator.MoveNext();
				yield return enumerator.Current;
			}
		}
	}
}
