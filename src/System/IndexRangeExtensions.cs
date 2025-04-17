namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Index"/> and <see cref="Range"/> instances.
/// </summary>
/// <seealso cref="Index"/>
/// <seealso cref="Range"/>
public static class IndexRangeExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Index"/>.
	/// </summary>
	extension(Index @this)
	{
		/// <include file="../../global-doc-comments.xml" path="g/csharp7/feature[@name='deconstruction-method']/target[@name='method']"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out int value, out bool isFromEnd) => (value, isFromEnd) = (@this.Value, @this.IsFromEnd);
	}

	/// <summary>
	/// Provides extension members on <see cref="Range"/>.
	/// </summary>
	extension(Range @this)
	{
		/// <include file="../../global-doc-comments.xml" path="g/csharp7/feature[@name='deconstruction-method']/target[@name='method']"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Deconstruct(out Index start, out Index end) => (start, end) = (@this.Start, @this.End);
	}
}
