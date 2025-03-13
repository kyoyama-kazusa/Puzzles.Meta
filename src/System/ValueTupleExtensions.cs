namespace System;

/// <summary>
/// Represents with extension methods for value tuple type set.
/// </summary>
public static class ValueTupleExtensions
{
	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3, T4}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T, T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T, T, T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T, T, T, T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTupleEnumerator<T> GetEnumerator<T>(this in (T, T, T, T, T, T, T) @this) => new(@this);

	/// <summary>
	/// Gets an <see cref="ValueTupleEnumerator{T}"/> instance that can iterate for a pair of values
	/// via a value tuple <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> of a uniform type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The uniform type that two instances defined in pair are.</typeparam>
	/// <typeparam name="TRest">The type that encapsulates a list of rest elements.</typeparam>
	/// <param name="this">The instance to be iterated.</param>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ComplexValueTupleEnumerator<T, TRest> GetEnumerator<T, TRest>(this in ValueTuple<T, T, T, T, T, T, T, TRest> @this)
		where TRest : struct => new(@this);

	/// <summary>
	/// Casts the current instance into a <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The instance.</param>
	/// <returns>The <see cref="ReadOnlySpan{T}"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T) @this) => (T[])[@this.Item1, @this.Item2];

	/// <inheritdoc cref="AsSpan{T}(in ValueTuple{T, T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T, T) @this) => (T[])[@this.Item1, @this.Item2, @this.Item3];

	/// <inheritdoc cref="AsSpan{T}(in ValueTuple{T, T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T, T, T) @this)
		=> (T[])[@this.Item1, @this.Item2, @this.Item3, @this.Item4];

	/// <inheritdoc cref="AsSpan{T}(in ValueTuple{T, T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T, T, T, T) @this)
		=> (T[])[@this.Item1, @this.Item2, @this.Item3, @this.Item4, @this.Item5];

	/// <inheritdoc cref="AsSpan{T}(in ValueTuple{T, T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T, T, T, T, T) @this)
		=> (T[])[@this.Item1, @this.Item2, @this.Item3, @this.Item4, @this.Item5, @this.Item6];

	/// <inheritdoc cref="AsSpan{T}(in ValueTuple{T, T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsSpan<T>(this scoped in (T, T, T, T, T, T, T) @this)
		=> (T[])[@this.Item1, @this.Item2, @this.Item3, @this.Item4, @this.Item5, @this.Item6, @this.Item7];

	/// <summary>
	/// Casts the current instance into a <see cref="ReadOnlySpan{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <typeparam name="TRest">The type of rest elements.</typeparam>
	/// <param name="this">The instance.</param>
	/// <returns>The <see cref="ReadOnlySpan{T}"/> instance.</returns>
	public static ReadOnlySpan<T> AsSpan<T, TRest>(this scoped in ValueTuple<T, T, T, T, T, T, T, TRest> @this)
		where TRest : struct
	{
		var result = new List<T>();
		foreach (ref readonly var element in @this)
		{
			result.AddRef(element);
		}
		return result.AsSpan();
	}
}
