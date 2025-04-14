namespace System.Linq;

public partial class SpanEnumerable
{
	/// <summary>
	/// Returns <paramref name="this"/>[0].
	/// </summary>
	/// <typeparam name="TSource">The type of each element.</typeparam>
	/// <param name="this">The source elements.</param>
	/// <returns>The element.</returns>
	[Obsolete("Use 'span[0]' instead.", true)]
	public static TSource First<TSource>(this ReadOnlySpan<TSource> @this) => @this[0];

	/// <summary>
	/// Returns first element of <paramref name="this"/> or <see langword="default"/>(<typeparamref name="TSource"/>)
	/// if the list is empty.
	/// </summary>
	/// <typeparam name="TSource">The type of each element.</typeparam>
	/// <param name="this">The sourc elementse.</param>
	/// <returns>The element.</returns>
	[Obsolete("Use 'span is [var first, ..] ? first : default' instead.", true)]
	public static TSource? FirstOrDefault<TSource>(this ReadOnlySpan<TSource> @this) => @this is [var first, ..] ? first : default;

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.First(Func{TSource, bool})"/>
	public static TSource First<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> predicate)
	{
		foreach (var element in @this)
		{
			if (predicate(element))
			{
				return element;
			}
		}
		throw new InvalidOperationException(SR.ExceptionMessage("NoSuchElementSatisfyingCondition"));
	}

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.First(Func{TSource, bool})"/>
	public static ref readonly TSource FirstRef<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> predicate)
	{
		foreach (ref readonly var element in @this)
		{
			if (predicate(element))
			{
				return ref element;
			}
		}
		throw new InvalidOperationException(SR.ExceptionMessage("NoSuchElementSatisfyingCondition"));
	}

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.FirstOrDefault(Func{TSource, bool}, TSource)"/>
	public static T? FirstOrDefault<T>(this ReadOnlySpan<T> @this, Func<T, bool> predicate)
	{
		foreach (var element in @this)
		{
			if (predicate(element))
			{
				return element;
			}
		}
		return default;
	}

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.Last(Func{TSource, bool})"/>
	public static T Last<T>(this ReadOnlySpan<T> @this, Func<T, bool> predicate)
	{
		foreach (var element in @this.EnumerateReversely())
		{
			if (predicate(element))
			{
				return element;
			}
		}
		throw new InvalidOperationException(SR.ExceptionMessage("NoSuchElementSatisfyingCondition"));
	}

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.LastOrDefault(Func{TSource, bool})"/>
	public static T? LastOrDefault<T>(this ReadOnlySpan<T> @this, Func<T, bool> predicate)
	{
		foreach (var element in @this.EnumerateReversely())
		{
			if (predicate(element))
			{
				return element;
			}
		}
		return default;
	}
}
