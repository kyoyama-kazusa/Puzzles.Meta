namespace System;

/// <summary>
/// Provides with extension methods on <see cref="ArgumentOutOfRangeException"/>.
/// </summary>
/// <seealso cref="ArgumentOutOfRangeException"/>
public static class ArgumentOutOfRangeExceptionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	extension(ArgumentOutOfRangeException)
	{
		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> if the target instance is below <paramref name="min"/>,
		/// or above or equals to <paramref name="max"/>.
		/// </summary>
		/// <typeparam name="T">The type of instance.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="min">The minimal limit.</param>
		/// <param name="max">The maximal limit.</param>
		/// <param name="variableName">The name of variable.</param>
		public static void ThrowIfOutOfRange<T>(
			in T instance,
			T min,
			T max,
			[CallerArgumentExpression(nameof(instance))] string variableName = null!
		) where T : INumber<T>
		{
			if (instance < min || instance >= max)
			{
				throw new ArgumentOutOfRangeException(variableName);
			}
		}
	}
}
