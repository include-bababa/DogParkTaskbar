// -----------------------------------------------------------------------
// <copyright file="ScreenController.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Controllers
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Manages screens.
    /// </summary>
    public class ScreenController : IScreenController
    {
        private int currentScreen;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenController"/> class.
        /// </summary>
        public ScreenController()
        {
            var index = 0;
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Primary)
                {
                    this.currentScreen = index;
                    break;
                }

                ++index;
            }
        }

        /// <inheritdoc />
        public event EventHandler<ChangeScreenEventArgs> ScreenChanged;

        /// <inheritdoc />
        public int CurrentScreen
        {
            get => this.currentScreen;
            set
            {
                if (this.currentScreen == value)
                {
                    return;
                }

                this.currentScreen = value;
                this.ScreenChanged?.Invoke(this, new ChangeScreenEventArgs(value));
            }
        }
    }
}
