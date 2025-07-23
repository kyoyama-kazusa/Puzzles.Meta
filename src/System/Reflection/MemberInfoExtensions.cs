namespace System.Reflection;

/// <summary>
/// Provides with extension methods on <see cref="MemberInfo"/> instances.
/// </summary>
/// <seealso cref="MemberInfo"/>
public static class MemberInfoExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="PropertyInfo"/>.
	/// </summary>
	extension(PropertyInfo @this)
	{
		/// <summary>
		/// When overridden in a derived class, returns the <see langword="init"/> accessor for this property.
		/// </summary>
		/// <param name="nonPublic">
		/// Indicates whether the accessor should be returned if it is non-public.
		/// <see langword="true"/> if a non-public accessor is to be returned; otherwise, <see langword="false"/>.
		/// </param>
		/// <returns>
		/// This property's <see langword="init"/> method, or <see langword="null"/>, as shown in the following table.
		/// <list type="table">
		/// <listheader>
		/// <term>Value</term>
		/// <description>Condition</description>
		/// </listheader>
		/// <item>
		/// <term>The <see langword="init"/> method for this property</term>
		/// <description>
		/// The <see langword="init"/> accessor is public, or <paramref name="nonPublic"/> is <see langword="true"/>
		/// and the <see langword="init"/> accessor is non-public.
		/// </description>
		/// </item>
		/// <item>
		/// <term><see langword="null"/></term>
		/// <description>
		/// <paramref name="nonPublic"/> is <see langword="true"/>, but the property is read-only,
		/// or <paramref name="nonPublic"/> is <see langword="false"/> and the <see langword="init"/> accessor is non-public,
		/// or there is no <see langword="init"/> accessor.
		/// </description>
		/// </item>
		/// </list>
		/// </returns>
		public MethodInfo? GetInitMethod(bool nonPublic)
			=> @this.GetSetMethod(nonPublic) switch
			{
				{ ReturnParameter: var r } i
					when Array.Exists(r.GetRequiredCustomModifiers(), static modreq => modreq == typeof(IsExternalInit)) => i,
				_ => null
			};
	}

	/// <summary>
	/// Provides extension members on <see cref="MemberInfo"/>.
	/// </summary>
	extension(MemberInfo @this)
	{
		/// <inheritdoc cref="CustomAttributeExtensions.IsDefined(MemberInfo, Type)"/>
		public bool IsDefined<TAttribute>() where TAttribute : Attribute => @this.IsDefined(typeof(TAttribute));
	}
}
