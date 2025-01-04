namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static SpanLookup<TKey, TSource> ToLookup<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : notnull
		=> @this.ToLookup(keySelector, @delegate.Self, null);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey}?)"/>
	public static SpanLookup<TKey, TSource> ToLookup<TSource, TKey>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
		=> @this.ToLookup(keySelector, @delegate.Self, comparer);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement})"/>
	public static SpanLookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector
	)
		where TKey : notnull
		=> @this.ToLookup(keySelector, elementSelector, null);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement}, IEqualityComparer{TKey}?)"/>
	public static SpanLookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
	{
		var dictionary = new Dictionary<TKey, List<TElement>>(comparer);
		foreach (var source in @this)
		{
			var key = keySelector(source);
			var element = elementSelector(source);
			if (!dictionary.TryAdd(key, [element]))
			{
				dictionary[key].AddRef(in element);
			}
		}
		return new(dictionary.ToDictionary(static kvp => kvp.Key, static kvp => kvp.Value.ToArray()));
	}
}
