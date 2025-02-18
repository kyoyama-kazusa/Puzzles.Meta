namespace System.Collections;

/// <summary>
/// Provides with a converter that can convert the object <see cref="BitArray"/> into an array of commonly-used binary integer types.
/// </summary>
public static class BitArrayConverter
{
	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="bool"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="bool"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<bool> ToBooleanSpan(BitArray bits)
	{
		var result = new bool[bits.Count];
		bits.CopyTo(result, 0);
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="byte"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="byte"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<byte> ToByteSpan(BitArray bits)
	{
		var result = new byte[(bits.Count + 7) / 8];
		bits.CopyTo(result, 0);
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="sbyte"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="sbyte"/> instance.</returns>
	public static ReadOnlySpan<sbyte> ToSByteSpan(BitArray bits)
	{
		var groupCount = (bits.Count + 7) / 8;
		var result = new sbyte[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (sbyte)0;
			var startBit = groupIndex * 8;
			var bitsToProcess = Math.Min(8, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (sbyte)(1 << i);
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="ushort"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="ushort"/> instance.</returns>
	public static ReadOnlySpan<ushort> ToUInt16Span(BitArray bits)
	{
		var groupCount = (bits.Count + 15) / 16;
		var result = new ushort[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (ushort)0;
			var startBit = groupIndex * 16;
			var bitsToProcess = Math.Min(16, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (ushort)(1 << i);
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="short"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="short"/> instance.</returns>
	public static ReadOnlySpan<short> ToInt16Span(BitArray bits)
	{
		var groupCount = (bits.Count + 15) / 16;
		var result = new short[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (short)0;
			var startBit = groupIndex * 16;
			var bitsToProcess = Math.Min(16, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (short)(1 << i);
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="uint"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="uint"/> instance.</returns>
	public static ReadOnlySpan<uint> ToUInt32Span(BitArray bits)
	{
		var groupCount = (bits.Count + 31) / 32;
		var result = new uint[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = 0U;
			var startBit = groupIndex * 32;
			var bitsToProcess = Math.Min(32, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= 1U << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="int"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="int"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<int> ToInt32Span(BitArray bits)
	{
		var result = new int[(bits.Count + 31) / 32];
		bits.CopyTo(result, 0);
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="ulong"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="ulong"/> instance.</returns>
	public static ReadOnlySpan<ulong> ToUInt64Span(BitArray bits)
	{
		var groupCount = (bits.Count + 63) / 64;
		var result = new ulong[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = 0UL;
			var startBit = groupIndex * 64;
			var bitsToProcess = Math.Min(64, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= 1UL << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="long"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="long"/> instance.</returns>
	public static ReadOnlySpan<long> ToInt64Span(BitArray bits)
	{
		var groupCount = (bits.Count + 63) / 64;
		var result = new long[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = 0L;
			var startBit = groupIndex * 64;
			var bitsToProcess = Math.Min(64, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= 1L << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="UInt128"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="UInt128"/> instance.</returns>
	public static ReadOnlySpan<UInt128> ToUInt128Span(BitArray bits)
	{
		var groupCount = (bits.Count + 127) / 128;
		var result = new UInt128[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (UInt128)0;
			var startBit = groupIndex * 128;
			var bitsToProcess = Math.Min(128, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (UInt128)1 << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="Int128"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="Int128"/> instance.</returns>
	public static ReadOnlySpan<Int128> ToInt128Span(BitArray bits)
	{
		var groupCount = (bits.Count + 127) / 128;
		var result = new Int128[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (Int128)0;
			var startBit = groupIndex * 128;
			var bitsToProcess = Math.Min(128, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (Int128)1 << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="nuint"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="nuint"/> instance.</returns>
	public static ReadOnlySpan<nuint> ToNUIntSpan(BitArray bits)
	{
		var unit = nuint.Size << 3;
		var groupCount = (bits.Count + unit - 1) / unit;
		var result = new nuint[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (nuint)0;
			var startBit = groupIndex * unit;
			var bitsToProcess = Math.Min(unit, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (nuint)1 << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance into a <see cref="ReadOnlySpan{T}"/> of <see cref="nint"/> instance.
	/// </summary>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <see cref="nint"/> instance.</returns>
	public static ReadOnlySpan<nint> ToNIntSpan(BitArray bits)
	{
		var unit = nint.Size << 3;
		var groupCount = (bits.Count + unit - 1) / unit;
		var result = new nint[groupCount];
		for (var groupIndex = 0; groupIndex < groupCount; groupIndex++)
		{
			var value = (nint)0;
			var startBit = groupIndex * unit;
			var bitsToProcess = Math.Min(unit, bits.Count - startBit);
			for (var i = 0; i < bitsToProcess; i++)
			{
				if (bits[startBit + i])
				{
					value |= (nint)1 << i;
				}
			}
			result[groupIndex] = value;
		}
		return result;
	}

	/// <summary>
	/// Converts the specified <see cref="BitArray"/> instance
	/// into a <see cref="ReadOnlySpan{T}"/> of <typeparamref name="T"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of the binary integer to be converted.</typeparam>
	/// <param name="bits">The bits.</param>
	/// <returns>A <see cref="ReadOnlySpan{T}"/> of <typeparamref name="T"/> instance.</returns>
	/// <exception cref="OverflowException">
	/// Throws when create an overflowed <see cref="int"/> value as an index from <typeparamref name="T"/>.
	/// </exception>
	/// <exception cref="NotSupportedException">
	/// Throws when type <typeparamref name="T"/> is not compatible with <see cref="int"/>.
	/// </exception>
	public static ReadOnlySpan<T> ToSpan<T>(BitArray bits) where T : IBinaryInteger<T>, IShiftOperators<T, T, T>
	{
		var dic = new Dictionary<Type, Func<ReadOnlySpan<T>>>
		{
			{
				typeof(byte),
				() =>
				{
					var tempResult = ToByteSpan(bits);
					return Unsafe.As<ReadOnlySpan<byte>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(sbyte),
				() =>
				{
					var tempResult = ToSByteSpan(bits);
					return Unsafe.As<ReadOnlySpan<sbyte>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(ushort),
				() =>
				{
					var tempResult = ToUInt16Span(bits);
					return Unsafe.As<ReadOnlySpan<ushort>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(short),
				() =>
				{
					var tempResult = ToInt16Span(bits);
					return Unsafe.As<ReadOnlySpan<short>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(uint),
				() =>
				{
					var tempResult = ToUInt32Span(bits);
					return Unsafe.As<ReadOnlySpan<uint>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(int),
				() =>
				{
					var tempResult = ToInt32Span(bits);
					return Unsafe.As<ReadOnlySpan<int>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(ulong),
				() =>
				{
					var tempResult = ToUInt64Span(bits);
					return Unsafe.As<ReadOnlySpan<ulong>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(long),
				() =>
				{
					var tempResult = ToInt64Span(bits);
					return Unsafe.As<ReadOnlySpan<long>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(nuint),
				() =>
				{
					var tempResult = ToNUIntSpan(bits);
					return Unsafe.As<ReadOnlySpan<nuint>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(nint),
				() =>
				{
					var tempResult = ToNIntSpan(bits);
					return Unsafe.As<ReadOnlySpan<nint>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(UInt128),
				() =>
				{
					var tempResult = ToUInt128Span(bits);
					return Unsafe.As<ReadOnlySpan<UInt128>, ReadOnlySpan<T>>(ref tempResult);
				}
			},
			{
				typeof(Int128),
				() =>
				{
					var tempResult = ToInt128Span(bits);
					return Unsafe.As<ReadOnlySpan<Int128>, ReadOnlySpan<T>>(ref tempResult);
				}
			}
		};
		if (dic.TryGetValue(typeof(T), out var resultCreator))
		{
			return resultCreator();
		}

		// Fallback to handling logic on an arbitrary type T.
		var unit = T.PopCount(T.AllBitsSet);
		var length = T.CreateChecked(bits.Count);
		var groupCount = (length + unit - T.One) / unit;
		var result = new T[int.CreateChecked(groupCount)];
		for (var groupIndex = T.Zero; groupIndex < groupCount; groupIndex++)
		{
			var value = T.Zero;
			var startBit = groupIndex * unit;
			var bitsToProcess = T.Min(unit, length - startBit);
			for (var i = T.Zero; i < bitsToProcess; i++)
			{
				if (bits[int.CreateChecked(startBit + i)])
				{
					value |= T.One << i;
				}
			}
			result[int.CreateChecked(groupIndex)] = value;
		}
		return result;
	}

	/// <summary>
	/// Convert the specified <see cref="bool"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="bool"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromBooleanArray(bool[] array) => new(array);

	/// <summary>
	/// Convert the specified <see cref="byte"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="byte"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromByteArray(byte[] array) => new(array);

	/// <summary>
	/// Convert the specified <see cref="sbyte"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="sbyte"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromSByteArray(sbyte[] array) => new(Unsafe.As<sbyte[], byte[]>(ref array));

	/// <summary>
	/// Convert the specified <see cref="ushort"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="ushort"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	public static BitArray FromUInt16Array(ushort[] array)
	{
		var result = new BitArray(array.Length * 16);
		for (var i = 0; i < array.Length; i++)
		{
			foreach (var bit in array[i])
			{
				result[i * 16 + bit] = true;
			}
		}
		return result;
	}

	/// <summary>
	/// Convert the specified <see cref="short"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="short"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromInt16Array(short[] array) => FromUInt16Array(Unsafe.As<short[], ushort[]>(ref array));

	/// <summary>
	/// Convert the specified <see cref="uint"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="uint"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromUInt32Array(uint[] array) => new(Unsafe.As<uint[], int[]>(ref array));

	/// <summary>
	/// Convert the specified <see cref="int"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="int"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromInt32Array(int[] array) => new(array);

	/// <summary>
	/// Convert the specified <see cref="ulong"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="ulong"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	public static BitArray FromUInt64Array(ulong[] array)
	{
		var result = new BitArray(array.Length * 64);
		for (var i = 0; i < array.Length; i++)
		{
			foreach (var bit in array[i])
			{
				result[i * 64 + bit] = true;
			}
		}
		return result;
	}

	/// <summary>
	/// Convert the specified <see cref="long"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="long"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromInt64Array(long[] array) => FromUInt64Array(Unsafe.As<long[], ulong[]>(ref array));

	/// <summary>
	/// Convert the specified <see cref="UInt128"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">A <see cref="UInt128"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	public static BitArray FromUInt128Array(UInt128[] array)
	{
		var result = new BitArray(array.Length * 128);
		for (var i = 0; i < array.Length; i++)
		{
			foreach (var bit in array[i])
			{
				result[i * 128 + bit] = true;
			}
		}
		return result;
	}

	/// <summary>
	/// Convert the specified <see cref="Int128"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="Int128"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromInt128Array(Int128[] array) => FromUInt128Array(Unsafe.As<Int128[], UInt128[]>(ref array));

	/// <summary>
	/// Convert the specified <see cref="nuint"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="nuint"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	public static BitArray FromNUIntArray(nuint[] array)
	{
		var unit = nuint.Size << 3;
		var result = new BitArray(array.Length * unit);
		for (var i = 0; i < array.Length; i++)
		{
			foreach (var bit in array[i])
			{
				result[i * unit + bit] = true;
			}
		}
		return result;
	}

	/// <summary>
	/// Convert the specified <see cref="nint"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <param name="array">An <see cref="nint"/>[] instance.</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BitArray FromNIntArray(nint[] array) => FromNUIntArray(Unsafe.As<nint[], nuint[]>(ref array));

	/// <summary>
	/// Convert the specified <typeparamref name="T"/>[] into a <see cref="BitArray"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of binary integer.</typeparam>
	/// <param name="array">An instance of type <typeparamref name="T"/>[].</param>
	/// <returns>A <see cref="BitArray"/> instance converted.</returns>
	/// <exception cref="OverflowException">
	/// Throws when overflow on casting from <typeparamref name="T"/> to <see cref="int"/>.
	/// </exception>
	public static BitArray FromArray<T>(T[] array) where T : IBinaryInteger<T>
	{
		var unit = int.CreateChecked(T.PopCount(T.AllBitsSet));
		var result = new BitArray(array.Length * unit);
		for (var i = 0; i < array.Length; i++)
		{
			foreach (var bit in array[i])
			{
				result[i * unit + bit] = true;
			}
		}
		return result;
	}
}
