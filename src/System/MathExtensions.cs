namespace System;

/// <summary>
/// Provides extension methods on <see cref="Math"/>.
/// </summary>
/// <seealso cref="Math"/>
public static class MathExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Math"/>.
	/// </summary>
	extension(Math)
	{
		/// <summary>
		/// Get the minimal one of three values.
		/// </summary>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <param name="c">The third value.</param>
		/// <returns>The minimal one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(int a, int b, int c) => a < b ? a < c ? a : c : b < c ? b : c;

		/// <summary>
		/// Get the maximum one of three values.
		/// </summary>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <param name="c">The third value.</param>
		/// <returns>The maximum one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(int a, int b, int c) => a > b ? a > c ? a : c : b > c ? b : c;

		/// <summary>
		/// Gets the minimal one of two values.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <returns>The minimal one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Min<T>(T a, T b) where T : IComparisonOperators<T, T, bool> => a < b ? a : b;

		/// <summary>
		/// Gets the minimal one of 3 values.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <param name="c">The third value.</param>
		/// <returns>The minimal one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Min<T>(T a, T b, T c) where T : IComparisonOperators<T, T, bool>
			=> a < b ? a < c ? a : c : b < c ? b : c;

		/// <summary>
		/// Gets the maximal one of two values.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <returns>The maximal one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Max<T>(T a, T b) where T : IComparisonOperators<T, T, bool> => a > b ? a : b;

		/// <summary>
		/// Gets the maximal one of 3 values.
		/// </summary>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		/// <param name="c">The third value.</param>
		/// <returns>The maximal one.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Max<T>(T a, T b, T c) where T : IComparisonOperators<T, T, bool>
			=> a > b ? a > c ? a : c : b > c ? b : c;
	}
}
