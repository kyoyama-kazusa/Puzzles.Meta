namespace System.Collections.ObjectModel;

/// <summary>
/// Provides with extension methods on <see cref="ObservableCollection{T}"/>.
/// </summary>
/// <seealso cref="ObservableCollection{T}"/>
public static class ObservableCollectionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ObservableCollection{T}"/>.
	/// </summary>
	extension<T>(ObservableCollection<T> @this)
	{
		/// <summary>
		/// Searches for an element that matches the conditions defined by the specified predicate,
		/// and returns the zero-based index of the first occurrence within the entire <see cref="ObservableCollection{T}"/>.
		/// </summary>
		/// <param name="match"><inheritdoc cref="List{T}.FindIndex(Predicate{T})" path="/param[@name='match']"/></param>
		/// <returns><inheritdoc cref="List{T}.FindIndex(Predicate{T})" path="/returns"/></returns>
		public int FindIndex(Func<T, bool> match)
		{
			for (var i = 0; i < @this.Count; i++)
			{
				if (match(@this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Searches for an element that matches the conditions defined by the specified predicate,
		/// and returns the zero-based index of the last occurrence within the entire <see cref="ObservableCollection{T}"/>.
		/// </summary>
		/// <param name="match"><inheritdoc cref="List{T}.FindLastIndex(Predicate{T})" path="/param[@name='match']"/></param>
		/// <returns><inheritdoc cref="List{T}.FindLastIndex(Predicate{T})" path="/returns"/></returns>
		public int FindLastIndex(Func<T, bool> match)
		{
			for (var i = @this.Count - 1; i >= 0; i--)
			{
				if (match(@this[i]))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
