namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <seealso cref="Span{T}"/>
/// <seealso cref="ReadOnlySpan{T}"/>
public static class ReadOnlySpanExtensions
{
	/// <summary>
	/// Finds the first element satisfying the specified condition, and return its corresponding index.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The sequence.</param>
	/// <param name="predicate">The condition.</param>
	/// <returns>
	/// An <see cref="int"/> indicating the found element. -1 returns if the sequence has no element satisfying the condition.
	/// </returns>
	public static int FirstIndex<T>(this ReadOnlySpan<T> @this, Func<T, bool> predicate)
	{
		for (var i = 0; i < @this.Length; i++)
		{
			if (predicate(@this[i]))
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// Finds the last element satisfying the specified condition, and return its corresponding index.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The sequence.</param>
	/// <param name="predicate">The condition.</param>
	/// <returns>
	/// An <see cref="int"/> indicating the found element. -1 returns if the sequence has no element satisfying the condition.
	/// </returns>
	public static int LastIndex<T>(this ReadOnlySpan<T> @this, Func<T, bool> predicate)
	{
		for (var i = @this.Length - 1; i >= 0; i--)
		{
			if (predicate(@this[i]))
			{
				return i;
			}
		}
		return -1;
	}

	/// <inheritdoc cref="List{T}.FindIndex(Predicate{T})"/>
	public static int FindIndex<T>(this ReadOnlySpan<T> @this, Func<T, bool> condition)
	{
		for (var i = 0; i < @this.Length; i++)
		{
			if (condition(@this[i]))
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// Returns a new <see cref="ReadOnlySpan{T}"/> instance whose internal elements are all come from the current collection,
	/// with reversed order.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The current collection.</param>
	/// <returns>A new collection whose elements are in reversed order.</returns>
	[OverloadResolutionPriority(1)]
	public static ReadOnlySpan<T> Reverse<T>(this scoped ReadOnlySpan<T> @this)
	{
		var result = new T[@this.Length];
		for (var (i, j) = (@this.Length - 1, 0); i >= 0; i--, j++)
		{
			result[j] = @this[i];
		}
		return result;
	}

	/// <summary>
	/// Iterates on each element, in reverse order.
	/// </summary>
	/// <typeparam name="T">The type of each element in the sequence.</typeparam>
	/// <param name="this">The sequence to be iterated.</param>
	/// <returns>An enumerator type that iterates on each element.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReverseEnumerator<T> EnumerateReversely<T>(this ReadOnlySpan<T> @this) => new(@this);

	/// <summary>
	/// Creates a <see cref="PairEnumerator{T}"/> instance that iterates on each element of pair elements.
	/// </summary>
	/// <typeparam name="T">The type of the array elements.</typeparam>
	/// <param name="this">The array.</param>
	/// <returns>An enumerable collection.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PairEnumerator<T> EnumeratePaired<T>(this ReadOnlySpan<T> @this) where T : notnull
		=> new(
			(@this.Length & 1) == 0
				? @this
				: throw new ArgumentException(SR.ExceptionMessage("SpecifiedValueMustBeEven"), nameof(@this))
		);

	/// <summary>
	/// Retrieves all the elements that match the conditions defined by the specified predicate.
	/// </summary>
	/// <typeparam name="T">The type of the elements of the span.</typeparam>
	/// <param name="this">The collection to be used and checked.</param>
	/// <param name="match">The <see cref="Func{T, TResult}"/> that defines the conditions of the elements to search for.</param>
	/// <returns>
	/// A <see cref="ReadOnlySpan{T}"/> containing all the elements that match the conditions defined
	/// by the specified predicate, if found; otherwise, an empty <see cref="ReadOnlySpan{T}"/>.
	/// </returns>
	public static ReadOnlySpan<T> FindAll<T>(this ReadOnlySpan<T> @this, Func<T, bool> match)
	{
		var result = new List<T>(@this.Length);
		foreach (ref readonly var element in @this)
		{
			if (match(element))
			{
				result.AddRef(element);
			}
		}
		return result.AsSpan();
	}
}
