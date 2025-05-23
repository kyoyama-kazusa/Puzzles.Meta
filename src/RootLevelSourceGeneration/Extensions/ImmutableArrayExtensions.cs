namespace System.Collections.Immutable;

/// <summary>
/// Provides with extension methods on <see cref="ImmutableArray{T}"/>.
/// </summary>
/// <seealso cref="ImmutableArray{T}"/>
internal static class ImmutableArrayExtensions
{
	/// <summary>
	/// Try to get the target value of the named argument collection, whose key is equal to the specified one.
	/// </summary>
	/// <typeparam name="T">The type of the target value.</typeparam>
	/// <param name="this">The collection of named arguments.</param>
	/// <param name="name">The name to be compared.</param>
	/// <param name="resultValue">The final found result value.</param>
	/// <returns>A <see cref="bool"/> result indicating whether we can use the argument <paramref name="resultValue"/>.</returns>
	public static bool TryGetValueOrDefault<T>(
		this ImmutableArray<KeyValuePair<string, TypedConstant>> @this,
		string name,
		[NotNullWhen(true)] out T? resultValue
	)
	{
		foreach (var (key, value) in @this)
		{
			if (key == name)
			{
				resultValue = (T)value.Value!;
				return true;
			}
		}

		resultValue = default;
		return false;
	}
}
