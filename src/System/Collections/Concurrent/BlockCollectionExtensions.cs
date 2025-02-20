namespace System.Collections.Concurrent;

/// <summary>
/// Provides with extension methods on <see cref="BlockingCollection{T}"/>.
/// </summary>
/// <seealso cref="BlockingCollection{T}"/>
public static class BlockCollectionExtensions
{
	/// <summary>
	/// Execute <see cref="BlockingCollection{T}.GetConsumingEnumerable(CancellationToken)"/> in asynchronous way.
	/// </summary>
	/// <typeparam name="T">The type of result.</typeparam>
	/// <param name="collection">The current instance.</param>
	/// <param name="cancellationToken">The cancellation token that can cancel the current operation.</param>
	/// <returns>An enumerator instance that can iterate elements in asynchronous way.</returns>
	public static async IAsyncEnumerable<T> GetConsumingEnumerableAsync<T>(
		this BlockingCollection<T> collection,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		// Loop until the collection is marked as complete.
		while (!collection.IsCompleted)
		{
			T item;
			try
			{
				// Blocking call: it waits until an item is available.
				item = collection.Take(cancellationToken);
			}
			catch (InvalidOperationException)
			{
				// Collection has been marked as complete.
				yield break;
			}
			yield return item;

			// Yield control to avoid blocking the async context.
			await Task.Yield();
		}
	}
}
