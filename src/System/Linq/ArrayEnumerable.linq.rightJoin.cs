namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="IRightJoinMethod{TSelf, TSource}.RightJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult})"/>
	public static TResult?[] RightJoin<TOuter, TInner, TKey, TResult>(
		this TOuter[] outer,
		TInner[] inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter?, TInner, TResult?> resultSelector
	) where TKey : notnull => RightJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <inheritdoc cref="IRightJoinMethod{TSelf, TSource}.RightJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult}, IEqualityComparer{TKey}?)"/>
	public static TResult?[] RightJoin<TOuter, TInner, TKey, TResult>(
		this TOuter[] outer,
		TInner[] inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter?, TInner, TResult?> resultSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var outerLookup = outer.ToLookup(outerKeySelector, comparer);
		var result = new List<TResult?>();
		foreach (var innerElement in inner)
		{
			if (outerLookup[innerKeySelector(innerElement)] is var outerElements and not [])
			{
				foreach (var outerElement in outerElements)
				{
					result.AddRef(resultSelector(outerElement, innerElement));
				}
			}
			else
			{
				result.AddRef(resultSelector(default, innerElement));
			}
		}
		return [.. result];
	}
}
