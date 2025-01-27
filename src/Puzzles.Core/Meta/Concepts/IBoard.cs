namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a game board.
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
