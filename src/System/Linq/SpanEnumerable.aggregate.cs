namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IAggregateMethod{TSelf, TSource}.Aggregate(Func{TSource, TSource, TSource})"/>
	public static TSource Aggregate<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, TSource, TSource> func)
	{
		var result = default(TSource)!;
		foreach (var element in @this)
		{
			result = func(result, element);
		}
		return result;
	}

	/// <inheritdoc cref="IAggregateMethod{TSelf, TSource}.Aggregate{TAccumulate, TResult}(TAccumulate, Func{TAccumulate, TSource, TAccumulate}, Func{TAccumulate, TResult})"/>
	public static TAccumulate Aggregate<TSource, TAccumulate>(
		this ReadOnlySpan<TSource> @this,
		TAccumulate seed,
		Func<TAccumulate, TSource, TAccumulate> func
	)
		where TAccumulate : allows ref struct
	{
		var result = seed;
		foreach (var element in @this)
		{
			result = func(result, element);
		}
		return result;
	}
}
