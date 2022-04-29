// -----------------------------------------------------------------------
// <copyright file="GameTimer.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Models
{
    using System;
    using System.Diagnostics;
    using System.Windows.Media;

    /// <summary>
    /// Timer class to manage update frames.
    /// </summary>
    internal class GameTimer
    {
        private Stopwatch stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTimer"/> class.
        /// </summary>
        public GameTimer()
        {
            this.stopwatch = new Stopwatch();

            CompositionTarget.Rendering += this.OnUpdate;
        }

        /// <summary>
        /// Event called on each frame.
        /// </summary>
        public event EventHandler<GameTimerUpdateEventArgs> Updated;

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            this.stopwatch.Start();
        }

        private void OnUpdate(object sender, EventArgs ev)
        {
            var elapsed = this.stopwatch.Elapsed;
            this.stopwatch.Restart();

            var args = new GameTimerUpdateEventArgs(elapsed);
            this.Updated?.Invoke(this, args);
        }
    }
}
