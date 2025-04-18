namespace System.Collections.Generic;

/// <summary>
/// Provides with extension methods on <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/> instances.
/// </summary>
/// <seealso cref="IEnumerable"/>
/// <seealso cref="IEnumerable{T}"/>
public static class IEnumerableExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="IEnumerable"/>.
	/// </summary>
	extension(IEnumerable @this)
	{
		/// <summary>
		/// Indicates the length of the sequence.
		/// </summary>
		public int Length
		{
			get
			{
				if (@this is ICollection collection)
				{
					return collection.Count;
				}

				var (count, enumerator) = (0, @this.GetEnumerator());
				try
				{
					checked
					{
						while (enumerator.MoveNext())
						{
							count++;
						}
					}
					return count;
				}
				finally
				{
					if (enumerator is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
			}
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="IEnumerable{T}"/>.
	/// </summary>
	extension<T>(IEnumerable<T> @this)
	{
		/// <summary>
		/// Indicates the length of the sequence.
		/// </summary>
		public int Length => @this.Count();
	}
}
