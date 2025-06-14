namespace System.Text.RegularExpressions;

/// <summary>
/// Provides LINQ-based extension methods on <see cref="CaptureCollection"/>.
/// </summary>
/// <seealso cref="CaptureCollection"/>
public static class CaptureCollectionEnumerable
{
	/// <inheritdoc cref="SpanEnumerable.Select{T, TResult}(ReadOnlySpan{T}, Func{T, TResult})"/>
	public static ReadOnlySpan<TResult> Select<TResult>(this CaptureCollection @this, Func<Capture, TResult> selector)
	{
		var result = new TResult[@this.Count];
		var i = 0;
		foreach (Capture element in @this)
		{
			result[i++] = selector(element);
		}
		return result;
	}
}
