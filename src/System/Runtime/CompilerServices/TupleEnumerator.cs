namespace System.Runtime.CompilerServices;

/// <summary>
/// Represents for an enumerator that iterates on each elements stored in a <see cref="ITuple"/>.
/// </summary>
/// <typeparam name="TTuple">The type of tuple.</typeparam>
/// <param name="tuple"><inheritdoc cref="_tuple" path="/summary"/></param>
[StructLayout(LayoutKind.Auto)]
[TypeImpl(
	TypeImplFlags.AllObjectMethods | TypeImplFlags.Disposable,
	OtherModifiersOnDisposableDispose = "readonly",
	ExplicitlyImplsDisposable = true)]
public ref partial struct TupleEnumerator<TTuple>(TTuple tuple) : IEnumerator<object?> where TTuple : ITuple?, allows ref struct
{
	/// <summary>
	/// A tuple instance.
	/// </summary>
	private readonly TTuple _tuple = tuple;

	/// <summary>
	/// The current index.
	/// </summary>
	private int _index = -1;


	/// <inheritdoc cref="IEnumerator.Current"/>
	public readonly object? Current => _tuple?[_index];


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public bool MoveNext() => ++_index < _tuple?.Length;

	/// <inheritdoc/>
	[DoesNotReturn]
	readonly void IEnumerator.Reset() => throw new NotImplementedException();
}
