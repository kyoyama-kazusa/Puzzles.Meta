namespace Puzzles.Analytics;

/// <summary>
/// Represents a step.
/// </summary>
/// <typeparam name="TSelf"><include file="../../global-doc-comments.xml" path="/g/self-type-constraint"/></typeparam>
public interface IStep<TSelf> : IEquatable<TSelf>, IEqualityOperators<TSelf, TSelf, bool> where TSelf : IStep<TSelf>;
