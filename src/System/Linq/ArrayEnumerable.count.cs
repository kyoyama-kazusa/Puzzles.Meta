namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Totals up the number of elements that satisfy the specified condition.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array.</param>
	/// <param name="predicate">The condition.</param>
	/// <returns>The number of elements satisfying the specified condition.</returns>
	public static int Count<T>(this T[] @this, Func<T, bool> predicate)
	{
		var result = 0;
		foreach (var element in @this)
		{
			if (predicate(element))
			{
				result++;
			}
		}
		return result;
	}

	/// <inheritdoc cref="Count{T}(T[], Func{T, bool})"/>
	public static unsafe int CountUnsafe<T>(this T[] @this, delegate*<T, bool> predicate)
	{
		var result = 0;
		foreach (var element in @this)
		{
			if (predicate(element))
			{
				result++;
			}
		}
		return result;
	}
}
