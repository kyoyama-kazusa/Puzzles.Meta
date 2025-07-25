namespace System;

public partial class ExceptionExtensions
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
}
