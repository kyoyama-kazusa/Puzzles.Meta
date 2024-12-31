namespace System.Linq;

public partial class SpanEnumerable
{
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

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.First(Func{TSource, bool})"/>
	public static ref readonly TSource FirstRef<TSource>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, bool> predicate)
	{
		foreach (ref readonly var element in @this)
		{
			if (predicate(in element))
			{
				return ref element;
			}
		}
		throw new InvalidOperationException(SR.ExceptionMessage("NoSuchElementSatisfyingCondition"));
	}

	/// <inheritdoc cref="IFirstLastMethod{TSelf, TSource}.FirstOrDefault(Func{TSource, bool})"/>
	public static ref readonly T FirstRefOrNullRef<T>(this ReadOnlySpan<T> @this, FuncRefReadOnly<T, bool> predicate)
	{
		foreach (ref readonly var element in @this)
		{
			if (predicate(in element))
			{
				return ref element;
			}
		}
		return ref Unsafe.NullRef<T>();
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
