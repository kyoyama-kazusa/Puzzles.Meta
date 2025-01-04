namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static Lookup<TKey, TSource> ToLookup<TSource, TKey>(this TSource[] @this, Func<TSource, TKey> keySelector)
		where TKey : notnull
		=> @this.ToLookup(keySelector, @delegate.Self, null);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey}?)"/>
	public static Lookup<TKey, TSource> ToLookup<TSource, TKey>(
		this TSource[] @this,
		Func<TSource, TKey> keySelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
		=> @this.ToLookup(keySelector, @delegate.Self, comparer);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement})"/>
	public static Lookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this TSource[] @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector
	)
		where TKey : notnull
		=> @this.ToLookup(keySelector, elementSelector, null);

	/// <inheritdoc cref="Enumerable.ToLookup{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement}, IEqualityComparer{TKey}?)"/>
	public static Lookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(
		this TSource[] @this,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector,
		IEqualityComparer<TKey>? comparer
	)
		where TKey : notnull
		=> (Lookup<TKey, TElement>)@this.AsEnumerable().ToLookup(keySelector, elementSelector, comparer);
}
