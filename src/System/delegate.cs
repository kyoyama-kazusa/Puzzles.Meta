namespace System;

/// <summary>
/// Represents a set of method groups that can be used as delegate-typed arguments, in easy ways.
/// </summary>
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class @delegate
{
	/// <summary>
	/// Do nothing. This method is equivalent to lambda expression <c>static () => {}</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DoNothing()
	{
	}

	/// <summary>
	/// Returns an empty string equivalent to <see cref="string.Empty"/> no matter what the argument is.
	/// </summary>
	/// <typeparam name="T">The type of the argument. In fact the argument won't be used in this method.</typeparam>
	/// <param name="instance">The instance. In fact the argument won't be used in this method.</param>
	/// <returns>A string that is equal to <see cref="string.Empty"/>.</returns>
	/// <seealso cref="string.Empty"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ReturnEmptyString<T>(T instance) where T : allows ref struct => string.Empty;

	/// <summary>
	/// Merges two integers by bits. This method will be used by LINQ method
	/// <see cref="Enumerable.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})"/>.
	/// </summary>
	/// <typeparam name="TBinaryInteger">The type of the value. The value must be an integer type, and supports for shifting operators.</typeparam>
	/// <param name="interim">The interim value.</param>
	/// <param name="next">The next value to be merged by its bits.</param>
	/// <returns>The final value merged.</returns>
	/// <seealso cref="Enumerable.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TBinaryInteger BitMerger<TBinaryInteger>(TBinaryInteger interim, TBinaryInteger next)
		where TBinaryInteger : IBinaryInteger<TBinaryInteger>, IShiftOperators<TBinaryInteger, TBinaryInteger, TBinaryInteger>
		=> interim | TBinaryInteger.MultiplicativeIdentity << next;

	/// <summary>
	/// Merges two flags of type <typeparamref name="TEnum"/>.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
	/// <param name="left">The first instance to be merged.</param>
	/// <param name="right">The second instance to be merged.</param>
	/// <returns>A merged result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe TEnum EnumFlagMerger<TEnum>(TEnum left, TEnum right) where TEnum : unmanaged, Enum
		=> sizeof(TEnum) switch
		{
			1 or 2 or 4 when (Unsafe.As<TEnum, int>(ref left) | Unsafe.As<TEnum, int>(ref right)) is var f => Unsafe.As<int, TEnum>(ref f),
			8 when (Unsafe.As<TEnum, long>(ref left) | Unsafe.As<TEnum, long>(ref right)) is var f => Unsafe.As<long, TEnum>(ref f),
			_ => throw new NotSupportedException(SR.ExceptionMessage("UnderlyingTypeNotSupported"))
		};

	/// <summary>
	/// Returns the argument <paramref name="value"/>.
	/// </summary>
	/// <typeparam name="T">The type of the argument.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>The value itself.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Self<T>(T value) where T : allows ref struct => value;

	/// <summary>
	/// (Easter egg) Represents Y-Combinator. This method can allow you create recursive lambdas:
	/// <code><![CDATA[
	/// var factorial = YCombinator(static (Func<int, int> lambda) => value => value == 0 ? 1 : value * lambda(value - 1));
	/// int result = factorial(5); // 120
	/// Console.WriteLine(result);
	/// ]]></code>
	/// </summary>
	/// <typeparam name="T">The type of the argument to be passed.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="f">The recursion logic.</param>
	/// <returns>A function that creates a nesting lambda that is a recursive lambda.</returns>
	public static Func<T, TResult> YCombinator<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
		where T : allows ref struct
		where TResult : allows ref struct
		=> value => f(YCombinator(f))(value);
}
