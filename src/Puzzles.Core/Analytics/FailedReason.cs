namespace Puzzles.Analytics;

/// <summary>
/// Represents a failed reason.
/// </summary>
public enum FailedReason
{
	/// <summary>
	/// Indicates there's no failure.
	/// </summary>
	None,

	/// <summary>
	/// Indicates the puzzle is invalid.
	/// </summary>
	PuzzleInvalid,

	/// <summary>
	/// Indicates the user has cancelled the current task.
	/// </summary>
	UserCancelled,

	/// <summary>
	/// Indicates the failed reason is that the puzzle has run out of memory to be allocated.
	/// </summary>
	OutOfMemory,

	/// <summary>
	/// Indicates an unhandled exception is thrown.
	/// </summary>
	ExceptionThrown
}
