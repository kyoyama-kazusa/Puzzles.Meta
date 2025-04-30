namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="SortedDictionary{TKey, TValue}"/>.
/// </summary>
/// <seealso cref="SortedDictionary{TKey, TValue}"/>
public static class SortedDictionaryExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="SortedDictionary{TKey, TValue}"/>,
	/// where <typeparamref name="TKey"/> satisfies <see langword="notnull"/> constraint.
	/// </summary>
	extension<TKey, TValue>(SortedDictionary<TKey, TValue> @this) where TKey : notnull
	{
		/// <summary>
		/// Indicates the minimal key of the collection.
		/// </summary>
		public TKey? MinKey
		{
			get
			{
				var result = default(TKey);
				var comparer = @this.Comparer;
				foreach (var key in @this.Keys)
				{
					if (comparer.Compare(key, result) <= 0)
					{
						result = key;
					}
				}
				return result;
			}
		}

		/// <summary>
		/// Indicates the maximal key of the collection.
		/// </summary>
		public TKey? MaxKey
		{
			get
			{
				var result = default(TKey);
				var comparer = @this.Comparer;
				foreach (var key in @this.Keys)
				{
					if (comparer.Compare(key, result) >= 0)
					{
						result = key;
					}
				}
				return result;
			}
		}
	}
}
