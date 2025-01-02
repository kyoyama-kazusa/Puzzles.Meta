namespace Puzzles.Analytics;

/// <summary>
/// Represents an instance that describes the result after being analyzed.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TBoard">The type of board.</typeparam>
/// <typeparam name="TStep">The type of step.</typeparam>
public interface IAnalysisResult<TSelf, TBoard, TStep> : IEquatable<TSelf>, IEqualityOperators<TSelf, TSelf, bool>
	where TSelf : IAnalysisResult<TSelf, TBoard, TStep>
	where TBoard : IBoard, IDataStructure, allows ref struct
	where TStep : IStep<TStep>
{
	/// <summary>
	/// Indicates whether the solver has solved the puzzle.
	/// </summary>
	public abstract bool IsSolved { get; init; }

	/// <summary>
	/// Indicates the failed reason.
	/// </summary>
	public FailedReason FailedReason { get; init; }

	/// <summary>
	/// Indicates the elapsed time used during solving the puzzle. The value may not be an useful value.
	/// Some case if the puzzle doesn't contain a valid unique solution, the value may be
	/// <see cref="TimeSpan.Zero"/>.
	/// </summary>
	/// <seealso cref="TimeSpan.Zero"/>
	public abstract TimeSpan ElapsedTime { get; init; }

	/// <summary>
	/// Indicates the steps.
	/// </summary>
	public abstract ReadOnlySpan<TStep> Steps { get; }

	/// <summary>
	/// Indicates the original puzzle to be solved.
	/// </summary>
	public abstract TBoard Puzzle { get; }

	/// <summary>
	/// Indicates the unhandled exception thrown.
	/// </summary>
	public abstract Exception? UnhandledException { get; init; }


	/// <inheritdoc cref="object.ToString"/>
	public abstract string ToString();
}
