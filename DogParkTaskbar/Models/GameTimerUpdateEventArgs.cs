// -----------------------------------------------------------------------
// <copyright file="GameTimerUpdateEventArgs.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Models
{
    using System;

    /// <summary>
    /// EventArgs for GameTimer.Update.
    /// </summary>
    internal class GameTimerUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameTimerUpdateEventArgs"/> class.
        /// </summary>
        /// <param name="elapsed">Elapsed time from previous frame.</param>
        public GameTimerUpdateEventArgs(TimeSpan elapsed)
        {
            this.Elapsed = elapsed;
        }

        /// <summary>
        /// Gets the elapsed time from previous frame.
        /// </summary>
        public TimeSpan Elapsed { get; }
    }
}
