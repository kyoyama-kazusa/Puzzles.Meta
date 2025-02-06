namespace System.Linq.Providers;

/// <summary>
/// Represents a type that supports method group <c>Shuffle</c>.
/// </summary>
/// <inheritdoc/>
public interface IShuffleMethod<TSelf, TSource> : ILinqMethod<TSelf, TSource>
	where TSelf : IShuffleMethod<TSelf, TSource>, allows ref struct
{
	/// <summary>
	/// Shuffles the whole collection.
	/// </summary>
	/// <returns>The whole collection, shuffled.</returns>
	public virtual IEnumerable<TSource> Shuffle() => Shuffle(Random.Shared);

	/// <summary>
	/// Shuffles the whole collection.
	/// </summary>
	/// <param name="random">The random number generator to be called.</param>
	/// <returns>The whole collection, shuffled.</returns>
	public virtual IEnumerable<TSource> Shuffle(Random random)
	{
		var array = this.ToArray();
		random.Shuffle(array);
		foreach (var element in array)
		{
			yield return element;
		}
	}
}
