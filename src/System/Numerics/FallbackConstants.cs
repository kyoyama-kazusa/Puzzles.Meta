namespace System.Numerics;

/// <summary>
/// Provides with fallback constants that will be used for checking target values,
/// e.g. method <see cref="BitOperations.TrailingZeroCount(int)"/>.
/// </summary>
/// <seealso cref="BitOperations.TrailingZeroCount(int)"/>
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class FallbackConstants
{
	/// <summary>
	/// Indicates the fallback value is for <see cref="int"/> and <see cref="uint"/> type.
	/// </summary>
	public const int @int = 32;

	/// <summary>
	/// Indicates the fallback value is for <see cref="long"/> and <see cref="ulong"/> type.
	/// </summary>
	public const int @long = 64;
}
