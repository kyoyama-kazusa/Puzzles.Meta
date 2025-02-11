namespace System.Linq;

public partial class SpanEnumerable
{
	/// <summary>
	/// Create a range of variables that starts with 0, incrementing values and putting them into the other positions.
	/// </summary>
	/// <param name="count">The number of elements created.</param>
	/// <returns>The result sequence [0, 1, 2, 3, ..].</returns>
	public static ReadOnlySpan<int> Range(int count) => Range(0, count, static previous => previous + 1);

	/// <inheritdoc cref="IRangeMethod{TSelf, TSource}.Range(int, int)"/>
	public static ReadOnlySpan<int> Range(int start, int count) => Range(start, count, static previous => previous + 1);

	/// <summary>
	/// Create a range of variables that starts with <see cref="INumberBase{TSelf}.Zero"/> from type <typeparamref name="TInteger"/>,
	/// incrementing values and putting them into the other positions.
	/// </summary>
	/// <typeparam name="TInteger">The type of integer.</typeparam>
	/// <param name="count">The number of elements created.</param>
	/// <returns>The result sequence [0, 1, 2, 3, ..].</returns>
	public static ReadOnlySpan<TInteger> Range<TInteger>(int count) where TInteger : IBinaryInteger<TInteger>
	{
		var one = TInteger.CreateChecked(1);
		return Range(TInteger.Zero, count, previous => previous + one);
	}

	/// <summary>
	/// Create a range of variables that starts with the specified value, and iterates the value to create followed values.
	/// </summary>
	/// <typeparam name="T">The type of the target value.</typeparam>
	/// <param name="start">The start value.</param>
	/// <param name="count">The number of elements created.</param>
	/// <param name="iterator">The creator method that create a value from the previous value.</param>
	/// <returns>The result sequence.</returns>
	public static ReadOnlySpan<T> Range<T>(T start, int count, Func<T, T> iterator)
	{
		var result = new T[count];
		result[0] = start;

		for (var i = 1; i < count; i++)
		{
			result[i] = iterator(result[i - 1]);
		}
		return result;
	}
}
