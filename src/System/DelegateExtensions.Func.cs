namespace System;

public partial class DelegateExtensions
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
		public static T SelfMethod(T instance) => instance;
	}

	/// <summary>
	/// Provides extension members on <see cref="Func{T, TResult}"/>
	/// of <see cref="Func{T, TResult}"/> and <see cref="Func{T, TResult}"/>.
	/// </summary>
	extension<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> @this)
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
		/// <returns>A function that creates a nesting lambda that is a recursive lambda.</returns>
		public Func<T, TResult> YCombinator => value => @this(get_YCombinator(@this))(value);
	}
}
