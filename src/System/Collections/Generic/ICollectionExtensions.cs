namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="ICollection{T}"/>.
/// </summary>
/// <seealso cref="ICollection{T}"/>
public static class ICollectionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ICollection{T}"/>.
	/// </summary>
	extension<T>(ICollection<T> @this)
	{
		/// <summary>
		/// Adds the elements of the specified collection to the end of the <see cref="ICollection{T}"/>.
		/// </summary>
		/// <param name="elements">The collection whose elements should be added to the end of the <see cref="ICollection{T}"/>.</param>
		public void AddRange(IEnumerable<T> elements)
		{
			foreach (var element in elements)
			{
				@this.Add(element);
			}
		}
	}
}
