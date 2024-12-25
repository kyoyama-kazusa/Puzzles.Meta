namespace Puzzles.Analytics;

/// <summary>
/// Represents a collector instance.
/// </summary>
/// <typeparam name="TPuzzleOrGrid">The type of puzzle or grid.</typeparam>
/// <typeparam name="TMatch">The type of match.</typeparam>
public interface ICollector<TPuzzleOrGrid, TMatch>
	where TMatch : IEquatable<TMatch>, IEqualityOperators<TMatch, TMatch, bool>
{
	/// <summary>
	/// Try to find all possible steps appeared in the grid; if no steps found, an empty array will be returned.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="cancellationToken">The cancellation token that can cancel the current task.</param>
	/// <returns>All matched items.</returns>
	public abstract ReadOnlySpan<TMatch> Collect(TPuzzleOrGrid grid, CancellationToken cancellationToken = default);
}
