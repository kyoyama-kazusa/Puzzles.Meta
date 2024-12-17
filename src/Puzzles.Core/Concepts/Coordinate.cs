namespace Puzzles.Concepts;

/// <summary>
/// Represents a coordinate.
/// </summary>
/// <param name="X">Indicates the row index.</param>
/// <param name="Y">Indicates the column index.</param>
[TypeImpl(TypeImplFlags.ComparisonOperators)]
public readonly partial record struct Coordinate(int X, int Y) :
	IComparable<Coordinate>,
	IComparisonOperators<Coordinate, Coordinate, bool>,
	IEqualityOperators<Coordinate, Coordinate, bool>,
	ISubtractionOperators<Coordinate, Coordinate, Direction>
{
	/// <summary>
	/// Indicates the left cell.
	/// </summary>
	public Coordinate Up => new(X - 1, Y);

	/// <summary>
	/// Indicates the right cell.
	/// </summary>
	public Coordinate Down => new(X + 1, Y);

	/// <summary>
	/// Indicates the up cell.
	/// </summary>
	public Coordinate Left => new(X, Y - 1);

	/// <summary>
	/// Indicates the down cell.
	/// </summary>
	public Coordinate Right => new(X, Y + 1);


	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="/g/csharp9/feature[@name='records']/target[@name='method' and @cref='PrintMembers']"/>
	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(X)} = {X}, {nameof(Y)} = {Y}");
		return true;
	}


	/// <inheritdoc/>
	public int CompareTo(Coordinate other)
	{
		if (X.CompareTo(other.X) is var r1 and not 0)
		{
			return r1;
		}
		if (Y.CompareTo(other.Y) is var r2 and not 0)
		{
			return r2;
		}
		return 0;
	}


	/// <inheritdoc/>
	/// <exception cref="InvalidOperationException">
	/// Throws when the two coordinates has a gap between them, or they cannot see each other in their own direction.
	/// </exception>
	public static Direction operator -(Coordinate left, Coordinate right)
	{
		if (left == right)
		{
			return Direction.None;
		}
		else if (left.X - right.X == -1 && left.Y == right.Y)
		{
			return Direction.Up;
		}
		else if (left.X - right.X == 1 && left.Y == right.Y)
		{
			return Direction.Down;
		}
		else if (left.X == right.X && left.Y - right.Y == -1)
		{
			return Direction.Left;
		}
		else if (left.X == right.X && left.Y - right.Y == 1)
		{
			return Direction.Right;
		}
		throw new InvalidOperationException();
	}

	/// <summary>
	/// Moves the coordinate one step forward to the next coordinate by the specified direction.
	/// </summary>
	/// <param name="coordinate">The coordinate.</param>
	/// <param name="arrow">The direction.</param>
	/// <returns>The new coordinate.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when the argument <paramref name="arrow"/> is out of range.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Coordinate operator >>(Coordinate coordinate, char arrow)
		=> arrow switch
		{
			'↑' => coordinate.Up,
			'↓' => coordinate.Down,
			'←' => coordinate.Left,
			'→' => coordinate.Right,
			_ => throw new ArgumentOutOfRangeException(nameof(arrow))
		};

	/// <summary>
	/// Moves the coordinate one step forward to the next coordinate by the specified direction.
	/// </summary>
	/// <param name="coordinate">The coordinate.</param>
	/// <param name="direction">The direction.</param>
	/// <returns>The new coordinate.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when the argument <paramref name="direction"/> is out of range.
	/// </exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Coordinate operator >>(Coordinate coordinate, Direction direction) => coordinate >> direction.GetArrow();
}
