namespace System.Numerics;

/// <summary>
/// Defines an enumerator type that iterates on bits of an integer of generic type.
/// </summary>
/// <typeparam name="TInteger">The type of the integer that supports for iteration on bits.</typeparam>
/// <param name="_value">The integer to be iterated.</param>
/// <param name="_bitsCount">The integer of bits to be iterated.</param>
public ref struct GenericIntegerEnumerator<TInteger>(TInteger _value, int _bitsCount) : IBitEnumerator
	where TInteger :
		IBitwiseOperators<TInteger, TInteger, TInteger>,
		IBinaryInteger<TInteger>,
		IShiftOperators<TInteger, int, TInteger>
{
	/// <inheritdoc/>
	public readonly int PopulationCount => int.CreateChecked(TInteger.PopCount(_value));

	/// <inheritdoc/>
	public readonly ReadOnlySpan<int> Bits
	{
		get
		{
			var enumerator = new GenericIntegerEnumerator<TInteger>(_value, _bitsCount);
			var result = new List<int>();
			while (enumerator.MoveNext())
			{
				result.Add(enumerator.Current);
			}
			return result.AsSpan();
		}
	}

	/// <inheritdoc cref="IEnumerator{TNumber}.Current"/>
	public int Current { get; private set; } = -1;

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc/>
	public readonly int this[int index] => Bits[index];


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext()
	{
		while (++Current < _bitsCount)
		{
			if ((_value >> Current & TInteger.One) != TInteger.Zero)
			{
				return true;
			}
		}
		return false;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();

	/// <inheritdoc/>
	readonly void IDisposable.Dispose()
	{
	}
}
