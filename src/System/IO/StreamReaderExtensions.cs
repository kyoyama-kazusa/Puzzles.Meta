namespace System.IO;

/// <summary>
/// Provides with extension methods on <see cref="StreamReader"/>.
/// </summary>
/// <seealso cref="StreamReader"/>
public static class StreamReaderExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="StreamReader"/>.
	/// </summary>
	extension(StreamReader @this)
	{
		/// <summary>
		/// Determines whether a file ends with new line character.
		/// </summary>
		/// <returns>A <see cref="bool"/> result.</returns>
		/// <exception cref="ArgumentException">Throws when the stream cannot seek.</exception>
		public bool EndsWithNewLine
		{
			get
			{
				// Determine the encoding and get bytes of new-line characters using the current encoding.
				var encoding = @this.CurrentEncoding;
				var newLine = Environment.NewLine;
				var newLineBytes = encoding.GetBytes(newLine);

				// Check whether the stream can seek.
				var stream = @this.BaseStream;
				if (!stream.CanSeek)
				{
					throw new ArgumentException("Stream must be seekable.", nameof(@this));
				}

				var originalPosition = stream.Position;
				try
				{
					// If the stream has no enough characters to be checked, just return false.
					if (stream.Length < newLineBytes.Length)
					{
						return false;
					}

					// Seek to the end.
					stream.Seek(-newLineBytes.Length, SeekOrigin.End);

					// Read the specified number of characters and compare with new-line.
					var buffer = new byte[newLineBytes.Length];
					var bytesRead = stream.Read(buffer, 0, buffer.Length);
					return bytesRead == buffer.Length && buffer.SequenceEqual(newLineBytes);
				}
				catch (ArgumentOutOfRangeException)
				{
					// Handle if '-newLineBytes.Length' returns a negative value.
					return false;
				}
				finally
				{
					// Discard buffered data.
					stream.Position = originalPosition;
					@this.DiscardBufferedData();
				}
			}
		}
	}
}
