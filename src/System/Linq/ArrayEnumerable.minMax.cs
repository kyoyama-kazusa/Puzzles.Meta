namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Invokes a transform function on each element of a sequence and returns the minimum <typeparamref name="TInterim"/> value.
	/// </summary>
	/// <typeparam name="T">The type of the elements of <paramref name="this"/>.</typeparam>
	/// <typeparam name="TInterim">The type of projected values after the transform function invoked.</typeparam>
	/// <param name="this">A sequence of values to determine the minimum value of.</param>
	/// <param name="selector">A transform function to apply to each element.</param>
	/// <returns>The value of type <typeparamref name="TInterim"/> that corresponds to the minimum value in the sequence.</returns>
	public static TInterim Min<T, TInterim>(this T[] @this, Func<T, TInterim> selector)
		where TInterim : IMinMaxValue<TInterim>, IComparisonOperators<TInterim, TInterim, bool>
	{
		var result = TInterim.MaxValue;
		foreach (var element in @this)
		{
			var elementCasted = selector(element);
			if (elementCasted <= result)
			{
				result = elementCasted;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Min{T, TInterim}(T[], Func{T, TInterim})"/>
	public static unsafe TInterim MinUnsafe<T, TInterim>(this T[] @this, delegate*<T, TInterim> selector)
		where TInterim : IMinMaxValue<TInterim>, IComparisonOperators<TInterim, TInterim, bool>
	{
		var result = TInterim.MaxValue;
		foreach (var element in @this)
		{
			var elementCasted = selector(element);
			if (elementCasted <= result)
			{
				result = elementCasted;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Enumerable.MinBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static TSource? MinBy<TSource, TKey>(this TSource[] @this, Func<TSource, TKey> keySelector)
		where TKey : IComparable<TKey>, allows ref struct
	{
		var result = default(TSource);
		var minValue = default(TKey);
		foreach (ref readonly var element in @this.AsReadOnlySpan())
		{
			var elementKey = keySelector(element);
			if (elementKey.CompareTo(minValue) <= 0)
			{
				result = element;
				minValue = elementKey;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Enumerable.MinBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}?)"/>
	public static TSource? MinBy<TSource, TKey, TComparer>(this TSource[] @this, Func<TSource, TKey> keySelector, TComparer? comparer)
		where TKey : allows ref struct
		where TComparer : IComparer<TKey>, new(), allows ref struct
	{
		comparer ??= new();

		var result = default(TSource);
		var minValue = default(TKey);
		foreach (ref readonly var element in @this.AsReadOnlySpan())
		{
			var elementKey = keySelector(element);
			if (comparer.Compare(elementKey, minValue) <= 0)
			{
				result = element;
				minValue = elementKey;
			}
		}
		return result;
	}

	/// <summary>
	/// Invokes a transform function on each element of a sequence and returns the maximum <typeparamref name="TInterim"/> value.
	/// </summary>
	/// <typeparam name="T">The type of the elements of <paramref name="this"/>.</typeparam>
	/// <typeparam name="TInterim">The type of projected values after the transform function invoked.</typeparam>
	/// <param name="this">A sequence of values to determine the maximum value of.</param>
	/// <param name="selector">A transform function to apply to each element.</param>
	/// <returns>The value of type <typeparamref name="TInterim"/> that corresponds to the maximum value in the sequence.</returns>
	public static TInterim Max<T, TInterim>(this T[] @this, Func<T, TInterim> selector)
		where TInterim : IMinMaxValue<TInterim>, IComparisonOperators<TInterim, TInterim, bool>
	{
		var result = TInterim.MinValue;
		foreach (var element in @this)
		{
			var elementCasted = selector(element);
			if (elementCasted >= result)
			{
				result = elementCasted;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Max{T, TInterim}(T[], Func{T, TInterim})"/>
	public static unsafe TInterim MaxUnsafe<T, TInterim>(this T[] @this, delegate*<T, TInterim> selector)
		where TInterim : IMinMaxValue<TInterim>, IComparisonOperators<TInterim, TInterim, bool>
	{
		var result = TInterim.MinValue;
		foreach (var element in @this)
		{
			var elementCasted = selector(element);
			if (elementCasted >= result)
			{
				result = elementCasted;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Enumerable.MaxBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static TSource? MaxBy<TSource, TKey>(this TSource[] @this, Func<TSource, TKey> keySelector)
		where TKey : IComparable<TKey>, allows ref struct
	{
		var result = default(TSource);
		var maxValue = default(TKey);
		foreach (ref readonly var element in @this.AsReadOnlySpan())
		{
			var elementKey = keySelector(element);
			if (elementKey.CompareTo(maxValue) >= 0)
			{
				result = element;
				maxValue = elementKey;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Enumerable.MaxBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}?)"/>
	public static TSource? MaxBy<TSource, TKey, TComparer>(this TSource[] @this, Func<TSource, TKey> keySelector, TComparer? comparer)
		where TKey : allows ref struct
		where TComparer : IComparer<TKey>, new(), allows ref struct
	{
		comparer ??= new();

		var result = default(TSource);
		var maxValue = default(TKey);
		foreach (ref readonly var element in @this.AsReadOnlySpan())
		{
			var elementKey = keySelector(element);
			if (comparer.Compare(elementKey, maxValue) >= 0)
			{
				result = element;
				maxValue = elementKey;
			}
		}
		return result;
	}
}
