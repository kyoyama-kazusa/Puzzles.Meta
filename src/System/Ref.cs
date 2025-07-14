namespace System;

/// <summary>
/// Represents a list of methods that can check for the concept "References" defined in C#.
/// </summary>
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static unsafe class @ref
{
	/// <summary>
	/// Swaps for two elements.
	/// </summary>
	/// <typeparam name="T">The type of both two arguments.</typeparam>
	/// <param name="left">The first element to be swapped.</param>
	/// <param name="right">The second element to be swapped.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(ref T left, ref T right) where T : allows ref struct
	{
		if (!Unsafe.AreSame(in left, in right))
		{
			var temp = left;
			left = right;
			right = temp;
		}
	}

	/// <summary>
	/// Simply invokes the method <see cref="Unsafe.As{TFrom, TTo}(ref TFrom)"/>, but with target generic type being fixed type <see cref="byte"/>.
	/// </summary>
	/// <typeparam name="T">The base type that is converted from.</typeparam>
	/// <param name="ref">
	/// The reference to the value. Generally speaking the value should be a <see langword="ref readonly"/> parameter, but C# disallows it,
	/// using <see langword="ref readonly"/> as a combined parameter modifier.
	/// </param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref byte ByteRef<T>(ref T @ref) where T : allows ref struct => ref Unsafe.As<T, byte>(ref @ref);

	/// <inheritdoc cref="ByteRef{T}(ref T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref readonly byte ReadOnlyByteRef<T>(ref readonly T @ref) where T : allows ref struct
		=> ref Unsafe.As<T, byte>(ref Unsafe.AsRef(in @ref));

	/// <summary>
	/// Advances the pointer to an element after the specified number of block memory elements.
	/// </summary>
	/// <typeparam name="T">The type of the element in block memory.</typeparam>
	/// <param name="ref">The reference to be advanced.</param>
	/// <param name="length">The length that the pointer moves.</param>
	/// <returns>The target reference to the specified element.</returns>
	/// <remarks>
	/// Pass negative value into parameter <paramref name="length"/> if you want to move previously,
	/// which is equivalent to method call <see cref="Unsafe.Subtract{T}(ref T, int)"/>
	/// </remarks>
	/// <seealso cref="Unsafe.Subtract{T}(ref T, int)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref T Add<T>(ref T @ref, int length) where T : allows ref struct => ref Unsafe.Add(ref @ref, length);
}
