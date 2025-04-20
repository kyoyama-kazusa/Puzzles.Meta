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
	}
}
