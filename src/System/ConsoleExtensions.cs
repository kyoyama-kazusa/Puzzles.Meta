namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Console"/>.
/// </summary>
/// <seealso cref="Console"/>
public static class ConsoleExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Console"/>.
	/// </summary>
	extension(Console)
	{
		/// <summary>
		/// Show contents one by one, by clearing the screen and displaying the contents, with the specified time span as delay.
		/// </summary>
		/// <param name="delay">The delay.</param>
		/// <param name="contents">The contents.</param>
		public static async Task PlayAsync(TimeSpan delay, params string[] contents)
		{
			foreach (var content in contents)
			{
				Console.Clear();
				Console.WriteLine(content);
				await delay;
			}
		}
	}
}
