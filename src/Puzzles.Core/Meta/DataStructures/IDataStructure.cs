namespace Puzzles.Meta.DataStructures;

/// <summary>
/// Represents a data structure.
/// </summary>
public interface IDataStructure
{
	/// <summary>
	/// Indicates the type of the data structure.
	/// </summary>
	public abstract DataStructureType Type { get; }

	/// <summary>
	/// Indicates the base of the data structure.
	/// </summary>
	public abstract DataStructureBase Base { get; }

	/// <summary>
	/// Indicates the value base of the data structure.
	/// </summary>
	public virtual DataStructureValueBase ValueBase => DataStructureValueBase.Value;
}
