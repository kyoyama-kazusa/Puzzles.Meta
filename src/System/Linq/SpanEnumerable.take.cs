namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ITakeMethod{TSelf, TSource}.Take(int)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TSource> Take<TSource>(this ReadOnlySpan<TSource> @this, int count)
	{
		var result = new List<TSource>(count);
		result.AddRangeRef(@this[..Math.Min(count, @this.Length)]);
		return result.AsSpan();
	}

	/// <inheritdoc cref="ITakeMethod{TSelf, TSource}.Take(Range)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TSource> Take<TSource>(this ReadOnlySpan<TSource> @this, Range range)
	{
		var minIndex = range.Start.GetOffset(@this.Length);
		var maxIndex = range.End.GetOffset(@this.Length);
		if (maxIndex <= minIndex)
		{
			throw new InvalidOperationException(SR.ExceptionMessage("NoElementsFoundInCollection"));
		}

		var result = new List<TSource>(maxIndex - minIndex);
		result.AddRangeRef(@this[Math.Min(minIndex, @this.Length)..Math.Min(maxIndex, @this.Length)]);
		return result.AsSpan();
	}
}
