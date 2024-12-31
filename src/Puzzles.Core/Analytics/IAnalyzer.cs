namespace Puzzles.Analytics;

/// <summary>
/// Represents an analyzer.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TResult">The type of result.</typeparam>
/// <typeparam name="TBoard">The type of the board.</typeparam>
public interface IAnalyzer<TSelf, TResult, TBoard>
	where TSelf : IAnalyzer<TSelf, TResult, TBoard>
	where TResult : IAnalysisResult<TResult, TBoard>
	where TBoard : IBoard, IDataStructure, allows ref struct
{
	/// <summary>
	/// Analyzes the puzzle of type <typeparamref name="TBoard"/>.
	/// </summary>
	/// <param name="board">The board.</param>
	/// <param name="cancellationToken">The cancellation token that can cancel the current operation.</param>
	/// <returns>An instance of type <typeparamref name="TResult"/>.</returns>
	public abstract TResult Analyze(TBoard board, CancellationToken cancellationToken = default);
}
