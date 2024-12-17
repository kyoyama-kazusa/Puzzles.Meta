namespace System;

/// <summary>
/// Provides with extension methods on <see cref="Array"/>, especially for one-dimensional array.
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayExtensions
{
	/// <summary>
	/// Initializes an array, using the specified method to initialize each element.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="array">The array.</param>
	/// <param name="initializer">The initializer callback method.</param>
	public static void InitializeArray<T>(this T?[] array, ArrayInitializer<T> initializer)
	{
		foreach (ref var element in array.AsSpan())
		{
			initializer(ref element);
		}
	}

	/// <inheritdoc cref="SequenceEqual{T}(T[], T[], Func{T, T, bool})"/>
	public static bool SequenceEqual<T>(this T[] @this, T[] other) where T : IEqualityOperators<T, T, bool>
	{
		if (@this.Length != other.Length)
		{
			return false;
		}

		for (var i = 0; i < @this.Length; i++)
		{
			if (@this[i] != other[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Compares elements from two arrays one by one respectively.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The array to be compared.</param>
	/// <param name="other">The other array to be compared.</param>
	/// <param name="equalityComparer">
	/// A method that compares two <typeparamref name="T"/> elements, and returns a <see cref="bool"/> result
	/// indicating whether two elements are considered equal.
	/// </param>
	/// <returns>A <see cref="bool"/> result indicating whether two arrays are considered equal.</returns>
	public static bool SequenceEqual<T>(this T[] @this, T[] other, Func<T, T, bool> equalityComparer)
	{
		if (@this.Length != other.Length)
		{
			return false;
		}

		for (var i = 0; i < @this.Length; i++)
		{
			if (!equalityComparer(@this[i], other[i]))
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// Flats the specified 2D array into an 1D array.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">An array of elements of type <typeparamref name="T"/>.</param>
	/// <returns>An 1D array.</returns>
	public static T[] Flat<T>(this T[,] @this)
	{
		var result = new T[@this.Length];
		for (var i = 0; i < @this.GetLength(0); i++)
		{
			for (var (j, l2) = (0, @this.GetLength(1)); j < l2; j++)
			{
				result[i * l2 + j] = @this[i, j];
			}
		}
		return result;
	}

	/// <summary>
	/// Converts an array into a <see cref="string"/>.
	/// </summary>
	/// <typeparam name="T">The type of each element inside array.</typeparam>
	/// <param name="this">The array.</param>
	/// <returns>The string representation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[] @this) => @this.ToArrayString(null);

	/// <summary>
	/// Converts an array into a <see cref="string"/>, using the specified formatter method
	/// that can convert an instance of type <typeparamref name="T"/> into a <see cref="string"/> representation.
	/// </summary>
	/// <typeparam name="T">The type of each element inside array.</typeparam>
	/// <param name="this">The array.</param>
	/// <param name="valueConverter">The value converter method.</param>
	/// <returns>The string representation.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[] @this, Func<T, string?>? valueConverter)
	{
		valueConverter ??= (static value => value?.ToString());
		return $"[{string.Join(", ", from element in @this select valueConverter(element))}]";
	}

	/// <inheritdoc cref="ToArrayString{T}(T[])"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[OverloadResolutionPriority(1)]
	public static string ToArrayString<T>(this T[][] @this) => @this.ToArrayString(null);

	/// <inheritdoc cref="ToArrayString{T}(T[], Func{T, string?})"/>
	[OverloadResolutionPriority(1)]
	public static string ToArrayString<T>(this T[][] @this, Func<T, string?>? valueConverter)
	{
		var sb = new StringBuilder();
		sb.Append('[').AppendLine();
		for (var i = 0; i < @this.Length; i++)
		{
			var element = @this[i];
			sb.Append("  ").Append(element.ToArrayString(valueConverter));
			if (i != @this.Length - 1)
			{
				sb.Append(',');
			}
			sb.AppendLine();
		}
		sb.Append(']');
		return sb.ToString();
	}

	/// <inheritdoc cref="ToArrayString{T}(T[])"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToArrayString<T>(this T[,] @this) => @this.ToArrayString(null);

	/// <inheritdoc cref="ToArrayString{T}(T[], Func{T, string?})"/>
	public static string ToArrayString<T>(this T[,] @this, Func<T, string?>? valueConverter)
	{
		valueConverter ??= (static value => value?.ToString());

		var (m, n) = (@this.GetLength(0), @this.GetLength(1));
		var sb = new StringBuilder();
		sb.Append('[').AppendLine();
		for (var i = 0; i < m; i++)
		{
			sb.Append("  ");
			for (var j = 0; j < n; j++)
			{
				var element = @this[i, j];
				sb.Append(valueConverter(element));
				if (j != n - 1)
				{
					sb.Append(", ");
				}
			}
			if (i != m - 1)
			{
				sb.Append(',');
			}
			sb.AppendLine();
		}
		sb.Append(']');
		return sb.ToString();
	}
}
