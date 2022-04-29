// -----------------------------------------------------------------------
// <copyright file="AnyRoutedEventTrigger.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Behaviors
{
    using System;
    using System.Windows;

    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Triggered by any routed event.
    /// </summary>
    internal class AnyRoutedEventTrigger : TriggerBase<UIElement>
    {
        private Delegate handler;

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject == null)
            {
                return;
            }

            this.handler = new RoutedEventHandler(this.OnEvent);

            var events = EventManager.GetRoutedEvents();
            foreach (var ev in events)
            {
                this.AssociatedObject.AddHandler(ev, this.handler);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            if (this.AssociatedObject == null)
            {
                base.OnDetaching();
                return;
            }

            var events = EventManager.GetRoutedEvents();
            foreach (var ev in events)
            {
                this.AssociatedObject.RemoveHandler(ev, this.handler);
            }

            base.OnDetaching();
        }

        private void OnEvent(object sender, RoutedEventArgs ev)
        {
            this.InvokeActions(ev);
        }
    }
}
