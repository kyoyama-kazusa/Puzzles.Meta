namespace System.Linq;

public partial class ArrayEnumerable
{
	/// <inheritdoc cref="Enumerable.GroupBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/>
	public static ArrayGrouping<TSource, TKey>[] GroupBy<TSource, TKey>(this TSource[] values, Func<TSource, TKey> keySelector)
		where TKey : notnull
	{
		var tempDictionary = new Dictionary<TKey, List<TSource>>(values.Length >> 2);
		foreach (var element in values)
		{
			var key = keySelector(element);
			if (!tempDictionary.TryAdd(key, [element]))
			{
				tempDictionary[key].AddRef(element);
			}
		}

		var result = new List<ArrayGrouping<TSource, TKey>>(tempDictionary.Count);
		foreach (var key in tempDictionary.Keys)
		{
			var tempValues = tempDictionary[key];
			result.Add(new([.. tempValues], key));
		}
		return [.. result];
	}

	/// <inheritdoc cref="Enumerable.GroupBy{TSource, TKey, TElement}(IEnumerable{TSource}, Func{TSource, TKey}, Func{TSource, TElement})"/>
	public static ArrayGrouping<TElement, TKey>[] GroupBy<TSource, TKey, TElement>(
		this TSource[] values,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector
	) where TKey : notnull
	{
		var tempDictionary = new Dictionary<TKey, List<TSource>>(values.Length >> 2);
		foreach (var element in values)
		{
			var key = keySelector(element);
			if (!tempDictionary.TryAdd(key, [element]))
			{
				tempDictionary[key].AddRef(element);
			}
		}

		var result = new List<ArrayGrouping<TElement, TKey>>(tempDictionary.Count);
		foreach (var key in tempDictionary.Keys)
		{
			var tempValues = tempDictionary[key];
			var valuesConverted = from value in tempValues select elementSelector(value);
			result.Add(new([.. valuesConverted], key));
		}
		return [.. result];
	}
}
