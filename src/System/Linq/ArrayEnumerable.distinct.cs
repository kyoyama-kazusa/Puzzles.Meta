namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <summary>
	/// Filters duplicate items from an array.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array to be filtered.</param>
	/// <returns>A new array of elements that doesn't contain any duplicate items.</returns>
	public static T[] Distinct<T>(this T[] @this)
	{
		if (@this.Length == 0 || ReferenceEquals(@this, Array.Empty<T>()))
		{
			return [];
		}

		var tempSet = new HashSet<T>(@this.Length, EqualityComparer<T>.Default);
		var result = new T[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			if (tempSet.Add(element))
			{
				result[i++] = element;
			}
		}
		return result[..i];
	}

	/// <inheritdoc cref="Enumerable.DistinctBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static T[] DistinctBy<T, TKey>(this T[] @this, Func<T, TKey> keySelector)
		where TKey : notnull, IEqualityOperators<TKey, TKey, bool>
	{
		var result = new T[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			if (i == 0)
			{
				result[i++] = element;
			}
			else
			{
				var elementKey = keySelector(element);
				var contains = false;
				foreach (var recordedElement in result)
				{
					var recordedElementKey = keySelector(recordedElement);
					if (elementKey == recordedElementKey)
					{
						contains = true;
						break;
					}
				}
				if (!contains)
				{
					result[i++] = element;
				}
			}
		}
		return result[..i];
	}

	/// <inheritdoc cref="Enumerable.DistinctBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IEqualityComparer{TKey})"/>
	public static T[] DistinctBy<T, TKey>(this T[] @this, Func<T, TKey> keySelector, IEqualityComparer<TKey> equalityComparer)
		where TKey : notnull
	{
		var result = new T[@this.Length];
		var i = 0;
		foreach (var element in @this)
		{
			if (i == 0)
			{
				result[i++] = element;
			}
			else
			{
				var elementKey = keySelector(element);
				var hashCodeThis = equalityComparer.GetHashCode(elementKey);

				var contains = false;
				foreach (ref readonly var recordedElement in result.AsReadOnlySpan()[..i])
				{
					var recordedElementKey = keySelector(recordedElement);
					var hashCodeOther = equalityComparer.GetHashCode(recordedElementKey);
					if (hashCodeThis == hashCodeOther && equalityComparer.Equals(elementKey, recordedElementKey))
					{
						contains = true;
						break;
					}
				}
				if (!contains)
				{
					result[i++] = element;
				}
			}
		}
		return result[..i];
	}
}
