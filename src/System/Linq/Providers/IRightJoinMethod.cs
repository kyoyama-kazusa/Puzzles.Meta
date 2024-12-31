namespace System.Linq.Providers;

/// <summary>
/// Represents a type that supports method group <c>RightJoin</c>.
/// </summary>
/// <inheritdoc/>
public interface IRightJoinMethod<TSelf, TSource> : IQueryExpressionMethod<TSelf, TSource>
	where TSelf : IRightJoinMethod<TSelf, TSource>, allows ref struct
{
	/// <summary>
	/// Performs a right outer join with the other sequence.
	/// </summary>
	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult})"/>
	public virtual IEnumerable<TResult?> RightJoin<TInner, TKey, TResult>(
		IEnumerable<TInner> inner,
		Func<TSource, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TSource?, TInner, TResult?> resultSelector
	) where TKey : notnull => RightJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <summary>
	/// Performs a right outer join with the other sequence.
	/// </summary>
	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult}, IEqualityComparer{TKey}?)"/>
	public virtual IEnumerable<TResult?> RightJoin<TInner, TKey, TResult>(
		IEnumerable<TInner> inner,
		Func<TSource, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TSource?, TInner, TResult?> resultSelector,
		IEqualityComparer<TKey>? comparer
	) where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var outerLookup = this.ToLookup(outerKeySelector, comparer);
		var result = new List<TResult?>();
		foreach (var innerElement in inner)
		{
			var key = innerKeySelector(innerElement);
			var outerElements = outerLookup[key];
			if (outerElements.Any())
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
		return result;
	}
}
