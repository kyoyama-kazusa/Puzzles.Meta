namespace System.Numerics;

/// <summary>
/// Provides extension methods on <see cref="BitOperations"/>.
/// </summary>
/// <seealso cref="BitOperations"/>
[SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
public static partial class BitOperationsExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="sbyte"/>.
	/// </summary>
	extension(sbyte @this)
	{
		/// <summary>
		/// Find all offsets of set bits of the binary representation of a specified value.
		/// </summary>
		/// <returns>All offsets.</returns>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[sbyte.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = sbyte.TrailingZeroCount(@this);
				@this &= (sbyte)(@this - 1);
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(sbyte) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// <para>Get an enumerator to iterate on each bits of the specified integer value.</para>
		/// <para>This method will allow you to use <see langword="foreach"/> loop to iterate on all indices of set bits.</para>
		/// </summary>
		/// <returns>All indices of set bits.</returns>
		/// <remarks>
		/// This method allows you using <see langword="foreach"/> loop to iterate this value:
		/// <code><![CDATA[
		/// foreach (var bit in 17)
		/// {
		///     // Do something...
		/// }
		/// ]]></code>
		/// </remarks>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="byte"/>.
	/// </summary>
	extension(byte @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[byte.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = byte.TrailingZeroCount(@this);
				@this &= (byte)(@this - 1);
			}
			return result;
		}

		/// <summary>
		/// Find an index of the binary representation of a value after the specified index whose bit is set <see langword="true"/>.
		/// </summary>
		/// <param name="index">The index. The value will be automatically plus 1 in loop. Don't pass the value added 1.</param>
		/// <returns>The index.</returns>
		public int GetNextSet(int index)
		{
			for (var i = index + 1; i < 8; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Skip the specified number of set bits and iterate on the integer with other set bits.
		/// </summary>
		/// <param name="setBitPosCount">Indicates how many set bits you want to skip to iterate.</param>
		/// <returns>The byte value that only contains the other set bits.</returns>
		/// <remarks>
		/// For example:
		/// <code><![CDATA[
		/// byte value = 0b00010111;
		/// foreach (int bitPos in value.SkipSetBit(2))
		/// {
		///     yield return bitPos + 1;
		/// }
		/// ]]></code>
		/// You will get 3 and 5, because all set bit positions are 0, 1, 2 and 4, and we have skipped
		/// two of them, so the result set bit positions to iterate on are only 2 and 4.
		/// </remarks>
		public byte SkipSetBit(int setBitPosCount)
		{
			var result = @this;
			for (var (i, count) = (0, 0); i < 8; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					result &= (byte)~(1 << i);

					if (++count == setBitPosCount)
					{
						break;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Get an <see cref="int"/> value, indicating that the absolute position of all set bits with the specified set bit order.
		/// </summary>
		/// <param name="order">The number of the order of set bits.</param>
		/// <returns>The position.</returns>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(byte) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="byte"/>.
	/// </summary>
	extension(ref byte @this)
	{
		/// <summary>
		/// <para>Reverse all bits in a specified value.</para>
		/// <para>Note that the value is passed by <b>reference</b> though the method is an extension method, and returns nothing.</para>
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReverseBits()
		{
			@this = (byte)(@this >> 1 & 0x55 | (@this & 0x55) << 1);
			@this = (byte)(@this >> 2 & 0x33 | (@this & 0x33) << 2);
			@this = (byte)(@this >> 4 | @this << 4);
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="short"/>.
	/// </summary>
	extension(short @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[short.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = short.TrailingZeroCount(@this);
				@this &= (short)(@this - 1);
			}
			return result;
		}

		/// <inheritdoc cref="GetNextSet(byte, int)"/>
		public int GetNextSet(int index)
		{
			for (var i = index + 1; i < 16; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="SkipSetBit(byte, int)"/>
		public short SkipSetBit(int setBitPosCount)
		{
			var result = @this;
			for (var (i, count) = (0, 0); i < 16; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					result &= (short)~(1 << i);

					if (++count == setBitPosCount)
					{
						break;
					}
				}
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(short) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="short"/>.
	/// </summary>
	extension(ref short @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReverseBits()
		{
			@this = (short)(@this >> 1 & 0x5555 | (@this & 0x5555) << 1);
			@this = (short)(@this >> 2 & 0x3333 | (@this & 0x3333) << 2);
			@this = (short)(@this >> 4 & 0x0F0F | (@this & 0x0F0F) << 4);
			@this = (short)(@this >> 8 | @this << 8);
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="ushort"/>.
	/// </summary>
	extension(ushort @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[ushort.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = ushort.TrailingZeroCount(@this);
				@this &= (ushort)(@this - 1);
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(ushort) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="int"/>.
	/// </summary>
	extension(int @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[int.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = int.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="GetNextSet(byte, int)"/>
		public int GetNextSet(int index)
		{
			for (var i = index + 1; i < 32; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="SkipSetBit(byte, int)"/>
		public int SkipSetBit(int setBitPosCount)
		{
			var result = @this;
			for (var (i, count) = (0, 0); i < 32; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					result &= ~(1 << i);

					if (++count == setBitPosCount)
					{
						break;
					}
				}
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(int) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="int"/>.
	/// </summary>
	extension(ref int @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReverseBits()
		{
			@this = @this >> 1 & 0x55555555 | (@this & 0x55555555) << 1;
			@this = @this >> 2 & 0x33333333 | (@this & 0x33333333) << 2;
			@this = @this >> 4 & 0x0F0F0F0F | (@this & 0x0F0F0F0F) << 4;
			@this = @this >> 8 & 0x00FF00FF | (@this & 0x00FF00FF) << 8;
			@this = @this >> 16 | @this << 16;
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="uint"/>.
	/// </summary>
	extension(uint @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[uint.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = BitOperations.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(uint) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="long"/>.
	/// </summary>
	extension(long @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[long.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = BitOperations.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="GetNextSet(byte, int)"/>
		public int GetNextSet(int index)
		{
			for (var i = index + 1; i < 64; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="SkipSetBit(byte, int)"/>
		public long SkipSetBit(int setBitPosCount)
		{
			var result = @this;
			for (var (i, count) = (0, 0); i < 64; i++)
			{
				if ((@this >> i & 1) != 0)
				{
					result &= ~(1 << i);

					if (++count == setBitPosCount)
					{
						break;
					}
				}
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public int SetAt(int order) => SetAt((ulong)@this, order);

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int64Enumerator GetEnumerator() => new((ulong)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="long"/>.
	/// </summary>
	extension(ref long @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReverseBits()
		{
			@this = @this >> 1 & 0x55555555_55555555L | (@this & 0x55555555_55555555L) << 1;
			@this = @this >> 2 & 0x33333333_33333333L | (@this & 0x33333333_33333333L) << 2;
			@this = @this >> 4 & 0x0F0F0F0F_0F0F0F0FL | (@this & 0x0F0F0F0F_0F0F0F0FL) << 4;
			@this = @this >> 8 & 0x00FF00FF_00FF00FFL | (@this & 0x00FF00FF_00FF00FFL) << 8;
			@this = @this >> 16 & 0x0000FFFF_0000FFFFL | (@this & 0x0000FFFF_0000FFFFL) << 16;
			@this = @this >> 32 | @this << 32;
		}
	}

	/// <summary>
	/// Provides extension members on <see cref="ulong"/>.
	/// </summary>
	extension(ulong @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[ulong.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = BitOperations.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public int SetAt(int order)
		{
			var (mask, size, @base) = (0x0000FFFFU, 16U, 0U);
			if (order++ >= BitOperations.PopCount(@this))
			{
				return -1;
			}

			while (size > 0)
			{
				if (order > BitOperations.PopCount(@this & mask))
				{
					@base += size;
					size >>= 1;
					mask |= mask << (int)size;
				}
				else
				{
					size >>= 1;
					mask >>= (int)size;
				}
			}
			return @base == 64 ? -1 : (int)@base;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int64Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="Int128"/>.
	/// </summary>
	extension(Int128 @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[(int)Int128.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = (int)Int128.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public unsafe int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(Int128) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int128Enumerator GetEnumerator() => new((UInt128)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="UInt128"/>.
	/// </summary>
	extension(UInt128 @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[(int)UInt128.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = (int)UInt128.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public unsafe int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(UInt128) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public Int128Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="nint"/>.
	/// </summary>
	extension(nint @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[(int)nint.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = (int)nint.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public unsafe int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(nint) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public NIntEnumerator GetEnumerator() => new((nuint)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="nuint"/>.
	/// </summary>
	extension(nuint @this)
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == 0)
			{
				return [];
			}

			var (result, p) = (new int[(int)nuint.PopCount(@this)], 0);
			while (@this != 0)
			{
				result[p++] = (int)nuint.TrailingZeroCount(@this);
				@this &= @this - 1;
			}
			return result;
		}

		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(2)]
		public unsafe int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(nuint) << 3; i++, @this >>= 1)
			{
				if ((@this & 1) != 0 && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(2)]
		public NIntEnumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TNumber"/>,
	/// where <typeparamref name="TNumber"/> satisfies multiple constraints.
	/// </summary>
	extension<TNumber>(TNumber @this)
#if NUMERIC_GENERIC_TYPE
		where TNumber : IBitwiseOperators<TNumber, TNumber, TNumber>, INumber<TNumber>, IShiftOperators<TNumber, int, TNumber>
#else
		where TNumber :
			IAdditiveIdentity<TNumber, TNumber>,
			IBitwiseOperators<TNumber, TNumber, TNumber>,
			IEqualityOperators<TNumber, TNumber, bool>,
			IMultiplicativeIdentity<TNumber, TNumber>,
			IShiftOperators<TNumber, int, TNumber>
#endif
	{
		/// <inheritdoc cref="SetAt(byte, int)"/>
		[OverloadResolutionPriority(1)]
		public unsafe int SetAt(int order)
		{
			for (int i = 0, count = -1; i < sizeof(TNumber) << 3; i++, @this >>= 1)
			{
				if ((@this & TNumber.MultiplicativeIdentity) != TNumber.AdditiveIdentity && ++count == order)
				{
					return i;
				}
			}
			return -1;
		}
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TInteger"/>,
	/// where <typeparamref name="TInteger"/> satisfies <see cref="IBinaryInteger{TSelf}"/> constraint.
	/// </summary>
	extension<TInteger>(TInteger @this) where TInteger : IBinaryInteger<TInteger>
	{
		/// <inheritdoc cref="GetAllSets(sbyte)"/>
		[OverloadResolutionPriority(1)]
		public ReadOnlySpan<int> GetAllSets()
		{
			if (@this == TInteger.Zero)
			{
				return [];
			}

			var (result, p) = (new int[int.CreateChecked(TInteger.PopCount(@this))], 0);
			while (@this != TInteger.Zero)
			{
				result[p++] = int.CreateChecked(TInteger.TrailingZeroCount(@this));
				@this &= @this - TInteger.One;
			}
			return result;
		}
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TInteger"/>,
	/// where <typeparamref name="TInteger"/> satisfies <see cref="IBinaryInteger{TSelf}"/> constraint.
	/// </summary>
	extension<TInteger>(TInteger @this)
#if NUMERIC_GENERIC_TYPE
		where TInteger : IBitwiseOperators<TInteger, TInteger, TInteger>, IBinaryInteger<TInteger>, IShiftOperators<TInteger, int, TInteger>
#else
		where TInteger :
			IAdditiveIdentity<TInteger, TInteger>,
			IBitwiseOperators<TInteger, TInteger, TInteger>,
			IEqualityOperators<TInteger, TInteger, bool>,
			IMultiplicativeIdentity<TInteger, TInteger>,
			IShiftOperators<TInteger, int, TInteger>
#endif
	{
		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[OverloadResolutionPriority(1)]
		public unsafe GenericIntegerEnumerator<TInteger> GetEnumerator() => new(@this, sizeof(TInteger) << 3);
	}
}
