namespace System;

/// <summary>
/// Provides with extension methods on instances implementing <see cref="IFloatingPoint{TSelf}"/>.
/// </summary>
/// <seealso cref="IFloatingPoint{TSelf}"/>
public static class FloatingPointExtensions
{
	/// <summary>
	/// Indicates whether the specified value is nearly equals to the current value.
	/// </summary>
	/// <typeparam name="T">The type of floating point.</typeparam>
	/// <param name="this">The value.</param>
	/// <param name="other">The other value.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool NearlyEquals<T>(this T @this, T other) where T : IFloatingPoint<T>, IFloatingPointIeee754<T>
		=> @this.NearlyEquals(other, T.Epsilon);

	/// <summary>
	/// Indicates whether the specified value is nearly equals to the current value.
	/// If the differ of two values to compare is lower than the specified epsilon value,
	/// the method will return <see langword="true"/>.
	/// </summary>
	/// <typeparam name="T">The type of floating point.</typeparam>
	/// <param name="this">The value.</param>
	/// <param name="other">The other value to compare.</param>
	/// <param name="epsilon">The epsilon value (the minimal differ).</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool NearlyEquals<T>(this T @this, T other, T epsilon) where T : IFloatingPoint<T>, IFloatingPointIeee754<T>
		=> T.Abs(@this - other) < epsilon;
}
