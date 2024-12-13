namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IOfTypeMethod{TSelf, TSource}.OfType{TResult}"/>
	public static ReadOnlySpan<TResult> OfType<TSource, TResult>(this ReadOnlySpan<TSource> @this)
		where TSource : class?
		where TResult : class?, TSource?
	{
		var result = new TResult[@this.Length];
		var i = 0;
		foreach (ref readonly var element in @this)
		{
			if (element is TResult derived)
			{
				result[i++] = derived;
			}
		}
		return result;
	}
}
