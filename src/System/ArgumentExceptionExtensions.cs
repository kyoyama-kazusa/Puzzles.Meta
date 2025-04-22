namespace System;

/// <summary>
/// Provides with extension methods on <see cref="ArgumentException"/> and its derived types.
/// </summary>
/// <seealso cref="ArgumentException"/>
public static class ArgumentExceptionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ArgumentException"/>.
	/// </summary>
	extension(ArgumentException)
	{
		/// <summary>
		/// Throws an <see cref="ArgumentException"/> instance if the specified assertion is failed.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="failedExpressionString">The string to the argument <paramref name="expression"/>.</param>
		public static void ThrowIfAssertionFailed(
			[DoesNotReturnIf(false)] bool expression,
			[CallerArgumentExpression(nameof(expression))] string failedExpressionString = null!
		)
		{
			if (!expression)
			{
				throw new ArgumentException($"The specified expression is failed to be checked: '{failedExpressionString}'.");
			}
		}
	}

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
