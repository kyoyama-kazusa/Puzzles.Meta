namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Returns a new array instance that contains each element with its corresponding index.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The object to be iterated.</param>
	/// <returns>A new array instance.</returns>
	public static (int Index, T Value)[] Index<T>(this T[] @this)
	{
		var result = new (int, T)[@this.Length];
		for (var i = 0; i < @this.Length; i++)
		{
			result[i] = (i, @this[i]);
		}
		return result;
	}
}
