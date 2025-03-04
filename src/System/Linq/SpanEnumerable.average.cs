namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IAverageMethod{TSelf, TSource}.Average{TAccumulator, TResult}()"/>
	public static TResult Average<TAccumulator, TResult>(this ReadOnlySpan<TAccumulator> @this)
		where TAccumulator : INumberBase<TAccumulator>
		where TResult : INumberBase<TResult>
	{
		var sum = @this.Sum();
		return TResult.CreateChecked(sum) / TResult.CreateChecked(@this.Length);
	}
}
