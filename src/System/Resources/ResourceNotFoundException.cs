namespace System.Resources;

/// <summary>
/// Indicates an exception that will be thrown if target resource is not found.
/// </summary>
/// <param name="assembly"><inheritdoc/></param>
/// <param name="resourceKey"><inheritdoc cref="_resourceKey" path="/summary"/></param>
/// <param name="culture"><inheritdoc cref="_culture" path="/summary"/></param>
public sealed class ResourceNotFoundException(Assembly? assembly, string resourceKey, CultureInfo? culture) :
	ResourceException(assembly)
{
	/// <summary>
	/// The "unspecified" text.
	/// </summary>
	private const string CultureNotSpecifiedDefaultText = "<Unspecified>";


	/// <summary>
	/// The resource key.
	/// </summary>
	private readonly string _resourceKey = resourceKey;

	/// <summary>
	/// The culture information.
	/// </summary>
	private readonly CultureInfo? _culture = culture;


	/// <inheritdoc/>
	public override string Message
		=> string.Format(
			SR.Get("Message_ResourceNotFoundException"),
			[
				_resourceKey,
				_assembly,
				_culture?.EnglishName ?? CultureNotSpecifiedDefaultText
			]
		);

	/// <inheritdoc/>
	public override IDictionary Data
		=> new Dictionary<string, object?>
		{
			{ nameof(assembly), _assembly },
			{ nameof(resourceKey), _resourceKey },
			{ nameof(culture), _culture }
		};
}
