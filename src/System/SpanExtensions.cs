namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <seealso cref="Span{T}"/>
/// <seealso cref="ReadOnlySpan{T}"/>
public static class SpanExtensions
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

	/// <inheritdoc cref="FindAll{T}(ReadOnlySpan{T}, FuncRefReadOnly{T, bool})"/>
	public static ReadOnlySpan<T> FindAll<T>(this ReadOnlySpan<T> @this, Func<T, bool> match)
	{
		var result = new List<T>(@this.Length);
		foreach (ref readonly var element in @this)
		{
			if (match(element))
			{
				result.AddRef(in element);
			}
		}
		return result.AsSpan();
	}

	/// <summary>
	/// Retrieves all the elements that match the conditions defined by the specified predicate.
	/// </summary>
	/// <typeparam name="T">The type of the elements of the span.</typeparam>
	/// <param name="this">The collection to be used and checked.</param>
	/// <param name="match">The <see cref="FuncRefReadOnly{T, TResult}"/> that defines the conditions of the elements to search for.</param>
	/// <returns>
	/// A <see cref="ReadOnlySpan{T}"/> containing all the elements that match the conditions defined
	/// by the specified predicate, if found; otherwise, an empty <see cref="ReadOnlySpan{T}"/>.
	/// </returns>
	public static ReadOnlySpan<T> FindAll<T>(this ReadOnlySpan<T> @this, FuncRefReadOnly<T, bool> match)
	{
		var result = new List<T>(@this.Length);
		foreach (ref readonly var element in @this)
		{
			if (match(in element))
			{
				result.AddRef(in element);
			}
		}
		return result.AsSpan();
	}
}
