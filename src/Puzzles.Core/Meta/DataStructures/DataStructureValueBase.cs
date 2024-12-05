namespace Puzzles.Meta.DataStructures;

/// <summary>
/// Represents the value base of the data structure.
/// </summary>
public enum DataStructureValueBase : byte
{
	/// <summary>
	/// Indicates the value base is unknown.
	/// </summary>
	Unknown = 0,

	/// <summary>
	/// Indicates the value base is real value.
	/// </summary>
	Value,

	/// <summary>
	/// Indicates the value base is bit.
	/// </summary>
	Bit
}
