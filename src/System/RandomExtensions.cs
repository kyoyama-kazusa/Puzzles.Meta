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

		/// <summary>
		/// Try to get one element from the collection.
		/// </summary>
		/// <typeparam name="T">The type of each element.</typeparam>
		/// <param name="values">The values.</param>
		/// <returns>The chosen element.</returns>
		/// <exception cref="InvalidOperationException">Throws when the specified collection is empty.</exception>
		public T Choose<T>(ReadOnlySpan<T> values) => @this.Choose(values, 1)[0];

		/// <summary>
		/// Try to get a list of elements from the collection.
		/// </summary>
		/// <typeparam name="T">The type of each element.</typeparam>
		/// <param name="values">The values.</param>
		/// <param name="count">The desired number of elements to get.</param>
		/// <returns>The chosen elements.</returns>
		/// <exception cref="InvalidOperationException">
		/// Throws when the specified collection doesn't contain enough elements to get.
		/// </exception>
		public ReadOnlySpan<T> Choose<T>(ReadOnlySpan<T> values, int count)
		{
			InvalidOperationException.ThrowIfAssertionFailed(values.Length >= count);
			if (values.Length == count)
			{
				return values;
			}

			var result = new T[count];
			if (count <= values.Length >> 1)
			{
				var bitArray = new BitArray(values.Length);
				for (var i = 0; i < count; i++)
				{
					var index = @this.Next(0, values.Length);
					if (bitArray[index])
					{
						// Fallback.
						do
						{
							index = (index + 1) % values.Length;
						} while (bitArray[index]);
					}
					result[i] = values[index];
					bitArray[i] = true;
				}
			}
			else
			{
				var sequence = (stackalloc int[values.Length]);
				for (var i = 0; i < values.Length; i++)
				{
					sequence[i] = i;
				}
				@this.Shuffle(sequence);

				for (var i = 0; i < count; i++)
				{
					result[i] = values[sequence[i]];
				}
			}
			return result;
		}
	}
}
