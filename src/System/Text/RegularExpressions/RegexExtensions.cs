namespace System.Text.RegularExpressions;

/// <summary>
/// Provides with extension methods on <see cref="Regex"/>.
/// </summary>
/// <seealso cref="Regex"/>
public static class RegexExtensions
{
	/// <summary>
	/// Find the string pattern occurred at the specified time from the specified string.
	/// </summary>
	/// <param name="this">The regular expression.</param>
	/// <param name="str">The string to be matched.</param>
	/// <param name="index">The index.</param>
	/// <returns>The index.</returns>
	public static int FindOccurenceAt(this Regex @this, ReadOnlySpan<char> str, int index)
	{
		var enumerator = @this.EnumerateMatches(str);
		var i = -1;
		while (enumerator.MoveNext())
		{
			if (++i == index)
			{
				return enumerator.Current.Index;
			}
		}
		return -1;
	}
}
