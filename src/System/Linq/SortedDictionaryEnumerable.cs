namespace System.Linq;

/// <summary>
/// Provides extension methods on <see cref="SortedDictionary{TKey, TValue}"/>.
/// </summary>
/// <seealso cref="SortedDictionary{TKey, TValue}"/>
public static class SortedDictionaryEnumerable
{
	/// <inheritdoc cref="Enumerable.First{TSource}(IEnumerable{TSource})"/>
	public static KeyValuePair<TKey, TValue> First<TKey, TValue>(this SortedDictionary<TKey, TValue> @this) where TKey : notnull
	{
		using var enumerator = @this.GetEnumerator();
		if (enumerator.MoveNext())
		{
			return enumerator.Current;
		}
		throw new InvalidOperationException();
	}
}
