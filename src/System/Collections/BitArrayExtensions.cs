namespace System.Collections;

/// <summary>
/// Provides extension methods on <see cref="BitArray"/>.
/// </summary>
/// <seealso cref="BitArray"/>
public static class BitArrayExtensions
{
	/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SequenceEqual(this BitArray @this, [NotNullWhen(true)] BitArray? other)
		=> other is not null && @this.Length == other.Length
		&& Entry.GetArrayField(@this).SequenceEqual(Entry.GetArrayField(other));

	/// <summary>
	/// Get the cardinality of the specified <see cref="BitArray"/>.
	/// </summary>
	/// <param name="this">The array.</param>
	/// <returns>The total number of bits set <see langword="true"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetCardinality(this BitArray @this) => Entry.GetArrayField(@this).Sum(int.PopCount);

	/// <summary>
	/// Try to get internal array field.
	/// </summary>
	/// <param name="this">The array.</param>
	/// <returns>The field.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int[] GetInternalArrayField(this BitArray @this) => Entry.GetArrayField(@this);

	/// <summary>
	/// Slices the current <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="this">The instance.</param>
	/// <param name="start">The start index.</param>
	/// <param name="count">The number.</param>
	/// <returns>The result.</returns>
	public static BitArray Slice(this BitArray @this, int start, int count)
	{
		var result = new BitArray(count);
		for (var (i, j) = (start, 0); i < start + count; i++, j++)
		{
			result[j] = @this[i];
		}
		return result;
	}

	/// <summary>
	/// Slices the current <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="this">The instance.</param>
	/// <param name="start">The start index.</param>
	/// <returns>The result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray Slice(this BitArray @this, int start) => @this.Slice(start, @this.Count - start);

	/// <summary>
	/// Performs bitwise-or operation with the other instance at the start position, without equivalent length of the other object.
	/// </summary>
	/// <param name="this">The current object.</param>
	/// <param name="other">The other object.</param>
	/// <returns>The current instance.</returns>
	public static BitArray AlignedOr(this BitArray @this, BitArray other)
	{
		if (other.Count == 0)
		{
			return @this;
		}

		var indexCount = (other.Count + 31) / 32;
		var internalBits = Entry.GetArrayField(@this);
		var otherInternalBits = Entry.GetArrayField(other);
		for (var i = 0; i < indexCount; i++)
		{
			internalBits[i] |= otherInternalBits[i];
		}
		return @this;
	}
}

/// <summary>
/// Represents an entry to call internal fields on <see cref="BitArray"/>.
/// </summary>
/// <seealso cref="BitArray"/>
file static class Entry
{
	/// <summary>
	/// Try to fetch the internal field <c>m_array</c> in type <see cref="BitArray"/>.
	/// </summary>
	/// <param name="this">The list.</param>
	/// <returns>The reference to the internal field.</returns>
	/// <remarks>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='field-related-method']"/>
	/// </remarks>
	[UnsafeAccessor(UnsafeAccessorKind.Field, Name = LibraryIdentifiers.BitArray_Array)]
	public static extern ref int[] GetArrayField(BitArray @this);
}
