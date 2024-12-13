namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IMinMaxMethod{TSelf, TSource}.Min()"/>
	public static TSource Min<TSource>(this ReadOnlySpan<TSource> @this)
		where TSource : IComparisonOperators<TSource, TSource, bool>, IMinMaxValue<TSource>
	{
		var result = TSource.MaxValue;
		foreach (ref readonly var element in @this)
		{
			if (element <= result)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="MinBy{TSource, TKey}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, TKey})"/>
	public static TKey Min<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var resultKey = TKey.MaxValue;
		foreach (var element in @this)
		{
			var key = keySelector(element);
			if (key <= resultKey)
			{
				resultKey = key;
			}
		}
		return resultKey;
	}

	/// <inheritdoc cref="MinBy{TSource, TKey}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, TKey})"/>
	public static TKey Min<TSource, TKey>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var resultKey = TKey.MaxValue;
		foreach (ref readonly var element in @this)
		{
			var key = keySelector(in element);
			if (key <= resultKey)
			{
				resultKey = key;
			}
		}
		return resultKey;
	}

	/// <inheritdoc cref="IMinMaxMethod{TSelf, TSource}.MinBy{TKey}(Func{TSource, TKey})"/>
	public static TSource? MinBy<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var (resultKey, result) = (TKey.MaxValue, default(TSource));
		foreach (var element in @this)
		{
			if (keySelector(element) <= resultKey)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="MinBy{TSource, TKey}(ReadOnlySpan{TSource}, Func{TSource, TKey})"/>
	public static TSource? MinBy<TSource, TKey>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var (resultKey, result) = (TKey.MaxValue, default(TSource));
		foreach (ref readonly var element in @this)
		{
			if (keySelector(in element) <= resultKey)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Min{TSource, TKey}(ReadOnlySpan{TSource}, Func{TSource, TKey})"/>
	public static unsafe TResult? MinUnsafe<T, TResult>(this ReadOnlySpan<T> @this, delegate*<T, TResult> selector)
		where TResult : IMinMaxValue<TResult>, IComparisonOperators<TResult, TResult, bool>
	{
		var result = TResult.MaxValue;
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

	/// <inheritdoc cref="IMinMaxMethod{TSelf, TSource}.Max()"/>
	public static TSource Max<TSource>(this ReadOnlySpan<TSource> @this)
		where TSource : IComparisonOperators<TSource, TSource, bool>, IMinMaxValue<TSource>
	{
		var result = TSource.MinValue;
		foreach (ref readonly var element in @this)
		{
			if (element >= result)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="MaxBy{TSource, TKey}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, TKey})"/>
	public static TKey Max<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var resultKey = TKey.MinValue;
		foreach (var element in @this)
		{
			var key = keySelector(element);
			if (key >= resultKey)
			{
				resultKey = key;
			}
		}
		return resultKey;
	}

	/// <inheritdoc cref="MaxBy{TSource, TKey}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, TKey})"/>
	public static TKey Max<TSource, TKey>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var resultKey = TKey.MinValue;
		foreach (ref readonly var element in @this)
		{
			var key = keySelector(in element);
			if (key >= resultKey)
			{
				resultKey = key;
			}
		}
		return resultKey;
	}

	/// <inheritdoc cref="IMinMaxMethod{TSelf, TSource}.MaxBy{TKey}(Func{TSource, TKey})"/>
	public static TSource? MaxBy<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var (resultKey, result) = (TKey.MinValue, default(TSource));
		foreach (var element in @this)
		{
			if (keySelector(element) >= resultKey)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="MaxBy{TSource, TKey}(ReadOnlySpan{TSource}, Func{TSource, TKey})"/>
	public static TSource? MaxBy<TSource, TKey>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, TKey> keySelector)
		where TKey : IMinMaxValue<TKey>, IComparisonOperators<TKey, TKey, bool>
	{
		var (resultKey, result) = (TKey.MinValue, default(TSource));
		foreach (ref readonly var element in @this)
		{
			if (keySelector(in element) >= resultKey)
			{
				result = element;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Max{TSource, TInterim}(ReadOnlySpan{TSource}, Func{TSource, TInterim})"/>
	public static unsafe TResult MaxUnsafe<T, TResult>(this ReadOnlySpan<T> @this, delegate*<T, TResult> selector)
		where TResult : IMinMaxValue<TResult>, IComparisonOperators<TResult, TResult, bool>
	{
		var result = TResult.MinValue;
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
}
