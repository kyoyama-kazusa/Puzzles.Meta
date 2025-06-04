namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Func{TResult}"/> and its related types.
/// </summary>
/// <seealso cref="Func{TResult}"/>
public static class FuncExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Func{T, TResult}"/>.
	/// </summary>
	extension<T>(Func<T>) where T : allows ref struct
	{
		/// <summary>
		/// Creates a <see cref="Func{T, TResult}"/> instance that directly returns parameter.
		/// </summary>
		public static Func<T, T> Self => SelfMethod;


		/// <summary>
		/// Represents a method that directly returns <paramref name="instance"/>.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T SelfMethod(T instance) => instance;
	}

	/// <summary>
	/// Provides extension members on <see cref="Func{T, TResult}"/>.
	/// </summary>
	extension<T, TResult>(Func<T, TResult>)
		where T : allows ref struct
		where TResult : allows ref struct
	{
		/// <summary>
		/// (Easter egg) Represents Y-Combinator. This method can allow you create recursive lambdas.
		/// For example, this code snippet will calculate for factorial of the specified digit:
		/// <code><![CDATA[
		/// var factorial = Func<int, int>.YCombinator(
		///     // Defines a lambda expression that is of type 'Func<int, int>'.
		///     lambda =>
		///         // Defines a parameter as input.
		///         value => value switch
		///         {
		///             // Negative value (invalid).
		///             < 0 => throw new ArgumentException("Invalid argument", nameof(value)),
		///
		///             // Recursion exit.
		///             0 or 1 => 1,
		///
		///             // Otherwise, do calculation recursively.
		///             // The core expression can use 'lambda' outside the lambda scope to invoke recursion.
		///             _ => value * lambda(value - 1)
		///         }
		/// );
		/// Console.WriteLine(factorial(5)); // 120
		/// ]]></code>
		/// </summary>
		/// <param name="f">The recursion logic.</param>
		/// <returns>A function that creates a nesting lambda that is a recursive lambda.</returns>
		public static Func<T, TResult> YCombinator(Func<Func<T, TResult>, Func<T, TResult>> f) => value => f(YCombinator(f))(value);
	}
}
