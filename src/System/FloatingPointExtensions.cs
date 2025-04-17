namespace System;

/// <summary>
/// Provides with extension methods on instances implementing <see cref="IFloatingPoint{TSelf}"/>.
/// </summary>
/// <seealso cref="IFloatingPoint{TSelf}"/>
public static class FloatingPointExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="float"/>.
	/// </summary>
	extension(float @this)
	{
		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// </summary>
		/// <param name="other">The other value.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(float other) => @this.NearlyEquals(other, float.Epsilon);

		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// If the differ of two values to compare is lower than the specified epsilon value,
		/// the method will return <see langword="true"/>.
		/// </summary>
		/// <param name="other">The other value to compare.</param>
		/// <param name="epsilon">The epsilon value (the minimal differ).</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(float other, float epsilon) => MathF.Abs(@this - other) < epsilon;
	}

	/// <summary>
	/// Provides extension members on <see cref="double"/>.
	/// </summary>
	extension(double @this)
	{
		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// </summary>
		/// <param name="other">The other value.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(double other) => @this.NearlyEquals(other, double.Epsilon);

		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// If the differ of two values to compare is lower than the specified epsilon value,
		/// the method will return <see langword="true"/>.
		/// </summary>
		/// <param name="other">The other value to compare.</param>
		/// <param name="epsilon">The epsilon value (the minimal differ).</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(double other, double epsilon) => Math.Abs(@this - other) < epsilon;
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="T"/>,
	/// where <typeparamref name="T"/> satisfies <see cref="IFloatingPoint{TSelf}"/>, <see cref="IFloatingPointIeee754{TSelf}"/> constraints.
	/// </summary>
	extension<T>(T @this) where T : IFloatingPoint<T>, IFloatingPointIeee754<T>
	{
		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// </summary>
		/// <param name="other">The other value.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(T other) => @this.NearlyEquals(other, T.Epsilon);

		/// <summary>
		/// Indicates whether the specified value is nearly equals to the current value.
		/// If the differ of two values to compare is lower than the specified epsilon value,
		/// the method will return <see langword="true"/>.
		/// </summary>
		/// <param name="other">The other value to compare.</param>
		/// <param name="epsilon">The epsilon value (the minimal differ).</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NearlyEquals(T other, T epsilon) => T.Abs(@this - other) < epsilon;
	}
}
