namespace Puzzles.Meta.DataStructures;

/// <summary>
/// Represents a data structure type.
/// </summary>
[Flags]
public enum DataStructureType
{
	/// <summary>
	/// Indicates the type doesn't use a specific data structure.
	/// </summary>
	None = 0,

	/// <summary>
	/// Represents array (a sequence of fixed length).
	/// </summary>
	Array = 1 << 0,

	/// <summary>
	/// Represents sequence list.
	/// </summary>
	SequenceList = 1 << 1,

	/// <summary>
	/// Represents linked list (singly-linked, doubly-linked, cyclic, etc.).
	/// </summary>
	LinkedList = 1 << 2,

	/// <summary>
	/// Represents stack.
	/// </summary>
	Stack = 1 << 3,

	/// <summary>
	/// Represents queue.
	/// </summary>
	Queue = 1 << 4,

	/// <summary>
	/// Represents set.
	/// </summary>
	Set = 1 << 5,

	/// <summary>
	/// Represents hash table.
	/// </summary>
	HashTable = 1 << 6,

	/// <summary>
	/// Represents tree.
	/// </summary>
	Tree = 1 << 7,

	/// <summary>
	/// Represents graph.
	/// </summary>
	Graph = 1 << 8
}
