namespace System.Text.RegularExpressions;

/// <summary>
/// Provides LINQ-based extension methods on <see cref="GroupCollection"/>.
/// </summary>
/// <seealso cref="GroupCollection"/>
public static class GroupCollectionEnumerable
{
	/// <inheritdoc cref="SpanEnumerable.Select{T, TResult}(ReadOnlySpan{T}, Func{T, TResult})"/>
	public static ReadOnlySpan<TResult> Select<TResult>(this GroupCollection @this, Func<Group, TResult> selector)
	{
		var result = new TResult[@this.Count];
		var i = 0;
		foreach (Group element in @this)
		{
			result[i++] = selector(element);
		}
		return result;
	}
}
