namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="LinkedList{T}"/>.
/// </summary>
/// <seealso cref="LinkedList{T}"/>
public static class LinkedListExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="LinkedList{T}"/>.
	/// </summary>
	extension<T>(LinkedList<T> @this)
	{
		/// <summary>
		/// Indicates the value of the first node.
		/// </summary>
		/// <exception cref="NullReferenceException">Throws when the list is empty.</exception>
		public T FirstValue => @this.First!.Value;

		/// <summary>
		/// Indicates the value of the last node.
		/// </summary>
		/// <exception cref="NullReferenceException">Throws when the list is empty.</exception>
		public T LastValue => @this.Last!.Value;


		/// <summary>
		/// Checks whether the collection exists at least one element satisfying the specified condition.
		/// </summary>
		/// <param name="match">A method to be called.</param>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		public bool Exists(Func<T, bool> match)
		{
			foreach (var element in @this)
			{
				if (match(element))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes the first element and return that element.
		/// </summary>
		/// <returns>The first element to be removed.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T RemoveFirstNode()
		{
			var node = @this.First!.Value;
			@this.RemoveFirst();
			return node;
		}

		/// <summary>
		/// Removes the last element and return that element.
		/// </summary>
		/// <returns>The last element to be removed.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T RemoveLastNode()
		{
			var node = @this.Last!.Value;
			@this.RemoveLast();
			return node;
		}
	}
}
