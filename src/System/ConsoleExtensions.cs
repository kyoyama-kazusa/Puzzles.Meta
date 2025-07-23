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
		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void Write<TFormattable>(TFormattable instance) where TFormattable : IFormattable, allows ref struct
			=> Console.Write(instance.ToString(null, null));

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void Write<TFormattable>(TFormattable instance, byte r, byte g, byte b)
			where TFormattable : IFormattable, allows ref struct
			=> Console.Write($"\e[38;2;{r};{g};{b}m{instance.ToString(null, null)}\e[0m");

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void Write<TFormattable>(TFormattable instance, string? format, IFormatProvider? formatProvider)
			where TFormattable : IFormattable, allows ref struct
			=> Console.Write(instance.ToString(format, formatProvider));

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void Write<TFormattable>(TFormattable instance, string? format, IFormatProvider? formatProvider, byte r, byte g, byte b)
			where TFormattable : IFormattable, allows ref struct
			=> Console.Write($"\e[38;2;{r};{g};{b}m{instance.ToString(format, formatProvider)}\e[0m");

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void WriteLine<TFormattable>(TFormattable instance) where TFormattable : IFormattable, allows ref struct
			=> Console.WriteLine(instance.ToString(null, null));

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void WriteLine<TFormattable>(TFormattable instance, byte r, byte g, byte b)
			where TFormattable : IFormattable, allows ref struct
			=> Console.WriteLine($"\e[38;2;{r};{g};{b}m{instance.ToString(null, null)}\e[0m");

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void WriteLine<TFormattable>(TFormattable instance, string? format, IFormatProvider? formatProvider)
			where TFormattable : IFormattable, allows ref struct
			=> Console.WriteLine(instance.ToString(format, formatProvider));

		/// <inheritdoc cref="Console.WriteLine(object?)"/>
		public static void WriteLine<TFormattable>(TFormattable instance, string? format, IFormatProvider? formatProvider, byte r, byte g, byte b)
			where TFormattable : IFormattable, allows ref struct
			=> Console.WriteLine($"\e[38;2;{r};{g};{b}m{instance.ToString(format, formatProvider)}\e[0m");

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
