namespace System;

/// <summary>
/// Represents an event handler that simplifies the definition on customization on event handler delegate types.
/// </summary>
/// <typeparam name="TSender">The type of sender which triggers the event.</typeparam>
/// <typeparam name="TEventArgs">The type of event argument provided.</typeparam>
/// <param name="sender">The sender which triggers the event.</param>
/// <param name="e">The event arguments provided.</param>
public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e)
	where TSender : allows ref struct
	where TEventArgs : EventArgs;
