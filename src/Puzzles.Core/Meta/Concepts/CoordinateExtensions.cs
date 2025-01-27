namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a coordinate.
/// </summary>
public static class CoordinateExtensions
{
	/// <summary>
	/// Indicates whether the coordinate is out of bound.
	/// </summary>
	/// <typeparam name="TBoard">The type of the board.</typeparam>
	/// <param name="this">The coordinate.</param>
	/// <param name="grid">The grid.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static bool IsOutOfBound<TBoard>(this Coordinate @this, TBoard grid) where TBoard : IBoard, allows ref struct
	{
		var rows = grid.Rows;
		var columns = grid.Columns;
		return @this.X < 0 || @this.X >= rows || @this.Y < 0 || @this.Y >= columns;
	}

	/// <summary>
	/// Converts the current coordinate into an absolute index.
	/// </summary>
	/// <typeparam name="TBoard">The type of the board.</typeparam>
	/// <param name="this">The coordinate.</param>
	/// <param name="grid">The grid.</param>
	/// <returns>The absolute index.</returns>
	public static int ToIndex<TBoard>(this Coordinate @this, TBoard grid) where TBoard : IBoard, allows ref struct
		=> @this.X * grid.Columns + @this.Y;
}
