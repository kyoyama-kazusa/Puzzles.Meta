namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="IGroupByMethod{TSelf, TSource}.GroupBy{TKey}(Func{TSource, TKey})"/>
	public static ReadOnlySpan<SpanGrouping<TSource, TKey>> GroupBy<TSource, TKey>(this ReadOnlySpan<TSource> values, Func<TSource, TKey> keySelector)
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

		var result = new List<SpanGrouping<TSource, TKey>>(tempDictionary.Count);
		foreach (var key in tempDictionary.Keys)
		{
			var tempValues = tempDictionary[key];
			result.AddRef(new([.. tempValues], key));
		}
		return result.AsSpan();
	}

	/// <inheritdoc cref="IGroupByMethod{TSelf, TSource}.GroupBy{TKey, TElement}(Func{TSource, TKey}, Func{TSource, TElement})"/>
	public static ReadOnlySpan<SpanGrouping<TElement, TKey>> GroupBy<TSource, TKey, TElement>(
		this ReadOnlySpan<TSource> values,
		Func<TSource, TKey> keySelector,
		Func<TSource, TElement> elementSelector
	)
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

		var result = new List<SpanGrouping<TElement, TKey>>(tempDictionary.Count);
		foreach (var key in tempDictionary.Keys)
		{
			var tempValues = tempDictionary[key];
			var valuesConverted = from value in tempValues select elementSelector(value);
			result.AddRef(new(valuesConverted.ToArray(), key));
		}
		return result.AsSpan();
	}
}
