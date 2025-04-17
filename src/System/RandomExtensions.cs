namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Random"/>.
/// </summary>
/// <seealso cref="Random"/>
public static class RandomExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Random"/>.
	/// </summary>
	extension(Random @this)
	{
		/// <summary>
		/// Generates a random number obeying Gaussian's Normal Distribution,
		/// with σ value <paramref name="sigma"/> and μ value <paramref name="mu"/>.
		/// </summary>
		/// <param name="mu">Mu μ value.</param>
		/// <param name="sigma">Sigma σ value.</param>
		/// <returns>The result value.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double NextGaussian(double mu, double sigma)
		{
			var u1 = 1D - @this.NextDouble();
			var u2 = 1D - @this.NextDouble();
			var randStdNormal = Math.Sqrt(-2D * Math.Log(u1)) * Math.Sin(Math.Tau * u2);
			return mu + sigma * randStdNormal;
		}
	}
}
