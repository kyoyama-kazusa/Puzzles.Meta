namespace System.Linq;

public partial class SpanEnumerable
{
	/// <inheritdoc cref="Any{TSource}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, bool})"/>
	public static bool Any<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> match)
	{
		foreach (var element in @this)
		{
			if (match(element))
			{
				return true;
			}
		}
		return false;
	}

	/// <inheritdoc cref="IAnyAllMethod{TSelf, TSource}.Any(Func{TSource, bool})"/>
	public static bool Any<TSource>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, bool> match)
	{
		foreach (ref readonly var element in @this)
		{
			if (match(in element))
			{
				return true;
			}
		}
		return false;
	}

	/// <inheritdoc cref="Any{TSource}(ReadOnlySpan{TSource}, Func{TSource, bool})"/>
	public static unsafe bool AnyUnsafe<TSource>(this ReadOnlySpan<TSource> @this, delegate*<TSource, bool> match)
	{
		foreach (var element in @this)
		{
			if (match(element))
			{
				return true;
			}
		}
		return false;
	}

	/// <inheritdoc cref="All{TSource}(ReadOnlySpan{TSource}, FuncRefReadOnly{TSource, bool})"/>
	public static bool All<TSource>(this ReadOnlySpan<TSource> @this, Func<TSource, bool> match)
	{
		foreach (var element in @this)
		{
			if (!match(element))
			{
				return false;
			}
		}
		return true;
	}

	/// <inheritdoc cref="IAnyAllMethod{TSelf, TSource}.All(Func{TSource, bool})"/>
	public static bool All<TSource>(this ReadOnlySpan<TSource> @this, FuncRefReadOnly<TSource, bool> match)
	{
		foreach (ref readonly var element in @this)
		{
			if (!match(in element))
			{
				return false;
			}
		}
		return true;
	}

	/// <inheritdoc cref="All{TSource}(ReadOnlySpan{TSource}, Func{TSource, bool})"/>
	public static unsafe bool AllUnsafe<TSource>(this ReadOnlySpan<TSource> @this, delegate*<TSource, bool> match)
	{
		foreach (var element in @this)
		{
			if (!match(element))
			{
				return false;
			}
		}
		return true;
	}
}
