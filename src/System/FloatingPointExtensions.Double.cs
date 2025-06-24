namespace System;

public partial class FloatingPointExtensions
{
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
}
