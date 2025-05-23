namespace System.Reflection;

/// <summary>
/// Provides with extension methods on <see cref="Type"/>.
/// </summary>
/// <seealso cref="Type"/>
public static class TypeExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Type"/>.
	/// </summary>
	extension(Type @this)
	{
		/// <summary>
		/// Determines whether the type has a parameterless constructor.
		/// </summary>
		public bool HasParameterlessConstructor
			=> @this.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes) is not null;


		/// <summary>
		/// Determines whether the current type can be assigned to a variable of the specified
		/// <paramref name="targetType"/>, although it is with generic parameters.
		/// </summary>
		/// <param name="targetType">The type to compare with the current type.</param>
		/// <returns>Returns <see langword="true"/> if the target type is matched, without generic constraints.</returns>
		/// <seealso href="https://stackoverflow.com/questions/74616/how-to-detect-if-type-is-another-generic-type/1075059#1075059">
		/// Question: How to detect if type is another generic type
		/// </seealso>
		public bool IsGenericAssignableTo([NotNullWhen(true)] Type? targetType)
		{
			foreach (var it in @this.GetInterfaces())
			{
				if (it.IsGenericType && it.GetGenericTypeDefinition() == targetType)
				{
					return true;
				}
			}

			if (@this.IsGenericType && @this.GetGenericTypeDefinition() == targetType)
			{
				return true;
			}

			if (@this.BaseType is not { } baseType)
			{
				return false;
			}

			return baseType.IsGenericAssignableTo(targetType);
		}
	}
}
