// -----------------------------------------------------------------------
// <copyright file="DebugWriteAction.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Behaviors
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Action which calls System.Diagnostics.Debug.WriteLine().
    /// </summary>
    internal class DebugWriteAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// Dependency property of Text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(DebugWriteAction),
                new PropertyMetadata());

        /// <summary>
        /// Gets or sets the text to write.
        /// The parameter to this action will be printed when set to null(default).
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <inheritdoc/>
        protected override void Invoke(object parameter)
        {
            if (this.Text != null)
            {
                Debug.WriteLine(this.Text);
            }
            else if (parameter is RoutedEventArgs)
            {
                var ev = (RoutedEventArgs)parameter;
                Debug.WriteLine($"{ev.OriginalSource}: {ev.RoutedEvent}");
            }
            else
            {
                Debug.WriteLine(parameter);
            }
        }
    }
}
