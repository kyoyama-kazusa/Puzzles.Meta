namespace Microsoft.CodeAnalysis;

/// <summary>
/// Provides extension methods for <see cref="INamedTypeSymbol"/>.
/// </summary>
/// <seealso cref="INamedTypeSymbol"/>
internal static class INamedTypeSymbolExtensions
{
	/// <summary>
	/// Determines whether the current type is derived from the specified type (a <see langword="class"/>, not <see langword="interface"/>).
	/// </summary>
	/// <param name="this">The current type.</param>
	/// <param name="baseType">The base type to be checked.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	public static bool IsDerivedFrom(this INamedTypeSymbol @this, INamedTypeSymbol baseType)
	{
		for (var temp = @this.BaseType; temp is not null; temp = temp.BaseType)
		{
			if (SymbolEqualityComparer.Default.Equals(temp, baseType))
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Gets all possible members in a type, even including its base type members.
	/// </summary>
	/// <param name="this">The type symbol.</param>
	/// <param name="enumerateInterfaces">Indicates whether this method enumerates interface members.</param>
	/// <returns>All members.</returns>
	public static IEnumerable<ISymbol> GetAllMembers(this INamedTypeSymbol @this, bool enumerateInterfaces = false)
	{
		for (var current = @this; current is not null; current = current.BaseType)
		{
			foreach (var member in current.GetMembers())
			{
				yield return member;
			}
		}

		if (enumerateInterfaces)
		{
			foreach (var interfaceType in @this.AllInterfaces)
			{
				foreach (var member in interfaceType.GetMembers())
				{
					yield return member;
				}
			}
		}
	}
}
