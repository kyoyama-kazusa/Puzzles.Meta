namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ICastMethod{TSelf, TSource}.Cast{TResult}"/>
	public static ReadOnlySpan<TDerived> Cast<TBase, TDerived>(this ReadOnlySpan<TBase> @this)
		where TBase : class
		where TDerived : class, TBase
	{
		var result = new TDerived[@this.Length];
		var i = 0;
		foreach (ref readonly var element in @this)
		{
			result[i++] = (TDerived)element;
		}
		return result;
	}
}
