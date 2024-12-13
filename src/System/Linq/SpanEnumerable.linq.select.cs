namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ISelectMethod{TSelf, TSource}.Select{TResult}(Func{TSource, TResult})"/>
	public static ReadOnlySpan<TResult> Select<TSource, TResult>(this ReadOnlySpan<TSource> @this, Func<TSource, TResult> selector)
	{
		var result = new TResult[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			result[i++] = selector(element);
		}
		return result;
	}

	/// <inheritdoc cref="ISelectMethod{TSelf, TSource}.Select{TResult}(Func{TSource, int, TResult})"/>
	public static ReadOnlySpan<TResult> Select<TSource, TResult>(this ReadOnlySpan<TSource> @this, Func<TSource, int, TResult> selector)
	{
		var result = new TResult[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			result[i++] = selector(element, i);
		}
		return result;
	}
}
