// -----------------------------------------------------------------------
// <copyright file="ChangeScreenEventArgs.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Windows.Forms
{
    using System;

    /// <summary>
    /// Event arguments for changing screen.
    /// </summary>
    public class ChangeScreenEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeScreenEventArgs"/> class.
        /// </summary>
        /// <param name="screenIndex"></param>
        public ChangeScreenEventArgs(int screenIndex)
        {
            this.ScreenIndex = screenIndex;
        }

        /// <summary>
        /// Gets or sets the index value of screen to change.
        /// </summary>
        public int ScreenIndex { get; set; }
    }
}
