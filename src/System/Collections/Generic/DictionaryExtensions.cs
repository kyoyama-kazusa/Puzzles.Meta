namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
/// <seealso cref="Dictionary{TKey, TValue}"/>
public static class DictionaryExtensions
{
	/// <summary>
	/// Try to get the reference to the value whose corresponding key is specified one.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="this">The current dictionary instance.</param>
	/// <param name="key">The key to be checked.</param>
	/// <returns>The reference to the value; or a <see langword="null"/> reference if the key is not found.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref TValue GetValueRef<TKey, TValue>(this Dictionary<TKey, TValue> @this, in TKey key)
		where TKey : notnull
		=> ref CollectionsMarshal.GetValueRefOrNullRef(@this, key);

	/// <summary>
	/// Try to fetch the key whose cooresponding value is the specified one.
	/// </summary>
	/// <typeparam name="TKey">The type of key.</typeparam>
	/// <typeparam name="TValue">The type of value.</typeparam>
	/// <param name="this">The dictionary to look up.</param>
	/// <param name="value">The value to look up.</param>
	/// <returns>The key.</returns>
	/// <exception cref="InvalidOperationException">Throws when the dictionary has no valid value.</exception>
	public static TKey GetKey<TKey, TValue>(this Dictionary<TKey, TValue> @this, TValue value)
		where TKey : notnull
		where TValue : IEquatable<TValue>
	{
		foreach (var (k, v) in @this)
		{
			if (v.Equals(value))
			{
				return k;
			}
		}
		throw new InvalidOperationException();
	}
}
