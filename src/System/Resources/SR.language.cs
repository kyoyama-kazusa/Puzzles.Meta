namespace System.Resources;

public partial class SR
{
	/// <summary>
	/// Determine whether the specified culture is Chinese.
	/// </summary>
	/// <param name="culture">The culture.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static bool IsChinese(CultureInfo culture)
		=> culture.Name.Span[..2].Equals(ChineseLanguage.Span[..2], StringComparison.OrdinalIgnoreCase);

	/// <summary>
	/// Determine whether the specified culture is English.
	/// </summary>
	/// <param name="culture">The culture.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static bool IsEnglish(CultureInfo culture)
		=> culture.Name.Span[..2].Equals(EnglishLanguage.Span[..2], StringComparison.OrdinalIgnoreCase);
}
