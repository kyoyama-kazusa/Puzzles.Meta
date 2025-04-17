namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TResult> Join<TOuter, TInner, TKey, TResult>(
		this ReadOnlySpan<TOuter> outer,
		ReadOnlySpan<TInner> inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner, TResult> resultSelector
	) where TKey : notnull => Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.Join{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, TInner, TResult}, IEqualityComparer{TKey}?)"/>
	public static ReadOnlySpan<TResult> Join<TOuter, TInner, TKey, TResult>(
		this ReadOnlySpan<TOuter> outer,
		ReadOnlySpan<TInner> inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner, TResult> resultSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var result = new List<TResult>(outer.Length * inner.Length);
		foreach (var outerItem in outer)
		{
			var outerKey = outerKeySelector(outerItem);
			var outerKeyHash = comparer.GetHashCode(outerKey);
			foreach (var innerItem in inner)
			{
				var innerKey = innerKeySelector(innerItem);
				var innerKeyHash = comparer.GetHashCode(innerKey);
				if (outerKeyHash != innerKeyHash)
				{
					// They are not same due to hash code difference.
					continue;
				}

				if (!comparer.Equals(outerKey, innerKey))
				{
					// They are not same due to inequality.
					continue;
				}

				result.AddRef(resultSelector(outerItem, innerItem));
			}
		}
		return result.AsSpan();
	}

	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.GroupJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, IEnumerable{TInner}, TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
		this ReadOnlySpan<TOuter> outer,
		ReadOnlySpan<TInner> inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner[], TResult> resultSelector
	) where TKey : notnull => GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);

	/// <inheritdoc cref="IJoinMethod{TSelf, TSource}.GroupJoin{TInner, TKey, TResult}(IEnumerable{TInner}, Func{TSource, TKey}, Func{TInner, TKey}, Func{TSource, IEnumerable{TInner}, TResult}, IEqualityComparer{TKey}?)"/>
	public static ReadOnlySpan<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
		this ReadOnlySpan<TOuter> outer,
		ReadOnlySpan<TInner> inner,
		Func<TOuter, TKey> outerKeySelector,
		Func<TInner, TKey> innerKeySelector,
		Func<TOuter, TInner[], TResult> resultSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
	{
		comparer ??= EqualityComparer<TKey>.Default;

		var innerKvps = from element in inner select new KeyValuePair<TKey, TInner>(innerKeySelector(element), element);
		var result = new List<TResult>();
		foreach (var outerItem in outer)
		{
			var outerKey = outerKeySelector(outerItem);
			var outerKeyHash = comparer.GetHashCode(outerKey);
			var satisfiedInnerKvps = new List<TInner>(innerKvps.Length);
			foreach (var kvp in innerKvps)
			{
				ref readonly var innerKey = ref kvp.KeyRef;
				ref readonly var innerItem = ref kvp.ValueRef;
				var innerKeyHash = comparer.GetHashCode(innerKey);
				if (outerKeyHash != innerKeyHash)
				{
					// They are not same due to hash code difference.
					continue;
				}

				if (!comparer.Equals(outerKey, innerKey))
				{
					// They are not same due to inequality.
					continue;
				}

				satisfiedInnerKvps.AddRef(innerItem);
			}
			result.AddRef(resultSelector(outerItem, [.. satisfiedInnerKvps]));
		}
		return result.AsSpan();
	}
}
