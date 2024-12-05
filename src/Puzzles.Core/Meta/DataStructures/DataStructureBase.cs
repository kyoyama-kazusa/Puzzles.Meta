namespace Puzzles.Meta.DataStructures;

/// <summary>
/// Represents the base implementation of the data structure.
/// </summary>
public enum DataStructureBase : byte
{
	/// <summary>
	/// Indicates the backing data structure is neither array-based nor linked-list-based.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the backing data structure is array-based.
	/// </summary>
	ArrayBased,

	/// <summary>
	/// Indicates the backing data structure is linked-list-based.
	/// </summary>
	LinkedListBased
}
