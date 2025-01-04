namespace System.Linq;

/// <summary>
/// Represents an instance that supports looking up on elements of type <typeparamref name="TElement"/>,
/// grouped by key of type <typeparamref name="TKey"/>.
/// </summary>
/// <param name="groups">Indicates the groups to be initialized.</param>
/// <typeparam name="TKey">The type of key.</typeparam>
/// <typeparam name="TElement">The type of each element.</typeparam>
/// <remarks>
/// Please note, this type only supports for <see cref="ISelectMethod{TSelf, TSource}"/>,
/// <see cref="ISelectManyMethod{TSelf, TSource}"/> and <see cref="IWhereMethod{TSelf, TSource}"/>,
/// while other LINQ methods are not supported.
/// If you want to use advanced ones, cast the object to a <see cref="ReadOnlySpan{T}"/> by calling
/// method <see cref="AsSpan"/> and try them up then.
/// </remarks>
public readonly partial struct SpanLookup<TKey, TElement>([Field] Dictionary<TKey, TElement[]> groups) :
	IEnumerable<SpanGrouping<TElement, TKey>>,
	ILookup<TKey, TElement>,
	IReadOnlyDictionary<TKey, TElement[]>,
	ISelectMethod<SpanLookup<TKey, TElement>, SpanGrouping<TElement, TKey>>,
	ISelectManyMethod<SpanLookup<TKey, TElement>, SpanGrouping<TElement, TKey>>,
	IWhereMethod<SpanLookup<TKey, TElement>, SpanGrouping<TElement, TKey>>
	where TKey : notnull
{
	/// <inheritdoc/>
	public int Count => _groups.Count;

	/// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}.Keys"/>
	[SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>")]
	public ReadOnlySpan<TKey> Keys => new List<TKey>(_groups.Keys).AsSpan();

	/// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}.Values"/>
	[SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>")]
	public ReadOnlySpan<TElement[]> Values => new List<TElement[]>(_groups.Values).AsSpan();

	/// <inheritdoc/>
	IEnumerable<TKey> IReadOnlyDictionary<TKey, TElement[]>.Keys => _groups.Keys;

	/// <inheritdoc/>
	IEnumerable<TElement[]> IReadOnlyDictionary<TKey, TElement[]>.Values => _groups.Values;


	/// <inheritdoc cref="ILookup{TKey, TElement}.this[TKey]"/>
	public ReadOnlySpan<TElement> this[TKey key] => _groups.TryGetValue(key, out var values) ? values : [];

	/// <inheritdoc/>
	TElement[] IReadOnlyDictionary<TKey, TElement[]>.this[TKey key] => _groups[key];

	/// <inheritdoc/>
	IEnumerable<TElement> ILookup<TKey, TElement>.this[TKey key] => _groups[key];


	/// <inheritdoc cref="ILookup{TKey, TElement}.Contains(TKey)"/>
	public bool ContainsKey(TKey key) => _groups.ContainsKey(key);

	/// <summary>
	/// Converts the current instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="SpanGrouping{TSource, TKey}"/> instance.
	/// </summary>
	/// <returns>An instance of type <see cref="ReadOnlySpan{T}"/> of <see cref="SpanGrouping{TSource, TKey}"/>.</returns>
	public ReadOnlySpan<SpanGrouping<TElement, TKey>> AsSpan()
	{
		var result = new List<SpanGrouping<TElement, TKey>>(_groups.Count);
		foreach (var element in this)
		{
			result.Add(element);
		}
		return result.AsSpan();
	}

	/// <summary>
	/// Converts the current instance into a <see cref="Dictionary{TKey, TValue}"/> instance.
	/// </summary>
	/// <returns>The converted <see cref="Dictionary{TKey, TValue}"/> instance.</returns>
	public Dictionary<TKey, TElement[]> AsDictionary() => new(_groups);

	/// <inheritdoc/>
	bool ILookup<TKey, TElement>.Contains(TKey key) => _groups.ContainsKey(key);


	/// <inheritdoc/>
	public bool TryGetValue(TKey key, [NotNullWhen(true)] out TElement[]? value) => _groups.TryGetValue(key, out value);

	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	public Enumerator GetEnumerator() => new(_groups);

	/// <inheritdoc cref="ISelectMethod{TSelf, TSource}.Select{TResult}(Func{TSource, TResult})"/>
	public ReadOnlySpan<TResult> Select<TResult>(Func<SpanGrouping<TElement, TKey>, TResult> selector)
	{
		var result = new List<TResult>(Count);
		foreach (var element in this)
		{
			result.Add(selector(element));
		}
		return result.AsSpan();
	}

	/// <inheritdoc cref="ISelectManyMethod{TSelf, TSource}.SelectMany{TResult}(Func{TSource, IEnumerable{TResult}})"/>
	public ReadOnlySpan<TResult> SelectMany<TResult>(Func<SpanGrouping<TElement, TKey>, ReadOnlySpan<TResult>> selector)
	{
		var result = new List<TResult>();
		foreach (var element in this)
		{
			foreach (ref readonly var nestedElement in selector(element))
			{
				result.AddRef(in nestedElement);
			}
		}
		return result.AsSpan();
	}

	/// <inheritdoc cref="ISelectManyMethod{TSelf, TSource}.SelectMany{TCollection, TResult}(Func{TSource, IEnumerable{TCollection}}, Func{TSource, TCollection, TResult})"/>
	public ReadOnlySpan<TResult> SelectMany<TCollection, TResult>(
		Func<SpanGrouping<TElement, TKey>, ReadOnlySpan<TCollection>> collectionSelector,
		Func<SpanGrouping<TElement, TKey>, TCollection, TResult> resultSelector
	)
	{
		var result = new List<TResult>();
		foreach (var element in this)
		{
			foreach (ref readonly var nestedElement in collectionSelector(element))
			{
				result.AddRef(resultSelector(element, nestedElement));
			}
		}
		return result.AsSpan();
	}

	/// <inheritdoc cref="IWhereMethod{TSelf, TSource}.Where(Func{TSource, bool})"/>
	public ReadOnlySpan<SpanGrouping<TElement, TKey>> Where(Func<SpanGrouping<TElement, TKey>, bool> predicate)
	{
		var result = new List<SpanGrouping<TElement, TKey>>(Count);
		foreach (var element in this)
		{
			if (predicate(element))
			{
				result.Add(element);
			}
		}
		return result.AsSpan();
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<IGrouping<TKey, TElement>>)this).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<IGrouping<TKey, TElement>> IEnumerable<IGrouping<TKey, TElement>>.GetEnumerator()
	{
		foreach (var element in AsSpan().ToArray())
		{
			yield return element;
		}
	}

	/// <inheritdoc/>
	IEnumerator<SpanGrouping<TElement, TKey>> IEnumerable<SpanGrouping<TElement, TKey>>.GetEnumerator()
	{
		foreach (var element in AsSpan().ToArray())
		{
			yield return element;
		}
	}

	/// <inheritdoc/>
	IEnumerator<KeyValuePair<TKey, TElement[]>> IEnumerable<KeyValuePair<TKey, TElement[]>>.GetEnumerator()
		=> _groups.GetEnumerator();
}
