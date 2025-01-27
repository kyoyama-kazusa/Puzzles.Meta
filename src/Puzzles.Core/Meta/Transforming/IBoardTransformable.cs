namespace Puzzles.Meta.Transforming;

/// <summary>
/// Represents an object that can be transformed into another kind of value.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
public interface IBoardTransformable<TSelf> where TSelf : IBoard, IBoardTransformable<TSelf>, allows ref struct
{
	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance clockwisely.
	/// </summary>
	/// <returns>The result rotated.</returns>
	public abstract TSelf RotateClockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance counter-clockwisely.
	/// </summary>
	/// <returns>The result rotated.</returns>
	public virtual TSelf RotateCounterclockwise() => RotateClockwise().RotateClockwise().RotateClockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance 180 degrees.
	/// </summary>
	/// <returns>The result rotated.</returns>
	public virtual TSelf RotatePi() => RotateClockwise().RotateClockwise();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in left-right side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract TSelf MirrorLeftRight();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in top-bottom side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract TSelf MirrorTopBottom();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract TSelf MirrorDiagonal();

	/// <summary>
	/// Simply calls <see cref="MirrorDiagonal"/>.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public virtual TSelf Transpose() => MirrorDiagonal();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in anti-diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract TSelf MirrorAntidiagonal();
}
