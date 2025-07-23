namespace System.Text.Json;

/// <summary>
/// Provides extension methods on <see cref="Utf8JsonReader"/>.
/// </summary>
/// <seealso cref="Utf8JsonReader"/>
public static class Utf8JsonReaderExtensions
{
	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="Utf8JsonReader"/>.
	/// </summary>
	extension(ref Utf8JsonReader @this)
	{
		/// <summary>
		/// To read as a nested object in the JSON string stream.
		/// </summary>
		/// <typeparam name="T">The type of the instance to be deserialized.</typeparam>
		/// <param name="options">The options.</param>
		public T? GetNestedObject<T>(JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<T>(ref @this, options);
	}
}
