namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Action"/> and its related types.
/// </summary>
/// <seealso cref="Action"/>
public static class ActionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Action"/>.
	/// </summary>
	extension(Action)
	{
		/// <summary>
		/// Creates an <see cref="Action"/> instance that do nothing.
		/// </summary>
		public static Action DoNothing => DoNothingMethod;


		/// <summary>
		/// Represents a method that do nothing.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DoNothingMethod()
		{
		}
	}
}
