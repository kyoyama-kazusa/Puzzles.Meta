namespace System.Linq.Enumerators;

/// <summary>
/// Represents an enumerator that can be used in anonymous span iteration cases.
/// </summary>
/// <typeparam name="T">The type of elements.</typeparam>
/// <param name="elements"><inheritdoc cref="_elements" path="/summary"/></param>
public ref struct AnonymousSpanEnumerator<T>(ReadOnlySpan<T> elements) : IEnumerator<T>, IEnumerable<T>
{
	/// <summary>
	/// Indicates the elements.
	/// </summary>
	private readonly ReadOnlySpan<T> _elements = elements;

	/// <summary>
	/// Indicates the index.
	/// </summary>
	private int _index = -1;


	/// <inheritdoc cref="IEnumerator{T}.Current"/>
	public readonly ref readonly T Current => ref _elements[_index];

	/// <inheritdoc/>
	readonly object? IEnumerator.Current => Current;

	/// <inheritdoc/>
	readonly T IEnumerator<T>.Current => Current;


	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	public readonly AnonymousSpanEnumerator<T> GetEnuemrator() => this;

	/// <inheritdoc/>
	public bool MoveNext() => ++_index < _elements.Length;

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();

	/// <inheritdoc/>
	readonly void IDisposable.Dispose()
	{
	}

	/// <inheritdoc/>
	readonly IEnumerator IEnumerable.GetEnumerator() => _elements.ToArray().GetEnumerator();

	/// <inheritdoc/>
	readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() => _elements.ToArray().AsEnumerable().GetEnumerator();
}
