namespace System.Threading.Tasks;

/// <summary>
/// Provides with extension methods on <see cref="TimeSpan"/>.
/// </summary>
/// <seealso cref="TimeSpan"/>
public static class TimeSpanAwaitable
{
	/// <summary>
	/// Provides extension members on <see cref="TimeSpan"/>.
	/// </summary>
	extension(TimeSpan @this)
	{
		/// <summary>
		/// Creates a <see cref="Awaiter"/> instance used for <see langword="await"/> expressions.
		/// </summary>
		/// <returns>A <see cref="Awaiter"/> instance.</returns>
		public Awaiter GetAwaiter() => new(Task.Delay(@this).GetAwaiter());
	}


	/// <summary>
	/// Represents an awaiter for <see cref="TimeSpan"/> instance.
	/// </summary>
	/// <param name="_awaiter">The base awaiter instance.</param>
	/// <seealso cref="TimeSpan"/>
	public readonly struct Awaiter(TaskAwaiter _awaiter) : INotifyCompletion
	{
		/// <inheritdoc cref="TaskAwaiter.IsCompleted"/>
		public bool IsCompleted => _awaiter.IsCompleted;


		/// <inheritdoc cref="TaskAwaiter.GetResult"/>
		public void GetResult() => _awaiter.GetResult();

		/// <inheritdoc cref="TaskAwaiter.OnCompleted(Action)"/>
		public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
	}
}
