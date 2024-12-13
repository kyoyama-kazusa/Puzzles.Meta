namespace System.Linq;

public partial class SpanEnumerable
{
	/// <summary>
	/// Determines whether all elements are of type <typeparamref name="TDerived"/>.
	/// </summary>
	/// <typeparam name="TBase">The type of each element.</typeparam>
	/// <typeparam name="TDerived">The derived type to be checked.</typeparam>
	/// <param name="this">The collection to be used and checked.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static bool AllAre<TBase, TDerived>(this ReadOnlySpan<TBase> @this) where TDerived : class?, TBase?
	{
		foreach (ref readonly var element in @this)
		{
			if (element is not TDerived)
			{
				return false;
			}
		}
		return true;
	}
}
