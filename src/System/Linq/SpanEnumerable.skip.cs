namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="ISkipMethod{TSelf, TSource}.Skip(int)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<TSource> Skip<TSource>(this ReadOnlySpan<TSource> @this, int count) => @this[count..];
}
