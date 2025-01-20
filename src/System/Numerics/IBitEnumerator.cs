namespace System.Numerics;

/// <summary>
/// Represents an enumerator that can iterate bits on numeric type.
/// </summary>
public interface IBitEnumerator : IEnumerator<int>
{
	/// <summary>
	/// Indicates the population count of the value.
	/// </summary>
	public abstract int PopulationCount { get; }

	/// <summary>
	/// Indicates the bits set.
	/// </summary>
	public abstract ReadOnlySpan<int> Bits { get; }

	/// <inheritdoc/>
	object IEnumerator.Current => Current;


	/// <inheritdoc cref="BitOperationsExtensions.SetAt(uint, int)"/>
	public abstract int this[int index] { get; }


	/// <inheritdoc/>
	[DoesNotReturn]
	void IEnumerator.Reset() => throw new NotImplementedException();
}
