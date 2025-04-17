namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="Stack{T}"/> instances.
/// </summary>
/// <seealso cref="Stack{T}"/>
public static class StackExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Stack{T}"/>.
	/// </summary>
	extension<T>(Stack<T> @this)
	{
		/// <inheritdoc cref="Enumerable.Reverse{TSource}(IEnumerable{TSource})" />
		public Stack<T> Reverse()
		{
			var result = new Stack<T>(@this.Count);
			foreach (var element in @this)
			{
				result.Push(element);
			}
			return result;
		}

		/// <summary>
		/// Returns the internal array of <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>The internal array.</returns>
		/// <remarks>
		/// <include
		///     file="../../global-doc-comments.xml"
		///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
		/// <include
		///     file="../../global-doc-comments.xml"
		///     path="//g/dotnet/version[@value='9' and @preview-value='4']/feature[@name='unsafe-accessor']"/>
		/// </remarks>
		public T[] GetInternalArray() => StackEntry<T>.GetArray(@this);

		/// <summary>
		/// Returns the internal count value of <see cref="Stack{T}"/>.
		/// </summary>
		/// <returns>The internal field <c>_size</c>.</returns>
		/// <remarks>
		/// <include
		///     file="../../global-doc-comments.xml"
		///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
		/// <include
		///     file="../../global-doc-comments.xml"
		///     path="//g/dotnet/version[@value='9' and @preview-value='4']/feature[@name='unsafe-accessor']"/>
		/// </remarks>
		public ref int GetCount() => ref StackEntry<T>.GetCount(@this);
	}
}

/// <summary>
/// Represents an entry to call internal fields on <see cref="Stack{T}"/>.
/// </summary>
/// <typeparam name="T">The type of each element in <see cref="Stack{T}"/>.</typeparam>
/// <seealso cref="Stack{T}"/>
file static class StackEntry<T>
{
	/// <summary>
	/// Try to get the backing array of a <see cref="Stack{T}"/>.
	/// </summary>
	/// <param name="this">The stack of <typeparamref name="T"/> instances.</param>
	/// <returns>The reference to field <c>_array</c>.</returns>
	/// <remarks>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='field-related-method']"/>
	/// </remarks>
	[UnsafeAccessor(UnsafeAccessorKind.Field, Name = LibraryIdentifiers.Stack_Array)]
	public static extern ref T[] GetArray(Stack<T> @this);

	/// <summary>
	/// Try to get the backing field <c>_size</c> of a <see cref="Stack{T}"/>.
	/// </summary>
	/// <param name="this">The stack of <typeparamref name="T"/> instances.</param>
	/// <returns>The reference to field <c>_size</c>.</returns>
	/// <remarks>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='field-related-method']"/>
	/// </remarks>
	[UnsafeAccessor(UnsafeAccessorKind.Field, Name = LibraryIdentifiers.List_Size)]
	public static extern ref int GetCount(Stack<T> @this);
}
