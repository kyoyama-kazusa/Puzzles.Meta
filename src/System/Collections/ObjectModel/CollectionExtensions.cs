namespace System.Collections.ObjectModel;

/// <summary>
/// Provides with extension methods on <see cref="Collection{T}"/>.
/// </summary>
/// <seealso cref="Collection{T}"/>
public static class CollectionExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Collection{T}"/>.
	/// </summary>
	extension<T>(Collection<T> @this)
	{
		/// <inheritdoc cref="Collection{T}.RemoveAt(int)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveAt(Index index) => @this.RemoveAt(index.GetOffset(@this.Count));
	}
}
