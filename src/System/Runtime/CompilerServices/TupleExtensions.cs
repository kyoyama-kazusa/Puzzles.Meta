namespace System.Runtime.CompilerServices;

/// <summary>
/// Provides with extension methods on <see cref="ITuple"/>.
/// </summary>
/// <seealso cref="ITuple"/>
public static class TupleExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ITuple"/>.
	/// </summary>
	extension(ITuple @this)
	{
		/// <summary>
		/// Converts the tuple elements into a valid span of elements of type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The unified type for all elements.</typeparam>
		/// <returns>A <see cref="ReadOnlySpan{T}"/> instance.</returns>
		public ReadOnlySpan<T> AsSpan<T>()
		{
			var result = new T[@this.Length];
			var i = 0;
			foreach (var element in @this)
			{
				result[i++] = (T)element!;
			}
			return result;
		}
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TTuple"/>,
	/// where <typeparamref name="TTuple"/> satisfies <see cref="ITuple"/> constraint.
	/// </summary>
	extension<TTuple>(TTuple @this) where TTuple : ITuple, allows ref struct
	{
		/// <summary>
		/// Converts the <see cref="ITuple"/> instance into an array of objects.
		/// </summary>
		/// <returns>The array of elements.</returns>
		public object?[] ToArray()
		{
			var result = new object?[@this.Length];
			var i = 0;
			foreach (var element in @this)
			{
				result[i++] = element;
			}
			return result;
		}

		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TupleEnumerator<TTuple> GetEnumerator() => new(@this);
	}
}
