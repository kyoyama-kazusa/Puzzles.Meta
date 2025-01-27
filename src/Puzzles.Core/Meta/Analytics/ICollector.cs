namespace Puzzles.Meta.Analytics;

/// <summary>
/// Represents a collector instance.
/// </summary>
/// <typeparam name="TBoard">The type of puzzle or grid.</typeparam>
/// <typeparam name="TStep">The type of match.</typeparam>
public interface ICollector<TBoard, TStep>
	where TBoard : IBoard, allows ref struct
	where TStep : IStep<TStep>
{
	/// <summary>
	/// Try to find all possible steps appeared in the board; if no steps found, an empty array will be returned.
	/// </summary>
	/// <param name="board">The board.</param>
	/// <param name="cancellationToken">The cancellation token that can cancel the current task.</param>
	/// <returns>All matched items.</returns>
	public abstract ReadOnlySpan<TStep> Collect(TBoard board, CancellationToken cancellationToken = default);
}
