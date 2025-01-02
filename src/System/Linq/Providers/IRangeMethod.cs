namespace System.Linq.Providers;

/// <summary>
/// Represents a type that supports method group <c>Range</c>.
/// </summary>
/// <inheritdoc/>
public interface IRangeMethod<TSelf, TSource> : ILinqMethod<TSelf, TSource>
	where TSelf : IRangeMethod<TSelf, TSource>, allows ref struct
	where TSource : allows ref struct
{
	/// <inheritdoc/>
	static bool ILinqMethod<TSelf, TSource>.IsExtensionMethod => false;


	/// <inheritdoc cref="Enumerable.Range(int, int)"/>
	public static abstract TSelf Range(int start, int count);
}
