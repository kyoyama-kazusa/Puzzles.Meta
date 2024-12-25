namespace Puzzles.Analytics;

/// <summary>
/// Represents an analyzer object that will use randomization algorithm to find paths, and score them up.
/// </summary>
/// <typeparam name="TPuzzleOrGrid">The type of puzzle or grid.</typeparam>
/// <typeparam name="TPoint">The type of point.</typeparam>
/// <typeparam name="TMatch">The type of match.</typeparam>
/// <typeparam name="TCollector">The type of collector.</typeparam>
/// <typeparam name="TAnalysisResult">The type of analysis result.</typeparam>
public interface IRandomizedScoringAnalyzer<TPuzzleOrGrid, TPoint, TMatch, TCollector, TAnalysisResult>
	where TPoint : IEquatable<TPoint>
	where TCollector : ICollector<TPuzzleOrGrid, TMatch>
{
	/// <summary>
	/// Indicates the distance weight.
	/// </summary>
	public abstract double DistanceWeight { get; set; }

	/// <summary>
	/// Indicates the visual distance weight.
	/// </summary>
	public abstract double VisualDistanceWeight { get; set; }

	/// <summary>
	/// Indicates temporature factor.
	/// </summary>
	public abstract double TemporatureFactor { get; set; }

	/// <summary>
	/// Indicates the calculating distance type.
	/// </summary>
	public abstract DistanceType DistanceType { get; set; }

	/// <summary>
	/// Represents a start point creator.
	/// </summary>
	public abstract Func<TPuzzleOrGrid, TPoint> StartPointCreator { get; set; }

	/// <summary>
	/// Indicates the backing random number generator.
	/// </summary>
	protected abstract Random Random { get; }

	/// <summary>
	/// Indicates the backing collector.
	/// </summary>
	protected abstract TCollector Collector { get; }


	/// <summary>
	/// Try to analyze a puzzle, and return the steps found, encapsulated by <typeparamref name="TAnalysisResult"/>.
	/// </summary>
	/// <param name="grid">The grid to be analyzed.</param>
	/// <param name="cancellationToken">Indicates the cancellation token that can cancel the current operation.</param>
	/// <returns>An instance of type <typeparamref name="TAnalysisResult"/> indicating the result information.</returns>
	public abstract TAnalysisResult Analyze(TPuzzleOrGrid grid, CancellationToken cancellationToken = default);
}
