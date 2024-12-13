namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="Enumerable.Index{TSource}(IEnumerable{TSource})"/>
	public static ReadOnlySpan<(int Index, T Value)> Index<T>(this ReadOnlySpan<T> @this)
	{
		var result = new (int, T)[@this.Length];
		for (var i = 0; i < @this.Length; i++)
		{
			result[i] = (i, @this[i]);
		}
		return result;
	}
}
