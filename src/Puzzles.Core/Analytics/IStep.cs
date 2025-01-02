namespace Puzzles.Analytics;

/// <summary>
/// Represents a step.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
/// <typeparam name="TDifficulty">Indicates the type of difficulty.</typeparam>
public interface IStep<TSelf, TDifficulty> : IEquatable<TSelf>, IEqualityOperators<TSelf, TSelf, bool>
	where TSelf : IStep<TSelf, TDifficulty>
	where TDifficulty : INumberBase<TDifficulty>
{
	/// <summary>
	/// Indicates the difficulty rating of the instance.
	/// </summary>
	public abstract TDifficulty Difficulty { get; }


	/// <inheritdoc cref="object.GetHashCode"/>
	public abstract int GetHashCode();

	/// <inheritdoc cref="object.ToString"/>
	public abstract string ToString();

	/// <include
	///     file="../../../global-doc-comments.xml"
	///     path="/g/csharp9/feature[@name='records']/target[@name='method' and @cref='PrintMembers']"/>
	protected abstract bool PrintMembers(StringBuilder builder);
}
