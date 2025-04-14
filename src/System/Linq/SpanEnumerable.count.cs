namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ICountMethod{TSelf, TSource}.Count(Func{TSource, bool})"/>
	public static int Count<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> condition)
	{
		var result = 0;
		foreach (var element in @this)
		{
			if (condition(element))
			{
				result++;
			}
		}
		return result;
	}
}
