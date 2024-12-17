namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Applies an accumulator function over a sequence.
	/// </summary>
	/// <typeparam name="TSource">The type of each element.</typeparam>
	/// <param name="this">An array of elements to be aggregated over.</param>
	/// <param name="func">The function that aggregates the values.</param>
	/// <returns>An element accumulated, of type <typeparamref name="TSource"/>.</returns>
	public static TSource? Aggregate<TSource>(this TSource[] @this, Func<TSource?, TSource?, TSource> func)
	{
		var result = default(TSource);
		foreach (var element in @this)
		{
			result = func(result, element);
		}
		return result;
	}

	/// <summary>
	/// Applies an accumulator function over a sequence. The initial value can be set in this method.
	/// </summary>
	/// <typeparam name="TSource">The type of each element.</typeparam>
	/// <typeparam name="TAccumulate">The type of the accumulated values.</typeparam>
	/// <param name="this">An array of elements to be aggregated over.</param>
	/// <param name="seed">The initial value.</param>
	/// <param name="func">The function that aggregates the values.</param>
	/// <returns>An element accumulated, of type <typeparamref name="TSource"/>.</returns>
	public static TAccumulate Aggregate<TSource, TAccumulate>(this TSource[] @this, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
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
