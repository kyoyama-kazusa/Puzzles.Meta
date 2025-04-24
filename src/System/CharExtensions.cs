namespace System;

/// <summary>
/// Provides with extension methods on <see cref="char"/>.
/// </summary>
/// <seealso cref="char"/>
public static class CharExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="char"/>.
	/// </summary>
	extension(char @this)
	{
		/// <summary>
		/// Indicates the upper-casing of the current character.
		/// </summary>
		public char UpperCasing => char.ToUpper(@this);

		/// <summary>
		/// Indicates the lower-casing of the current character.
		/// </summary>
		public char LowerCasing => char.ToLower(@this);
	}
}
