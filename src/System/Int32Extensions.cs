namespace System;

/// <summary>
/// Provides with extension methods on <see cref="int"/>.
/// </summary>
/// <seealso cref="int"/>
public static class Int32Extensions
{
	/// <summary>
	/// Provides extension members on <see cref="int"/>.
	/// </summary>
	extension(int @this)
	{
		/// <summary>
		/// Creates a <see cref="TimeSpan"/> instane using the specified value as seconds.
		/// </summary>
		/// <returns>A <see cref="TimeSpan"/> instance.</returns>
		public TimeSpan Seconds => TimeSpan.FromSeconds(@this);

		/// <summary>
		/// Creates a <see cref="TimeSpan"/> instane using the specified value as milliseconds.
		/// </summary>
		/// <returns>A <see cref="TimeSpan"/> instance.</returns>
		public TimeSpan Milliseconds => TimeSpan.FromMilliseconds(@this);
	}
}
