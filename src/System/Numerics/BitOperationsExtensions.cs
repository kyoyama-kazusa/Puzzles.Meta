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
		/// Indicates all positions (indices) whose corresponding bits are set 1.
		/// </summary>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		/// Returns an enumerator type that can iterate on each position (index) whose cooresponding bit is set 1.
		/// </summary>
		/// <returns>An enumerator object that can iterate on each position.</returns>
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="byte"/>.
	/// </summary>
	extension(byte @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="short"/>.
	/// </summary>
	extension(ref short @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
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
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="int"/>.
	/// </summary>
	extension(int @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
			{
				if (@this == 0)
				{
					return [];
				}

				var (result, p) = (new int[BitOperations.PopCount(@this)], 0);
				while (@this != 0)
				{
					result[p++] = BitOperations.TrailingZeroCount(@this);
					@this &= @this - 1;
				}
				return result;
			}
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
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new((uint)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="int"/>.
	/// </summary>
	extension(ref int @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
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
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int32Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="long"/>.
	/// </summary>
	extension(long @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public int SetAt(int order) => SetAt((ulong)@this, order);

		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[OverloadResolutionPriority(2)]
		public Int64Enumerator GetEnumerator() => new((ulong)@this);
	}

	/// <summary>
	/// Provides extension members on <see langword="ref"/> <see cref="long"/>.
	/// </summary>
	extension(ref long @this)
	{
		/// <inheritdoc cref="ReverseBits(ref byte)"/>
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
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int64Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="Int128"/>.
	/// </summary>
	extension(Int128 @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int128Enumerator GetEnumerator() => new((UInt128)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="UInt128"/>.
	/// </summary>
	extension(UInt128 @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public Int128Enumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="nint"/>.
	/// </summary>
	extension(nint @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public NIntEnumerator GetEnumerator() => new((nuint)@this);
	}

	/// <summary>
	/// Provides extension members on <see cref="nuint"/>.
	/// </summary>
	extension(nuint @this)
	{
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		[OverloadResolutionPriority(2)]
		public NIntEnumerator GetEnumerator() => new(@this);
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TNumber"/>,
	/// where <typeparamref name="TNumber"/> satisfies multiple constraints.
	/// </summary>
	extension<TNumber>(TNumber @this)
		where TNumber : IBitwiseOperators<TNumber, TNumber, TNumber>, INumber<TNumber>, IShiftOperators<TNumber, int, TNumber>
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
		/// <inheritdoc cref="get_AllSets(sbyte)"/>
		public ReadOnlySpan<int> AllSets
		{
			get
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
		/// Creates a <see cref="BitCombinationGenerator{T}"/> instance that can generate a list of <see cref="long"/>
		/// values that are all possibilities of combinations of integer values containing specified number of bits,
		/// and the specified number of bits set 1.
		/// </summary>
		/// <param name="bitCount">Indicates how many bits should be enumerated.</param>
		/// <param name="oneCount">Indicates how many bits set one contained in the value.</param>
		/// <returns>A <see cref="BitCombinationGenerator{T}"/> instance.</returns>
		public static BitCombinationGenerator<TInteger> EnumerateOf(int bitCount, int oneCount)
			=> new(bitCount, oneCount);
	}

	/// <summary>
	/// Provides extension members on <typeparamref name="TInteger"/>,
	/// where <typeparamref name="TInteger"/> satisfies <see cref="IBinaryInteger{TSelf}"/> constraint.
	/// </summary>
	extension<TInteger>(TInteger @this)
		where TInteger : IBitwiseOperators<TInteger, TInteger, TInteger>, IBinaryInteger<TInteger>, IShiftOperators<TInteger, int, TInteger>
	{
		/// <inheritdoc cref="GetEnumerator(sbyte)"/>
		[OverloadResolutionPriority(1)]
		public unsafe GenericIntegerEnumerator<TInteger> GetEnumerator() => new(@this, sizeof(TInteger) << 3);
	}

	/// <summary>
	/// Provides extension members on <see cref="BitOperations"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// In hardware aspects, there's no concept for integer types narrowed than <see cref="int"/>,
	/// so there's no such overloads for <see cref="byte"/>, <see cref="short"/> and so on.
	/// Methods provided here will make the calling operation easier, without casting from <see cref="int"/> to <see cref="uint"/>,
	/// or casting <see cref="long"/> to <see cref="ulong"/>, but it may effect performance when utilized:
	/// <code><![CDATA[
	/// short b = 42;
	/// _ = BitOperations.TrailingZeroCount(b); // Calls overload 'TrailingZeroCount(short)' (not good)
	/// _ = BitOperations.TrailingZeroCount((uint)b); // Calls 'TrailingZeroCount(uint)', which is built-in (good)
	/// ]]></code>
	/// Although I append [<see cref="MethodImplAttribute"/>(<see cref="MethodImplOptions.AggressiveInlining"/>)]
	/// in order to annotate all methods here, there's no need to mark because JIT can determine whether the code can be inlined
	/// in such cases.
	/// </para>
	/// <para>
	/// Why I persist to create such overloads, even if I know the problem?
	/// Well, I just want to make the overloading methods complete :)
	/// </para>
	/// </remarks>
	/// <seealso cref="BitOperations"/>
	/// <seealso cref="MethodImplAttribute"/>
	/// <seealso cref="MethodImplOptions.AggressiveInlining"/>
	extension(BitOperations)
	{
		//
		// TrailingZeroCount
		//

		/// <inheritdoc cref="BitOperations.TrailingZeroCount(int)"/>
		public static int TrailingZeroCount(sbyte @this) => BitOperations.TrailingZeroCount(@this);

		/// <inheritdoc cref="BitOperations.TrailingZeroCount(uint)"/>
		public static int TrailingZeroCount(byte @this) => BitOperations.TrailingZeroCount((uint)@this);

		/// <inheritdoc cref="BitOperations.TrailingZeroCount(int)"/>
		public static int TrailingZeroCount(short @this) => BitOperations.TrailingZeroCount(@this);

		/// <inheritdoc cref="BitOperations.TrailingZeroCount(uint)"/>
		public static int TrailingZeroCount(ushort @this) => BitOperations.TrailingZeroCount((uint)@this);

		//
		// LeadingZeroCount
		//

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(uint)"/>
		public static int LeadingZeroCount(sbyte @this) => BitOperations.LeadingZeroCount((uint)@this);

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(uint)"/>
		public static int LeadingZeroCount(byte @this) => BitOperations.LeadingZeroCount(@this);

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(uint)"/>
		public static int LeadingZeroCount(short @this) => BitOperations.LeadingZeroCount((uint)@this);

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(uint)"/>
		public static int LeadingZeroCount(ushort @this) => BitOperations.LeadingZeroCount(@this);

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(uint)"/>
		public static int LeadingZeroCount(int @this) => BitOperations.LeadingZeroCount((uint)@this);

		/// <inheritdoc cref="BitOperations.LeadingZeroCount(ulong)"/>
		public static int LeadingZeroCount(long @this) => BitOperations.LeadingZeroCount((ulong)@this);

		//
		// PopCount
		//

		/// <inheritdoc cref="BitOperations.PopCount(uint)"/>
		public static int PopCount(sbyte @this) => BitOperations.PopCount((uint)@this);

		/// <inheritdoc cref="BitOperations.PopCount(uint)"/>
		public static int PopCount(byte @this) => BitOperations.PopCount(@this);

		/// <inheritdoc cref="BitOperations.PopCount(uint)"/>
		public static int PopCount(short @this) => BitOperations.PopCount((uint)@this);

		/// <inheritdoc cref="BitOperations.PopCount(uint)"/>
		public static int PopCount(ushort @this) => BitOperations.PopCount(@this);

		/// <inheritdoc cref="BitOperations.PopCount(uint)"/>
		public static int PopCount(int @this) => BitOperations.PopCount((uint)@this);

		/// <inheritdoc cref="BitOperations.PopCount(ulong)"/>
		public static int PopCount(long @this) => BitOperations.PopCount((ulong)@this);

		//
		// IsPow2
		//

		/// <inheritdoc cref="BitOperations.IsPow2(int)"/>
		public static bool IsPow2(sbyte @this) => BitOperations.IsPow2(@this);

		/// <inheritdoc cref="BitOperations.IsPow2(uint)"/>
		public static bool IsPow2(byte @this) => BitOperations.IsPow2((uint)@this);

		/// <inheritdoc cref="BitOperations.IsPow2(int)"/>
		public static bool IsPow2(short @this) => BitOperations.IsPow2(@this);

		/// <inheritdoc cref="BitOperations.IsPow2(uint)"/>
		public static bool IsPow2(ushort @this) => BitOperations.IsPow2((uint)@this);

		//
		// Log2
		//

		/// <inheritdoc cref="BitOperations.Log2(uint)"/>
		public static int Log2(sbyte @this) => BitOperations.Log2((uint)@this);

		/// <inheritdoc cref="BitOperations.Log2(uint)"/>
		public static int Log2(byte @this) => BitOperations.Log2(@this);

		/// <inheritdoc cref="BitOperations.Log2(uint)"/>
		public static int Log2(short @this) => BitOperations.Log2((uint)@this);

		/// <inheritdoc cref="BitOperations.Log2(uint)"/>
		public static int Log2(ushort @this) => BitOperations.Log2(@this);

		/// <inheritdoc cref="BitOperations.Log2(uint)"/>
		public static int Log2(int @this) => BitOperations.Log2((uint)@this);

		/// <inheritdoc cref="BitOperations.Log2(ulong)"/>
		public static int Log2(long @this) => BitOperations.Log2((ulong)@this);

		/// <inheritdoc cref="BitOperations.Log2(nuint)"/>
		public static int Log2(nint @this) => BitOperations.Log2((nuint)@this);
	}
}
