﻿using System;
using Xamarin.CommunityToolkit.Behaviors.Internals;
using Microsoft.Maui; using Microsoft.Maui.Controls; using Microsoft.Maui.Graphics; using Microsoft.Maui.Controls.Compatibility;

namespace Xamarin.CommunityToolkit.Behaviors
{
	/// <summary>
	/// The <see cref="SetFocusOnEntryCompletedBehavior"/> is an attached property for entries that allows the user to specify what <see cref="VisualElement"/> should gain focus after the user completes that entry.
	/// </summary>
	public class SetFocusOnEntryCompletedBehavior : BaseBehavior<VisualElement>
	{
		/// <summary>
		/// The <see cref="NextElementProperty"/> attached property.
		/// </summary>
		public static readonly BindableProperty NextElementProperty =
			BindableProperty.CreateAttached(
				"NextElement",
				typeof(VisualElement),
				typeof(SetFocusOnEntryCompletedBehavior),
				default(VisualElement),
				propertyChanged: OnNextElementChanged);

		/// <summary>
		/// Required <see cref="GetNextElement"/> accessor for <see cref="NextElementProperty"/> attached property.
		/// </summary>
		public static VisualElement GetNextElement(BindableObject view) =>
			(VisualElement)view.GetValue(NextElementProperty);

		/// <summary>
		/// Required <see cref="SetNextElement"/> accessor for <see cref="NextElementProperty"/> attached property.
		/// </summary>
		public static void SetNextElement(BindableObject view, VisualElement value) =>
			view.SetValue(NextElementProperty, value);

		static void OnNextElementChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var entry = (Entry)bindable;
			var weakEntry = new WeakReference<Entry>(entry);
			entry.Completed += completedHandler;

			void completedHandler(object? sender, EventArgs e)
			{
				if (weakEntry.TryGetTarget(out var origEntry))
				{
					GetNextElement(origEntry)?.Focus();
				}
			}
		}
	}
}