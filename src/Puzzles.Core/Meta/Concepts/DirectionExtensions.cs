namespace Puzzles.Meta.Concepts;

/// <summary>
/// Provides with extension methods on <see cref="Direction"/>.
/// </summary>
/// <seealso cref="Direction"/>
public static class DirectionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Direction"/>.
	/// </summary>
	extension(Direction @this)
	{
		/// <summary>
		/// Indicates the reversed direction.
		/// </summary>
		public Direction ReversedDirection
			=> @this switch
			{
				Direction.Up => Direction.Down,
				Direction.Down => Direction.Up,
				Direction.Left => Direction.Right,
				Direction.Right => Direction.Left,
				Direction.UpLeft => Direction.DownRight,
				Direction.UpRight => Direction.DownLeft,
				Direction.DownLeft => Direction.UpRight,
				Direction.DownRight => Direction.UpLeft,
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};


		/// <summary>
		/// Gets an arrow text that represents the specified direction.
		/// </summary>
		/// <returns>The character.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Throws when the argument is out of range.</exception>
		public char GetArrow()
			=> @this switch
			{
				Direction.Up => '↑',
				Direction.Down => '↓',
				Direction.Left => '←',
				Direction.Right => '→',
				Direction.UpLeft => '↖',
				Direction.UpRight => '↗',
				Direction.DownLeft => '↙',
				Direction.DownRight => '↘',
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}
