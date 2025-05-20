namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates the marked member will participate to-string operation.
/// </summary>
/// <param name="displayName"><inheritdoc cref="DisplayName" path="/summary"/></param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, Inherited = false)]
public sealed class StringMemberAttribute(string? displayName = null) : ComponentMemberAttribute
{
	/// <summary>
	/// Indicates the display name of the field or property to be changed.
	/// By default the value is <see langword="null"/>, which means no conversion will be existing here.
	/// </summary>
	public string? DisplayName { get; } = displayName;
}
