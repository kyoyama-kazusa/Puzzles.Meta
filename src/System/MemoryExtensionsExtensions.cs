namespace System;

/// <summary>
/// Provides with extension methods on <see cref="MemoryExtensions"/>.
/// </summary>
/// <seealso cref="MemoryExtensions"/>
public static class MemoryExtensionsExtensions
{
	/// <summary>
	/// Provides extension members of <typeparamref name="T"/>[]? instances.
	/// </summary>
	extension<T>(T[]? @this)
	{
		/// <inheritdoc cref="MemoryExtensions.AsSpan{T}(T[])"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<T> AsReadOnlySpan() => new(@this);
	}
}
