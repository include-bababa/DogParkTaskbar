// -----------------------------------------------------------------------
// <copyright file="IScreenController.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Controllers
{
    using System;

    /// <summary>
    /// Interface class for managing screens.
    /// </summary>
    public interface IScreenController
    {
        /// <summary>
        /// Occurs when the selected screen changed.
        /// </summary>
        event EventHandler<ChangeScreenEventArgs> ScreenChanged;

        /// <summary>
        /// Gets or sets current screen index to place window.
        /// </summary>
        int CurrentScreen { get; set; }
    }
}
