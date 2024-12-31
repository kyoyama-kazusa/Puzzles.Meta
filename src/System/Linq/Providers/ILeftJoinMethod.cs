namespace System.Linq.Providers;

/// <summary>
/// Represents a type that supports method group <c>LeftJoin</c>.
/// </summary>
/// <inheritdoc/>
public interface ILeftJoinMethod<TSelf, TSource> : IQueryExpressionMethod<TSelf, TSource>
	where TSelf : ILeftJoinMethod<TSelf, TSource>, allows ref struct
	where TSource : allows ref struct
{
	/// <summary>
	/// Performs a left outer join with the other sequence.
	/// </summary>
	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult})"/>
	public virtual IEnumerable<TResult?> LeftJoin<TInner, TKey, TResult>(
		IEnumerable<TInner> inner,
		Func<TSource, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TSource, TInner?, TResult?> resultSelector
	) where TKey : notnull => LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <summary>
	/// Performs a left outer join with the other sequence.
	/// </summary>
	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult}, IEqualityComparer{TKey}?)"/>
	public virtual IEnumerable<TResult?> LeftJoin<TInner, TKey, TResult>(
		IEnumerable<TInner> inner,
		Func<TSource, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TSource, TInner?, TResult?> resultSelector,
		IEqualityComparer<TKey>? comparer
	) where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var innerLookup = inner.ToLookup(innerKeySelector, comparer);
		var result = new List<TResult?>();
		foreach (var outerElement in this)
		{
			var key = outerKeySelector(outerElement);
			var innerElements = innerLookup[key];
			if (innerElements.Any())
			{
				foreach (var innerElement in innerElements)
				{
					result.Add(resultSelector(outerElement, innerElement));
				}
			}
			else
			{
				result.Add(resultSelector(outerElement, default));
			}
		}
		return result;
	}
}
