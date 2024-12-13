namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IOrderByMethod{TSelf, TSource}.OrderBy{TKey}(Func{TSource, TKey})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static SpanOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> selector)
		=> new(
			@this,
			(Func<TSource, TSource, int>[])[
				(l, r) => (selector(l), selector(r)) switch
				{
					(IComparable<TKey> left, var right) => left.CompareTo(right),
					var (a, b) => Comparer<TKey>.Default.Compare(a, b)
				}
			]
		);

	/// <inheritdoc cref="IOrderByMethod{TSelf, TSource}.OrderByDescending{TKey}(Func{TSource, TKey})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static SpanOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> selector)
		=> new(
			@this,
			(Func<TSource, TSource, int>[])[
				(l, r) => (selector(l), selector(r)) switch
				{
					(IComparable<TKey> left, var right) => -left.CompareTo(right),
					var (a, b) => -Comparer<TKey>.Default.Compare(a, b)
				}
			]
		);
}
