namespace System.Numerics;

/// <summary>
/// Represents an enumerator that iterates an <see cref="int"/> or <see cref="uint"/> value.
/// </summary>
/// <param name="_value">The value to be iterated.</param>
[TypeImpl(
	TypeImplFlags.AllObjectMethods | TypeImplFlags.Disposable,
	OtherModifiersOnDisposableDispose = "readonly",
	ExplicitlyImplsDisposable = true)]
public ref partial struct Int32Enumerator(uint _value) : IBitEnumerator
{
	/// <inheritdoc/>
	public readonly int PopulationCount => (int)uint.PopCount(_value);

	/// <inheritdoc/>
	public readonly ReadOnlySpan<int> Bits => _value.AllSets;

	/// <inheritdoc cref="IEnumerator{T}.Current"/>
	public int Current { get; private set; } = -1;

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc/>
	public readonly int this[int index] => _value.SetAt(index);


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext()
	{
		if (_value == 0)
		{
			return false;
		}

		var mask = (uint)((int)_value & -(int)_value);
		Current = BitOperations.Log2(mask);
		_value &= ~mask;
		return true;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();
}
