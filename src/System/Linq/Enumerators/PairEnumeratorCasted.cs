namespace System.Linq.Enumerators;

/// <inheritdoc cref="PairEnumerator{T}"/>
/// <typeparam name="T">The type of each element to be iterated.</typeparam>
/// <typeparam name="TFirst">The type of the first element in a pair.</typeparam>
/// <typeparam name="TSecond">The type of the second element in a pair.</typeparam>
/// <param name="sequence"><inheritdoc cref="_sequence" path="/summary"/></param>
public ref struct PairEnumeratorCasted<T, TFirst, TSecond>(ReadOnlySpan<T> sequence) : IEnumerator<(TFirst First, TSecond Second)>
	where T : notnull
	where TFirst : T
	where TSecond : T
{
	/// <summary>
	/// Indicates the sequence values.
	/// </summary>
	private readonly ReadOnlySpan<T> _sequence = sequence;

	/// <summary>
	/// Indicates the index.
	/// </summary>
	private int _index = -2;


	/// <inheritdoc cref="IEnumerator.Current"/>
	public readonly (TFirst First, TSecond Second) Current => ((TFirst)_sequence[_index], (TSecond)_sequence[_index + 1]);

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext() => (_index += 2) < _sequence.Length - 1;

	/// <inheritdoc cref="ReverseEnumerator{T}.GetEnumerator"/>
	public readonly PairEnumeratorCasted<T, TFirst, TSecond> GetEnumerator() => this;

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();

	/// <inheritdoc/>
	readonly void IDisposable.Dispose()
	{
	}
}
