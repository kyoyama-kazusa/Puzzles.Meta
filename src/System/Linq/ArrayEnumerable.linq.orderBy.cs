namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="Enumerable.ThenBy{TSource, TKey}(IOrderedEnumerable{TSource}, Func{TSource, TKey})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ArrayOrderedEnumerable<T> OrderBy<T, TKey>(this T[] @this, Func<T, TKey> selector)
		=> new(
			@this,
			(l, r) => (selector(l), selector(r)) switch
			{
				(IComparable<TKey> left, var right) => left.CompareTo(right),
				var (a, b) => Comparer<TKey>.Default.Compare(a, b)
			}
		);

	/// <inheritdoc cref="Enumerable.ThenByDescending{TSource, TKey}(IOrderedEnumerable{TSource}, Func{TSource, TKey})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ArrayOrderedEnumerable<T> OrderByDescending<T, TKey>(this T[] @this, Func<T, TKey> selector)
		=> new(
			@this,
			(l, r) => (selector(l), selector(r)) switch
			{
				(IComparable<TKey> left, var right) => -left.CompareTo(right),
				var (a, b) => -Comparer<TKey>.Default.Compare(a, b)
			}
		);
}
