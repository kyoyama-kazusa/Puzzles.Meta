namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="ILeftJoinMethod{TSelf, TSource}.LeftJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult?[] LeftJoin<TOuter, TInner, TKey, TResult>(
		this TOuter[] outer,
		TInner[] inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner?, TResult?> resultSelector
	) where TKey : notnull => LeftJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <inheritdoc cref="ILeftJoinMethod{TSelf, TSource}.LeftJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult}, IEqualityComparer{TKey}?)"/>
	public static TResult?[] LeftJoin<TOuter, TInner, TKey, TResult>(
		this TOuter[] outer,
		TInner[] inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner?, TResult?> resultSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var innerLookup = inner.ToLookup(innerKeySelector, comparer);
		var result = new List<TResult?>();
		foreach (var outerElement in outer)
		{
			if (innerLookup[outerKeySelector(outerElement)] is var innerElements and not [])
			{
				foreach (var innerElement in innerElements)
				{
					result.AddRef(resultSelector(outerElement, innerElement));
				}
			}
			else
			{
				result.AddRef(resultSelector(outerElement, default));
			}
		}
		return [.. result];
	}
}
