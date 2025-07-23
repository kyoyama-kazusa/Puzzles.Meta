namespace System.Reflection;

/// <summary>
/// Provides with extension methods on <see cref="Assembly"/>.
/// </summary>
/// <seealso cref="Assembly"/>
public static class AssemblyExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Assembly"/>.
	/// </summary>
	extension(Assembly @this)
	{
		/// <summary>
		/// Gets all possible types derived from an <see langword="interface"/> type,
		/// or a base <see langword="class"/> type, in the specified assembly.
		/// </summary>
		/// <param name="baseType">The type as the base type.</param>
		/// <returns>All possible derived types.</returns>
		public Type[] GetDerivedTypes(Type baseType)
			=> from type in @this.GetTypes() where type.IsAssignableTo(baseType) select type;

		/// <inheritdoc cref="GetDerivedTypes(Assembly, Type)"/>
		/// <typeparam name="TBase">The type as the base type.</typeparam>
		/// <returns><inheritdoc/></returns>
		public Type[] GetDerivedTypes<TBase>() where TBase : allows ref struct
			=> from type in @this.GetTypes() where type.IsAssignableTo(typeof(TBase)) select type;
	}
}
