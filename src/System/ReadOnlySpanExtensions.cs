namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <seealso cref="Span{T}"/>
/// <seealso cref="ReadOnlySpan{T}"/>
public static class ReadOnlySpanExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	extension<T>(ReadOnlySpan<T> @this)
	{
		/// <summary>
		/// Finds the first element satisfying the specified condition, and return its corresponding index.
		/// </summary>
		/// <param name="predicate">The condition.</param>
		/// <returns>
		/// An <see cref="int"/> indicating the found element. -1 returns if the sequence has no element satisfying the condition.
		/// </returns>
		public int FirstIndex(Func<T, bool> predicate)
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
		/// <param name="predicate">The condition.</param>
		/// <returns>
		/// An <see cref="int"/> indicating the found element. -1 returns if the sequence has no element satisfying the condition.
		/// </returns>
		public int LastIndex(Func<T, bool> predicate)
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
		public int FindIndex(Func<T, bool> condition)
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
		/// Iterates on each element, in reverse order.
		/// </summary>
		/// <returns>An enumerator type that iterates on each element.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReverseEnumerator<T> EnumerateReversely() => new(@this);

		/// <summary>
		/// Retrieves all the elements that match the conditions defined by the specified predicate.
		/// </summary>
		/// <param name="match">The <see cref="Func{T, TResult}"/> that defines the conditions of the elements to search for.</param>
		/// <returns>
		/// A <see cref="ReadOnlySpan{T}"/> containing all the elements that match the conditions defined
		/// by the specified predicate, if found; otherwise, an empty <see cref="ReadOnlySpan{T}"/>.
		/// </returns>
		public ReadOnlySpan<T> FindAll(Func<T, bool> match)
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

	/// <summary>
	/// Provides extension members on <see cref="ReadOnlySpan{T}"/>,
	/// where <typeparamref name="T"/> satisfies <see langword="notnull"/> constraint.
	/// </summary>
	extension<T>(ReadOnlySpan<T> @this) where T : notnull
	{
		/// <summary>
		/// Creates a <see cref="PairEnumerator{T}"/> instance that iterates on each element of pair elements.
		/// </summary>
		/// <returns>An enumerable collection.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PairEnumerator<T> EnumeratePaired()
			=> new(
				(@this.Length & 1) == 0
					? @this
					: throw new ArgumentException(SR.ExceptionMessage("SpecifiedValueMustBeEven"), nameof(@this))
			);
	}

	/// <summary>
	/// Provides extension members on <see langword="scoped"/> <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	extension<T>(scoped ReadOnlySpan<T> @this)
	{
		/// <summary>
		/// Returns a new <see cref="ReadOnlySpan{T}"/> instance whose internal elements are all come from the current collection,
		/// with reversed order.
		/// </summary>
		/// <returns>A new collection whose elements are in reversed order.</returns>
		[OverloadResolutionPriority(1)]
		public ReadOnlySpan<T> Reverse()
		{
			var result = new T[@this.Length];
			for (var (i, j) = (@this.Length - 1, 0); i >= 0; i--, j++)
			{
				result[j] = @this[i];
			}
			return result;
		}
	}
}
