namespace System.Linq;

/// <summary>
/// Provides with the LINQ-related methods on type <see cref="Array"/>, especially for the one-dimensional array.
/// </summary>
/// <seealso cref="Array"/>
public static partial class ArrayEnumerable;

/// <summary>
/// This type provides a way to record LINQ methods and interfaces implemented on <typeparamref name="T"/>[].
/// This recording will be consumed by future C# <see langword="extension"/> feature
/// that may be allowed implementing interface types with some types that I cannot modify them,
/// just like <typeparamref name="T"/>[].
/// </summary>
/// <typeparam name="T">The generic type argument.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
file abstract class __ImplementedTypes<T> : IEnumerable<T>,
	IAggregateMethod<__ImplementedTypes<T>, T>,
	IAppendPrependMethod<__ImplementedTypes<T>, T>,
	ICastMethod<__ImplementedTypes<T>, T>,
	ICountMethod<__ImplementedTypes<T>, T>,
	IDistinctMethod<__ImplementedTypes<T>, T>,
	IGroupByMethod<__ImplementedTypes<T>, T>,
	IIndexMethod<__ImplementedTypes<T>, T>,
	ILeftJoinMethod<__ImplementedTypes<T>, T>,
	IJoinMethod<__ImplementedTypes<T>, T>,
	IMinMaxMethod<__ImplementedTypes<T>, T>,
	IOfTypeMethod<__ImplementedTypes<T>, T>,
	IOrderByMethod<__ImplementedTypes<T>, T>,
	IRightJoinMethod<__ImplementedTypes<T>, T>,
	ISelectMethod<__ImplementedTypes<T>, T>,
	ISelectManyMethod<__ImplementedTypes<T>, T>,
	ISumMethod<__ImplementedTypes<T>, T>,
	IWhereMethod<__ImplementedTypes<T>, T>,
	IZipMethod<__ImplementedTypes<T>, T>
	where T :
		IAdditiveIdentity<T, T>,
		IAdditionOperators<T, T, T>,
		IComparable<T>,
		IComparisonOperators<T, T, bool>,
		IMinMaxValue<T>
{
	IEnumerator IEnumerable.GetEnumerator() => throw null!;

	IEnumerator<T> IEnumerable<T>.GetEnumerator() => throw null!;
}
