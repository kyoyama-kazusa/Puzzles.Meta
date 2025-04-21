namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Environment"/>.
/// </summary>
/// <seealso cref="Environment"/>
public static class EnvironmentExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Environment"/>.
	/// </summary>
	extension(Environment)
	{
		/// <summary>
		/// Indicates the desktop path.
		/// </summary>
		public static string DesktopPath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	}
}
