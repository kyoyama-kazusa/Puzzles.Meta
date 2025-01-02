namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static Lookup<TKey, TSource> ToLookup<TSource, TKey>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector
	) => (Lookup<TKey, TSource>)@this.ToArray().AsEnumerable().ToLookup(keySelector);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey}?)"/>
	public static Lookup<TKey, TSource> ToLookup<TSource, TKey>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		IEqualityComparer<TKey>? comparer
	) => (Lookup<TKey, TSource>)@this.ToArray().AsEnumerable().ToLookup(keySelector, comparer);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement})"/>
	public static Lookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector
	) => (Lookup<TKey, TElement>)@this.ToArray().AsEnumerable().ToLookup(keySelector, elementSelector);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement}, IEqualityComparer{TKey}?)"/>
	public static Lookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this ReadOnlySpan<TSource> @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector,
		IEqualityComparer<TKey>? comparer
	) => (Lookup<TKey, TElement>)@this.ToArray().AsEnumerable().ToLookup(keySelector, elementSelector, comparer);
}
