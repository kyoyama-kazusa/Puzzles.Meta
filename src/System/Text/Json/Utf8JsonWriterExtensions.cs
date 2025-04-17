namespace System.Text.Json;

/// <summary>
/// Provides extension methods on <see cref="Utf8JsonWriter"/>.
/// </summary>
/// <seealso cref="Utf8JsonWriter"/>
public static class Utf8JsonWriterExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Utf8JsonWriter"/>.
	/// </summary>
	extension(Utf8JsonWriter @this)
	{
		/// <summary>
		/// To write an object as nested one in the JSON string stream.
		/// </summary>
		/// <typeparam name="T">The type of the instance to be serialized.</typeparam>
		/// <param name="instance">The instance to be serialized.</param>
		/// <param name="options">The options.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteNestedObject<T>(T instance, JsonSerializerOptions? options = null)
			=> JsonSerializer.Serialize(@this, instance, options);

		/// <summary>
		/// To write an array of element type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type of each element.</typeparam>
		/// <param name="array">The array.</param>
		/// <param name="options">The options.</param>
		public void WriteArray<T>(T[] array, JsonSerializerOptions? options = null)
		{
			@this.WriteStartArray();
			foreach (var element in array)
			{
				switch (element)
				{
					case char c: { @this.WriteStringValue(c.ToString()); break; }
					case string s: { @this.WriteStringValue(s); break; }
					case var i and (sbyte or byte or short or ushort or int): { @this.WriteNumberValue((int)(object)i); break; }
					case uint u: { @this.WriteNumberValue(u); break; }
					case long l: { @this.WriteNumberValue(l); break; }
					case ulong u: { @this.WriteNumberValue(u); break; }
					case float f: { @this.WriteNumberValue(f); break; }
					case double d: { @this.WriteNumberValue(d); break; }
					case decimal d: { @this.WriteNumberValue(d); break; }
					default: { @this.WriteNestedObject<T>(element, options); break; }
				}
			}
			@this.WriteEndArray();
		}
	}
}
