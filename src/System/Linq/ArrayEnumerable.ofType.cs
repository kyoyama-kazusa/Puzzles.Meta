namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Filters the array, removing elements not of type <typeparamref name="TResult"/>.
	/// </summary>
	/// <typeparam name="TResult">The type of the target elements.</typeparam>
	/// <param name="this">The array to be filtered.</param>
	/// <returns>A list of <typeparamref name="TResult"/> elements.</returns>
	public static TResult[] OfType<TResult>(this object[] @this)
		=> from element in @this where element is TResult select (TResult)element;
}
