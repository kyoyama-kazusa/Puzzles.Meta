namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a game board. The board must be a rectangular or squared one, with a regular definition of "row" and "column".
/// </summary>
public interface IBoard
{
	/// <summary>
	/// Indicates the number of rows.
	/// </summary>
	public abstract int Rows { get; }

	/// <summary>
	/// Indicates the number of columns.
	/// </summary>
	public abstract int Columns { get; }
}
