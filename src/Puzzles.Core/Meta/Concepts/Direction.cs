namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a direction.
/// </summary>
public enum Direction : byte
{
	/// <summary>
	/// Indicates the placeholder of the direction.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the direction is up.
	/// </summary>
	Up,

	/// <summary>
	/// Indicates the direction is down.
	/// </summary>
	Down,

	/// <summary>
	/// Indicates the direction is left.
	/// </summary>
	Left,

	/// <summary>
	/// Indicates the diretcion is right.
	/// </summary>
	Right,

	/// <summary>
	/// Indicates the direction is up-left.
	/// </summary>
	UpLeft,

	/// <summary>
	/// Indicates the direction is up-right.
	/// </summary>
	UpRight,

	/// <summary>
	/// Indicates the direction is down-left.
	/// </summary>
	DownLeft,

	/// <summary>
	/// Indicates the direction is down-right.
	/// </summary>
	DownRight
}
