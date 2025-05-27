namespace System;

/// <summary>
/// Provides extension methods on <see cref="Delegate"/>.
/// </summary>
/// <seealso cref="Delegate"/>
public static class DelegateExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Delegate"/>.
	/// </summary>
	extension(Delegate)
	{
		/// <summary>
		/// Returns the invocation list of the delegate.
		/// </summary>
		/// <typeparam name="TDelegate">The type of the delegate.</typeparam>
		/// <param name="delegate">The instance.</param>
		/// <returns>An array of delegates representing the invocation list of the current delegate.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TDelegate[] GetInvocations<TDelegate>(TDelegate @delegate) where TDelegate : Delegate
			=> from TDelegate element in @delegate.GetInvocationList() select element;

		/// <inheritdoc cref="Delegate.EnumerateInvocationList{TDelegate}(TDelegate)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DelegateEnumerator<TDelegate> GetEnumerator<TDelegate>(TDelegate? @delegate) where TDelegate : Delegate
			=> new(@delegate);
	}

	/// <summary>
	/// Provides extension members on <see cref="Action"/>.
	/// </summary>
	extension(Action)
	{
		/// <summary>
		/// Creates an <see cref="Action"/> instance that do nothing.
		/// </summary>
		public static Action DoNothing => DoNothingMethod;


		/// <summary>
		/// Represents a method that do nothing.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DoNothingMethod()
		{
		}
	}

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
}
