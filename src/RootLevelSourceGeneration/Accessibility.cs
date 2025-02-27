/// <summary>
/// Defines an accessibility kind.
/// </summary>
internal enum Accessibility
{
	None,

	/// <summary>
	/// Indicates the accessibility is <see langword="file"/>-scoped.
	/// </summary>
	File,

	/// <summary>
	/// Indicates the accessibility is <see langword="private"/>.
	/// </summary>
	Private,

	/// <summary>
	/// Indicates the accessibility is <see langword="protected"/>.
	/// </summary>
	Protected,

	/// <summary>
	/// Indicates the accessibility is <see langword="private protected"/>.
	/// </summary>
	PrivateProtected,

	/// <summary>
	/// Indicates the accessibility is <see langword="internal"/>.
	/// </summary>
	Internal,

	/// <summary>
	/// Indicates the accessibility is <see langword="protected internal"/>.
	/// </summary>
	ProtectedInternal,

	/// <summary>
	/// Indicates the accessibility is <see langword="public"/>.
	/// </summary>
	Public
}
