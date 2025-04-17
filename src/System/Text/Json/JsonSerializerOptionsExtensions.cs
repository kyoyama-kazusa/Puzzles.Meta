namespace System.Text.Json;

/// <summary>
/// Provides extension methods on <see cref="JsonSerializerOptions"/>.
/// </summary>
/// <seealso cref="JsonSerializerOptions"/>
public static class JsonSerializerOptionsExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="JsonSerializerOptions"/>.
	/// </summary>
	extension(JsonSerializerOptions @this)
	{
		/// <summary>
		/// Returns the converter that supports the given type, or the <typeparamref name="TConverter"/>
		/// will be used when the property <see cref="JsonSerializerOptions.Converters"/>
		/// doesn't contain any valid converters.
		/// </summary>
		/// <typeparam name="T">The type to get converter.</typeparam>
		/// <typeparam name="TConverter">
		/// The type that is the converter type to convert the instance of type <typeparamref name="T"/>.
		/// </typeparam>
		/// <returns>
		/// The converter that supports the given type, or the <typeparamref name="TConverter"/>
		/// will be used when the property <see cref="JsonSerializerOptions.Converters"/>
		/// doesn't contain any valid converters.
		/// </returns>
		/// <seealso cref="JsonSerializerOptions.Converters"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public JsonConverter<T> GetConverter<T, TConverter>() where TConverter : JsonConverter<T>, new()
			=> (JsonConverter<T>?)@this.GetConverter(typeof(T)) ?? new TConverter();
	}
}
