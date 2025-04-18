namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="Enumerable.Zip{TFirst, TSecond}(IEnumerable{TFirst}, IEnumerable{TSecond})"/>
	public static (TFirst Left, TSecond Right)[] Zip<TFirst, TSecond>(this TFirst[] first, TSecond[] second)
	{
		ArgumentException.ThrowIfAssertionFailed(first.Length == second.Length);

		var result = new (TFirst, TSecond)[first.Length];
		for (var i = 0; i < first.Length; i++)
		{
			result[i] = (first[i], second[i]);
		}
		return result;
	}
}
