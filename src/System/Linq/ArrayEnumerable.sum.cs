namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Sum all elements up and return the result.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array that contains a list of elements to be calculated.</param>
	/// <returns>A <typeparamref name="T"/> instance as the result.</returns>
	public static T Sum<T>(this T[] @this) where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
	{
		var result = T.AdditiveIdentity;
		foreach (ref readonly var element in @this.AsReadOnlySpan())
		{
			result += element;
		}
		return result;
	}

	/// <summary>
	/// Computes the sum of the sequence of <typeparamref name="TResult"/> values that are obtained by invoking a transform function
	/// on each element of the input sequence.
	/// </summary>
	/// <typeparam name="T">The type of element of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TResult">The type of the return value.</typeparam>
	/// <param name="source">Indicates the source values.</param>
	/// <param name="selector">The method that projects the value into an instance of type <typeparamref name="TResult"/>.</param>
	/// <returns>The result value.</returns>
	public static TResult Sum<T, TResult>(this T[] source, Func<T, TResult> selector)
		where TResult : IAdditionOperators<TResult, TResult, TResult>, IAdditiveIdentity<TResult, TResult>
	{
		var result = TResult.AdditiveIdentity;
		foreach (var element in source)
		{
			result += selector(element);
		}
		return result;
	}

	/// <inheritdoc cref="Sum{T, TResult}(T[], Func{T, TResult})"/>
	public static unsafe TResult SumUnsafe<T, TResult>(this T[] source, delegate*<T, TResult> selector)
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
