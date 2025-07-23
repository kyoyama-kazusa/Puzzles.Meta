namespace System.Text.RegularExpressions;

/// <summary>
/// Provides with extension methdos on <see cref="ValueMatch"/>.
/// </summary>
/// <seealso cref="ValueMatch"/>
public static class ValueMatchExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ValueMatch"/>.
	/// </summary>
	extension(ValueMatch @this)
	{
		/// <summary>
		/// Try to get the target match string at the specified position the current instance specified.
		/// </summary>
		/// <param name="originalString">The original string.</param>
		/// <returns>The target string.</returns>
		public ReadOnlySpan<char> MatchString(ReadOnlySpan<char> originalString)
			=> originalString.Slice(@this.Index, @this.Length);
	}
}
