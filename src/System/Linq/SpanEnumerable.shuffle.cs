namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IShuffleMethod{TSelf, TSource}.Shuffle()"/>
	public static ReadOnlySpan<T> Shuffle<T>(this ReadOnlySpan<T> @this) => @this.Shuffle(Random.Shared);

	/// <inheritdoc cref="IShuffleMethod{TSelf, TSource}.Shuffle(Random)"/>
	public static ReadOnlySpan<T> Shuffle<T>(this ReadOnlySpan<T> @this, Random random)
	{
		var result = new T[@this.Length];
		@this.CopyTo(result);
		random.Shuffle(result);
		return result;
	}
}
