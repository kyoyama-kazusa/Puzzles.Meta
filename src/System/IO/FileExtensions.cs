namespace System.IO;

/// <summary>
/// Provides extension methods on <see cref="File"/>.
/// </summary>
/// <seealso cref="File"/>
public static class FileExtensions
{
	/// <summary>
	/// The field for invalid path characters as a file name.
	/// </summary>
	private static readonly SearchValues<char> InvalidCharacters = SearchValues.Create(""":\/?*<>"|""");


	/// <summary>
	/// Provides extension members on <see cref="File"/>.
	/// </summary>
	extension(File)
	{
		/// <summary>
		/// Append a new line into the file. If file doesn't exist, create a new file and append the line.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="line">The line.</param>
		public static void AppendLine(string path, string line) => File.AppendAllText(path, $"{line}{Environment.NewLine}");

		/// <summary>
		/// Determines whether the specified file name is valid.
		/// </summary>
		/// <param name="fileName">The file name to be checked.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		public static bool IsValidFileName(string fileName) => !fileName.Span.ContainsAny(InvalidCharacters);

		/// <summary>
		/// Reads the lines of file, with an option that can skip for the first line.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="skipFirstLine">Indicates whether the first line should be skipped.</param>
		/// <returns>An enumerable sequence of lines.</returns>
		public static IEnumerable<string> ReadLines(string path, bool skipFirstLine)
		{
			var firstLine = true;
			foreach (var line in File.ReadLines(path))
			{
				if (firstLine)
				{
					firstLine = false;

					if (skipFirstLine)
					{
						continue;
					}
				}

				yield return line;
			}
		}
	}
}
