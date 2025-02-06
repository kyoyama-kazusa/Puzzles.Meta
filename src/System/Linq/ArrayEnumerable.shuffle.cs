namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="IShuffleMethod{TSelf, TSource}.Shuffle()"/>
	public static T[] Shuffle<T>(this T[] @this) => @this.Shuffle(Random.Shared);

	/// <inheritdoc cref="IShuffleMethod{TSelf, TSource}.Shuffle(Random)"/>
	public static T[] Shuffle<T>(this T[] @this, Random random)
	{
		var result = @this[..];
		random.Shuffle(result);
		return result;
	}
}
