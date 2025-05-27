namespace System;

/// <summary>
/// Provides extension methods on <see cref="Enum"/>.
/// </summary>
/// <seealso cref="Enum"/>
public static class EnumExtensions
{
	/// <summary>
	/// Provides extension members on <typeparamref name="T"/>,
	/// where <typeparamref name="T"/> satisfies <see langword="unmanaged"/>, <see cref="Enum"/> constraints.
	/// </summary>
	extension<T>(T @this) where T : unmanaged, Enum
	{
		/// <summary>
		/// Checks whether the current enumeration field is a flag.
		/// </summary>
		/// <returns>A <see cref="bool"/> result indicating that.</returns>
		public bool IsFlag
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				var thisCopied = @this;
				return new Dictionary<Type, Func<bool>>
				{
					{ typeof(sbyte), f<sbyte> },
					{ typeof(byte), f<byte> },
					{ typeof(short), f<short> },
					{ typeof(ushort), f<ushort> },
					{ typeof(int), f<int> },
					{ typeof(uint), f<uint> },
					{ typeof(long), f<long> },
					{ typeof(ulong), f<ulong> },
				}.TryGetValue(Enum.GetUnderlyingType(typeof(T)), out var func) && func();


				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				bool f<TInteger>() where TInteger : IBinaryInteger<TInteger>
					=> Unsafe.As<T, TInteger>(ref thisCopied) is var integer && (integer == TInteger.Zero || TInteger.IsPow2(integer));
			}
		}

		/// <summary>
		/// To get all possible flags from a specified enumeration instance.
		/// </summary>
		public ReadOnlySpan<T> AllFlags
		{
			get
			{
				var set = new HashSet<T>(Enum.GetValues<T>().Length);
				foreach (var flag in @this)
				{
					set.Add(flag);
				}
				return set.AsReadOnlySpan();
			}
		}


		/// <summary>
		/// Indicates the length of the elements stored in type <typeparamref name="T"/>.
		/// </summary>
		public static int Length => Enum.GetValues<T>().Length;

		/// <summary>
		/// Indicates the values.
		/// </summary>
		public static ReadOnlySpan<T> Values => Enum.GetValues<T>();


		/// <summary>
		/// Get all possible flags that the current enumeration field set.
		/// </summary>
		/// <returns>All flags.</returns>
		/// <exception cref="InvalidOperationException">
		/// Throws when the type isn't applied the attribute <see cref="FlagsAttribute"/>.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EnumFlagsEnumerator<T> GetEnumerator() => new(@this);



		/// <inheritdoc cref="Enum.Parse{TEnum}(ReadOnlySpan{char})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryParse(ReadOnlySpan<char> value, out T result) => Enum.TryParse(value, out result);

		/// <inheritdoc cref="Enum.Parse{TEnum}(ReadOnlySpan{char}, bool)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryParse(ReadOnlySpan<char> value, bool ignoreCase, out T result)
			=> Enum.TryParse(value, ignoreCase, out result);

		/// <inheritdoc cref="Enum.Parse{TEnum}(string)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryParse(string? value, out T result) => Enum.TryParse(value, out result);

		/// <inheritdoc cref="Enum.Parse{TEnum}(string, bool)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryParse(string? value, bool ignoreCase, out T result) => Enum.TryParse(value, ignoreCase, out result);

		/// <inheritdoc cref="Enum.Parse{TEnum}(ReadOnlySpan{char})"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Parse(ReadOnlySpan<char> value) => Enum.Parse<T>(value);

		/// <inheritdoc cref="Enum.Parse{TEnum}(ReadOnlySpan{char}, bool)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Parse(ReadOnlySpan<char> value, bool ignoreCase) => Enum.Parse<T>(value, ignoreCase);

		/// <inheritdoc cref="Enum.Parse{TEnum}(string)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Parse(string value) => Enum.Parse<T>(value);

		/// <inheritdoc cref="Enum.Parse{TEnum}(string, bool)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Parse(string value, bool ignoreCase) => Enum.Parse<T>(value, ignoreCase);

		/// <summary>
		/// Merges all flags into one.
		/// </summary>
		/// <param name="flags">The flags.</param>
		/// <returns>The merged value.</returns>
		public static unsafe T MergeFlags(ReadOnlySpan<T> flags)
		{
			switch (sizeof(T))
			{
				case 1:
				{
					var result = (byte)0;
					foreach (var flag in flags)
					{
						result |= Unsafe.As<T, byte>(ref Unsafe.AsRef(in flag));
					}
					return Unsafe.As<byte, T>(ref result);
				}
				case 2:
				{
					var result = (ushort)0;
					foreach (var flag in flags)
					{
						result |= Unsafe.As<T, ushort>(ref Unsafe.AsRef(in flag));
					}
					return Unsafe.As<ushort, T>(ref result);
				}
				case 4:
				{
					var result = (uint)0;
					foreach (var flag in flags)
					{
						result |= Unsafe.As<T, uint>(ref Unsafe.AsRef(in flag));
					}
					return Unsafe.As<uint, T>(ref result);
				}
				case 8:
				{
					var result = (ulong)0;
					foreach (var flag in flags)
					{
						result |= Unsafe.As<T, ulong>(ref Unsafe.AsRef(in flag));
					}
					return Unsafe.As<ulong, T>(ref result);
				}
				default:
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}
