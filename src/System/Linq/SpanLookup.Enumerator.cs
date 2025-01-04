namespace System.Linq;

public partial struct SpanLookup<TKey, TElement>
{
	/// <summary>
	/// Represents an enumerator type that supports iterating on each element of <see cref="SpanGrouping{TSource, TKey}"/>.
	/// </summary>
	/// <param name="_groups">The groups.</param>
	/// <seealso cref="SpanGrouping{TSource, TKey}"/>
	public ref struct Enumerator(Dictionary<TKey, TElement[]> _groups) : IEnumerator<SpanGrouping<TElement, TKey>>
	{
		/// <summary>
		/// Indicates the backing enumerator.
		/// </summary>
		private Dictionary<TKey, TElement[]>.KeyCollection.Enumerator _enumerator = _groups.Keys.GetEnumerator();


		/// <inheritdoc/>
		public readonly SpanGrouping<TElement, TKey> Current
		{
			get
			{
				var key = _enumerator.Current;
				return new(_groups[key], key);
			}
		}

		/// <inheritdoc/>
		readonly object IEnumerator.Current => Current;


		/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
		public readonly Enumerator GetEnumerator() => this;

		/// <inheritdoc/>
		public bool MoveNext() => _enumerator.MoveNext();

		/// <inheritdoc/>
		readonly void IDisposable.Dispose() { }

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotSupportedException();
	}
}
