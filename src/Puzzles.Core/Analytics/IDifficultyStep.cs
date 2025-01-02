namespace Puzzles.Analytics;

/// <summary>
/// Represents a step that requires difficulty rating.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TDifficulty">Indicates the type of difficulty.</typeparam>
public interface IDifficultyStep<TSelf, TDifficulty> : IStep<TSelf>
	where TSelf : IDifficultyStep<TSelf, TDifficulty>
	where TDifficulty : INumberBase<TDifficulty>
{
	/// <summary>
	/// Indicates the difficulty rating of the instance.
	/// </summary>
	public abstract TDifficulty Difficulty { get; }
}
