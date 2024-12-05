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
	public abstract ref readonly TSelf RotateClockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance counter-clockwisely.
	/// </summary>
	/// <returns>The result rotated.</returns>
	public abstract ref readonly TSelf RotateCounterclockwise();

	/// <summary>
	/// Rotate <typeparamref name="TSelf"/> instance 180 degrees.
	/// </summary>
	/// <returns>The result rotated.</returns>
	public sealed ref readonly TSelf RotatePi() => ref RotateClockwise().RotateClockwise();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in left-right side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract ref readonly TSelf MirrorLeftRight();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in top-bottom side.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract ref readonly TSelf MirrorTopBottom();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract ref readonly TSelf MirrorDiagonal();

	/// <summary>
	/// Mirror <typeparamref name="TSelf"/> instance in anti-diagonal.
	/// </summary>
	/// <returns>The result fliped.</returns>
	public abstract ref readonly TSelf MirrorAntidiagonal();
}
