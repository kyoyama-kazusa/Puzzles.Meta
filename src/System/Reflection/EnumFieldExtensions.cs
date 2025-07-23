namespace System.Reflection;

/// <summary>
/// Provides with extension methods on fields on enumeration types.
/// </summary>
public static class EnumFieldExtensions
{
	/// <summary>
	/// Provides extension members on <typeparamref name="TEnum"/>,
	/// where <typeparamref name="TEnum"/> satisfies <see cref="Enum"/> constraint.
	/// </summary>
	extension<TEnum>(TEnum) where TEnum : Enum
	{
		/// <summary>
		/// Returns the field information of the specified field.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns>The <see cref="FieldInfo"/> instance.</returns>
		public static FieldInfo? FieldInfoOf(TEnum field) => typeof(TEnum).GetField(field.ToString());
	}
}
