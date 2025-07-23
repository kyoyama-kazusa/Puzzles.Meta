namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
/// <seealso cref="Dictionary{TKey, TValue}"/>
public static class DictionaryExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Dictionary{TKey, TValue}"/>,
	/// where <typeparamref name="TKey"/> satisfies <see langword="notnull"/> constraint.
	/// </summary>
	extension<TKey, TValue>(Dictionary<TKey, TValue> @this) where TKey : notnull
	{
		/// <summary>
		/// Try to get the reference to the value whose corresponding key is specified one.
		/// </summary>
		/// <param name="key">The key to be checked.</param>
		/// <returns>The reference to the value; or a <see langword="null"/> reference if the key is not found.</returns>
		public ref TValue GetValueRef(in TKey key) => ref CollectionsMarshal.GetValueRefOrNullRef(@this, key);

		/// <inheritdoc cref="ToDictionaryString{TKey, TValue}(Dictionary{TKey, TValue}, Func{TKey, string?}?, Func{TValue, string?}?)"/>
		public string ToDictionaryString() => @this.ToDictionaryString(null, null);

		/// <summary>
		/// Converts the current <see cref="Dictionary{TKey, TValue}"/> instance into string representation.
		/// </summary>
		/// <param name="keyConverter">
		/// The key converter that converts <typeparamref name="TKey"/> instance into <see cref="string"/> representation.
		/// </param>
		/// <param name="valueConverter">
		/// The key converter that converts <typeparamref name="TValue"/> instance into <see cref="string"/> representation.
		/// </param>
		/// <returns>The <see cref="string"/> representation.</returns>
		public string ToDictionaryString(Func<TKey, string?>? keyConverter, Func<TValue, string?>? valueConverter)
		{
			keyConverter ??= static key => key.ToString();
			valueConverter ??= static value => value?.ToString();

			const string separator = ", ";
			var sb = new StringBuilder();
			foreach (var (key, value) in @this)
			{
				sb.Append($"{keyConverter(key)}: {valueConverter(value)}");
				sb.Append(separator);
			}

			sb.RemoveFrom(^separator.Length);
			return $"[{sb}]";
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="Dictionary{TKey, TValue}"/>, where:<br/>
	/// <typeparamref name="TKey"/> satisfies <see langword="notnull"/> constraint, <br/>
	/// <typeparamref name="TValue"/> satisfies <see cref="IEquatable{T}"/> constraint.
	/// </summary>
	extension<TKey, TValue>(Dictionary<TKey, TValue> @this)
		where TKey : notnull
		where TValue : IEquatable<TValue>
	{
		/// <summary>
		/// Try to fetch the key whose cooresponding value is the specified one.
		/// </summary>
		/// <param name="value">The value to look up.</param>
		/// <returns>The key.</returns>
		/// <exception cref="InvalidOperationException">Throws when the dictionary has no valid value.</exception>
		public TKey GetKey(TValue value)
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
}
