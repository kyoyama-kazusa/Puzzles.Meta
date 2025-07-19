namespace System.Numerics;

/// <summary>
/// Represents a combination generator that iterations each combination of bits for the specified number of bits, and how many 1's in it.
/// </summary>
/// <typeparam name="TInteger">The type of the target integer value.</typeparam>
/// <param name="bitCount"><inheritdoc cref="_bitCount" path="/summary"/></param>
/// <param name="oneCount"><inheritdoc cref="_oneCount" path="/summary"/></param>
[DebuggerStepThrough]
public readonly ref struct BitCombinationGenerator<TInteger>(int bitCount, int oneCount)
	where TInteger : IBinaryInteger<TInteger>
{
	/// <summary>
	/// Indicates the number of bits.
	/// </summary>
	private readonly int _bitCount = bitCount;

	/// <summary>
	/// Indicates the number of bits set <see langword="true"/>.
	/// </summary>
	private readonly int _oneCount = oneCount;


	/// <summary>
	/// Gets the enumerator of the current instance in order to use <see langword="foreach"/> loop.
	/// </summary>
	/// <returns>The enumerator instance.</returns>
	public BitCombinationEnumerator<TInteger> GetEnumerator() => new(_bitCount, _oneCount);
}
