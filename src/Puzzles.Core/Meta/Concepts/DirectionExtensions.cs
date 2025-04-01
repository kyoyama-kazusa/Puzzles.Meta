namespace Puzzles.Meta.Concepts;

/// <summary>
/// Provides with extension methods on <see cref="Direction"/>.
/// </summary>
/// <seealso cref="Direction"/>
public static class DirectionExtensions
{
	/// <summary>
	/// Gets an arrow text that represents the specified direction.
	/// </summary>
	/// <param name="this">The direction.</param>
	/// <returns>The character.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Throws when the argument is out of range.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static char GetArrow(this Direction @this)
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
