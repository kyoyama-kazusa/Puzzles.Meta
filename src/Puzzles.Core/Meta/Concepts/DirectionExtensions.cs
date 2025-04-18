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
		/// Gets an arrow text that represents the specified direction.
		/// </summary>
		/// <returns>The character.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Throws when the argument is out of range.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
