namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a direction.
/// </summary>
[Flags]
public enum Direction : byte
{
	/// <summary>
	/// Indicates the placeholder of the direction.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the direction is up.
	/// </summary>
	Up = 1 << 0,

	/// <summary>
	/// Indicates the direction is down.
	/// </summary>
	Down = 1 << 1,

	/// <summary>
	/// Indicates the direction is left.
	/// </summary>
	Left = 1 << 2,

	/// <summary>
	/// Indicates the diretcion is right.
	/// </summary>
	Right = 1 << 3,

	/// <summary>
	/// Indicates the direction is up-left.
	/// </summary>
	UpLeft = 1 << 4,

	/// <summary>
	/// Indicates the direction is up-right.
	/// </summary>
	UpRight = 1 << 5,

	/// <summary>
	/// Indicates the direction is down-left.
	/// </summary>
	DownLeft = 1 << 6,

	/// <summary>
	/// Indicates the direction is down-right.
	/// </summary>
	DownRight = 1 << 7
}
