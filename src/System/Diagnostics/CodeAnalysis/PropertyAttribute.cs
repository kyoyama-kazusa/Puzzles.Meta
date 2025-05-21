namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Indicates the backing generated member of this primary constructor is a property.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
public sealed class PropertyAttribute : ParameterTargetAttribute;
