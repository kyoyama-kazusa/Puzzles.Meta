namespace Puzzles.Meta;

/// <summary>
/// Represents an object that can be transformed into another kind of value.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
public interface ITransformable<TSelf> where TSelf : ITransformable<TSelf>
{
	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance clockwisely.
	/// </summary>
	/// <returns>The result rotated.</returns>
	[UnscopedRef]
	public abstract ref TSelf RotateClockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance counter-clockwisely.
	/// </summary>
	/// <returns>The result rotated.</returns>
	[UnscopedRef]
	public abstract ref TSelf RotateCounterclockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance 180 degrees.
	/// </summary>
	/// <returns>The result rotated.</returns>
	[UnscopedRef]
	public virtual ref TSelf RotatePi() => ref RotateClockwise().RotateClockwise();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in left-right side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	[UnscopedRef]
	public abstract ref TSelf MirrorLeftRight();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in top-bottom side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	[UnscopedRef]
	public abstract ref TSelf MirrorTopBottom();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	[UnscopedRef]
	public abstract ref TSelf MirrorDiagonal();

	/// <summary>
	/// Simply calls <see cref="MirrorDiagonal"/>.
	/// </summary>
	/// <returns>The result fliped.</returns>
	[UnscopedRef]
	public virtual ref TSelf Transpose() => ref MirrorDiagonal();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in anti-diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	[UnscopedRef]
	public abstract ref TSelf MirrorAntidiagonal();
}
