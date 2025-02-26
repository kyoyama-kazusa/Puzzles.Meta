namespace Puzzles.Meta.Transforming;

/// <summary>
/// Specifies the comparison rule of a <see cref="IBoard"/> instance.
/// </summary>
/// <seealso cref="IBoard"/>
public enum BoardComparison
{
	/// <summary>
	/// Indicates two <see cref="IBoard"/> instances compare with each other by using the default checking rule
	/// (cell by cell, bit by bit).
	/// </summary>
	Default,

	/// <summary>
	/// Indicates two <see cref="IBoard"/> instances compare with each other, including considerations on transforming cases.
	/// </summary>
	IncludingTransforms
}
