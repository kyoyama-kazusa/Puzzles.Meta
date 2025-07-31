namespace System.Numerics;

/// <summary>
/// Indicates the enumerator of the current instance.
/// </summary>
/// <typeparam name="TInteger">The type of the target integer value.</typeparam>
/// <param name="bitCount">The number of bits.</param>
/// <param name="oneCount">The number of <see langword="true"/> bits.</param>
public ref struct BitCombinationEnumerator<TInteger>(int bitCount, int oneCount) : IEnumerator<TInteger>
	where TInteger : IBinaryInteger<TInteger>
{
	/// <summary>
	/// The mask.
	/// </summary>
	private readonly TInteger _mask = (TInteger.MultiplicativeIdentity << bitCount - oneCount) - TInteger.MultiplicativeIdentity;

	/// <summary>
	/// Indicates whether that the value is the last one.
	/// </summary>
	private bool _isLast = bitCount == 0;


	/// <inheritdoc cref="IEnumerator.Current"/>
	public TInteger Current { get; private set; } = (TInteger.MultiplicativeIdentity << oneCount) - TInteger.MultiplicativeIdentity;

	/// <inheritdoc/>
	readonly object IEnumerator.Current => Current;


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext()
	{
		// Check whether another combination is available.
		var result = HasNext();

		if (result && !_isLast)
		{
			// Step 1: Find the lowest set bit (the rightmost '1')
			// e.g., if Current = 0b10100, then -Current = 0b01100 (two's complement), and smallest = 0b00100,
			var smallest = Current & -Current;

			// Step 2: Add smallest to Current to "ripple" the lowest set bit to the left,
			// e.g., 0b10100 + 0b00100 = 0b11000.
			var ripple = Current + smallest;

			// Step 3: Find the bits that changed between Current and ripple i.e., where the bits got reset or flipped.
			var ones = Current ^ ripple;

			// Step 4: Right-shift by 2 to remove the flipped bit and one more,
			// then divide by smallest to reposition the remaining set bits back to the lowest possible positions.
			ones = (ones >> 2) / smallest;

			// Step 5: Combine the rippled result and repositioned ones
			// to produce the next valid combination with the same number of 1's.
			Current = ripple | ones;
		}

		return result;
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();

	/// <inheritdoc/>
	readonly void IDisposable.Dispose()
	{
	}

	/// <summary>
	/// Changes the state of the fields, and check whether the bit has another available possibility to be iterated.
	/// </summary>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	private bool HasNext()
	{
		var result = !_isLast;

		// Check whether the current value is the last possible combination.
		// Extract the lowest 1-bit (Current & -Current), and check if it's still within the valid bit range (using _mask).
		// If there's no more movable '1' in the legal mask range, we've reached the last combination.
		_isLast = (Current & -Current & _mask) == TInteger.AdditiveIdentity;

		return result;
	}
}
