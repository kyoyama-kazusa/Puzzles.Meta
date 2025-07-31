namespace System;

/// <summary>
/// Provides with extension methods on generic type.
/// </summary>
public static class GenericExtensions
{
	/// <summary>
	/// Provides extension members on <typeparamref name="T"/>,
	/// where <typeparamref name="T"/> satisfies <see langword="struct"/> constraint.
	/// </summary>
	extension<T>(T) where T : struct, allows ref struct
	{
		/// <summary>
		/// Represents null reference of the type <typeparamref name="T"/>.
		/// </summary>
		[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
		public static ref T nullref => ref Unsafe.NullRef<T>();
	}
}
