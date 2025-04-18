namespace Puzzles.Meta.Concepts;

/// <summary>
/// Represents a coordinate.
/// </summary>
public static class CoordinateExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Coordinate"/>.
	/// </summary>
	extension(Coordinate @this)
	{
		/// <summary>
		/// Indicates whether the coordinate is out of bound.
		/// </summary>
		/// <typeparam name="TBoard">The type of the board.</typeparam>
		/// <param name="grid">The grid.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		public bool IsOutOfBound<TBoard>(TBoard grid) where TBoard : IBoard, allows ref struct
		{
			var rows = grid.Rows;
			var columns = grid.Columns;
			return @this.X < 0 || @this.X >= rows || @this.Y < 0 || @this.Y >= columns;
		}

		/// <summary>
		/// Converts the current coordinate into an absolute index.
		/// </summary>
		/// <typeparam name="TBoard">The type of the board.</typeparam>
		/// <param name="grid">The grid.</param>
		/// <returns>The absolute index.</returns>
		public int ToIndex<TBoard>(TBoard grid) where TBoard : IBoard, allows ref struct
			=> @this.X * grid.Columns + @this.Y;
	}
}
