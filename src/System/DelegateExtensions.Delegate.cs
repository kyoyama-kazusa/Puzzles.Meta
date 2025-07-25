namespace System;

public partial class DelegateExtensions
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
		public static TDelegate[] GetInvocations<TDelegate>(TDelegate @delegate) where TDelegate : Delegate
			=> from TDelegate element in @delegate.GetInvocationList() select element;

		/// <inheritdoc cref="Delegate.EnumerateInvocationList{TDelegate}(TDelegate)"/>
		public static DelegateEnumerator<TDelegate> GetEnumerator<TDelegate>(TDelegate? @delegate) where TDelegate : Delegate
			=> new(@delegate);
	}
}
