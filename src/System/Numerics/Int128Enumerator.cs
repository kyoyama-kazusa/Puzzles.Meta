namespace System.Numerics;

/// <summary>
/// Represents an enumerator that iterates a <see cref="Int128"/> or <see cref="UInt128"/> value.
/// </summary>
/// <param name="_value">The value to be iterated.</param>
[TypeImpl(
	TypeImplFlags.AllObjectMethods | TypeImplFlags.Disposable,
	OtherModifiersOnDisposableDispose = "readonly",
	ExplicitlyImplsDisposable = true)]
public ref partial struct Int128Enumerator(UInt128 _value) : IBitEnumerator
{
	/// <inheritdoc/>
	public readonly int PopulationCount
	{
		get
		{
			var (upper, lower) = ((ulong)(_value >>> 64), (ulong)(_value & ulong.MaxValue));
			return BitOperations.PopCount(upper) + BitOperations.PopCount(lower);
		}
	}

	/// <inheritdoc/>
	public readonly ReadOnlySpan<int> Bits => _value.GetAllSets();

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

		var mask = (UInt128)((Int128)_value & -(Int128)_value);
		Current = (int)UInt128.Log2(mask);
		_value &= ~mask;
		return true;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();
}
