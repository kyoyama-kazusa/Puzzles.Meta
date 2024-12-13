namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IWhereMethod{TSelf, TSource}.Where(Func{TSource, bool})"/>
	public static ReadOnlySpan<TSource> Where<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> predicate)
	{
		var result = new TSource[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			if (predicate(element))
			{
				result[i++] = element;
			}
		}
		return result.AsReadOnlySpan()[..i];
	}
}
