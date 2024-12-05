namespace Puzzles.Meta.DataStructures;

/// <summary>
/// Represents a data structure type.
/// </summary>
public enum DataStructureType
{
	/// <summary>
	/// Indicates the data structure is currently unknown.
	/// </summary>
	Unknown = 0,

	/// <summary>
	/// Represents sequence list.
	/// </summary>
	SequenceList,

	/// <summary>
	/// Represents linked list (singly-linked, doubly-linked, cyclic, etc.).
	/// </summary>
	LinkedList,

	/// <summary>
	/// Represents stack.
	/// </summary>
	Stack,

	/// <summary>
	/// Represents queue.
	/// </summary>
	Queue,

	/// <summary>
	/// Represents tree.
	/// </summary>
	Tree,

	/// <summary>
	/// Represents graph.
	/// </summary>
	Graph
}
