namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ISumMethod{TSelf, TSource}.Sum"/>
	public static T Sum<T>(this ReadOnlySpan<T> @this) where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
	{
		var result = T.AdditiveIdentity;
		foreach (ref readonly var element in @this)
		{
			result += element;
		}
		return result;
	}

	/// <summary>
	/// Totals up all elements, and return the result of the sum by the specified property calculated from each element.
	/// </summary>
	/// <typeparam name="TSource">The type of the elements of source.</typeparam>
	/// <typeparam name="TKey">The type of key to add up.</typeparam>
	/// <param name="this">The collection to be used and checked.</param>
	/// <param name="keySelector">A function to extract the key for each element.</param>
	/// <returns>The value with the sum key in the sequence.</returns>
	public static TKey Sum<TSource, TKey>(this ReadOnlySpan<TSource> @this, Func<TSource, TKey> keySelector)
		where TKey : IAdditiveIdentity<TKey, TKey>, IAdditionOperators<TKey, TKey, TKey>
	{
		var result = TKey.AdditiveIdentity;
		foreach (var element in @this)
		{
			result += keySelector(element);
		}
		return result;
	}

	/// <inheritdoc cref="Sum{TSource, TKey}(ReadOnlySpan{TSource}, Func{TSource, TKey})"/>
	public static unsafe TResult SumUnsafe<T, TResult>(this ReadOnlySpan<T> source, delegate*<T, TResult> selector)
		where TResult : IAdditionOperators<TResult, TResult, TResult>, IAdditiveIdentity<TResult, TResult>
	{
		var result = TResult.AdditiveIdentity;
		foreach (var element in source)
		{
			result += selector(element);
		}
		return result;
	}
}
