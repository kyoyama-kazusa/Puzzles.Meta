namespace System;

public partial class CharSetExtensions
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
