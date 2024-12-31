namespace Puzzles.Transforming;

/// <summary>
/// Represents an object that can swap elements of itself.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TElement">The type of each element.</typeparam>
public interface IElementSwappingTransformable<TSelf, TElement>
	where TSelf : IBoard, IEnumerable<TElement>, IElementSwappingTransformable<TSelf, TElement>
{
	/// <summary>
	/// Swaps all elements whose values are equal to <paramref name="element1"/> and <paramref name="element2"/>.
	/// </summary>
	/// <param name="element1">The first element to be swapped.</param>
	/// <param name="element2">The second element to be swapped.</param>
	/// <returns>The result swapped.</returns>
	public abstract TSelf SwapElement(TElement element1, TElement element2);

	/// <summary>
	/// Try to shuffle <typeparamref name="TSelf"/> instance of elements, keeping equality with the current instance.
	/// </summary>
	/// <returns>The result shuffled.</returns>
	public abstract TSelf Shuffle();
}
