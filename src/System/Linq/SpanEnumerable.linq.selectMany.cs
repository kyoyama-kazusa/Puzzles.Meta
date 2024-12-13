namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ISelectManyMethod{TSelf, TSource}.SelectMany{TCollection, TResult}(Func{TSource, IEnumerable{TCollection}}, Func{TSource, TCollection, TResult})"/>
	public static ReadOnlySpan<TResult> SelectMany<TSource, TCollection, TResult>(
		this ReadOnlySpan<TSource> source,
		Func<TSource, ReadOnlySpan<TCollection>> collectionSelector,
		Func<TSource, TCollection, TResult> resultSelector
	)
	{
		var length = source.Length;
		var result = new List<TResult>(length << 1);
		for (var i = 0; i < length; i++)
		{
			var element = source[i];
			foreach (ref readonly var subElement in collectionSelector(element))
			{
				result.AddRef(resultSelector(element, subElement));
			}
		}
		return result.ToArray();
	}
}
