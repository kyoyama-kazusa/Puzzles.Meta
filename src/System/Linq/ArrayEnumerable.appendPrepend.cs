namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="Enumerable.Append{TSource}(IEnumerable{TSource}, TSource)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ArrayAppendIterator<T> Append<T>(this T[] @this, T value) => new(@this, value);

	/// <inheritdoc cref="Enumerable.Prepend{TSource}(IEnumerable{TSource}, TSource)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ArrayPrependIterator<T> Prepend<T>(this T[] @this, T value) => new(@this, value);
}
