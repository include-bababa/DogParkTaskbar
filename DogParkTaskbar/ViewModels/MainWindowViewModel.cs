// -----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.ViewModels
{
    using System;
    using System.Windows.Forms;

    using DogParkTaskbar.Models;

    using Prism.Mvvm;

    /// <summary>
    /// View model for MainWindow.
    /// </summary>
    internal class MainWindowViewModel : BindableBase
    {
        private GameTimer gameTimer;

        private double top;
        private double left;
        private double width;
        private double height;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.UpdatePosition();

            this.Ball = new BallViewModel();
            this.Dog = new DogViewModel(this.Width, this.Ball);

            this.gameTimer = new GameTimer();
            this.gameTimer.Updated += this.OnUpdate;
            this.gameTimer.Start();
        }

        /// <summary>
        /// Gets or sets top position of the window.
        /// </summary>
        public double Top
        {
            get => this.top;
            set => this.SetProperty(ref this.top, value);
        }

        /// <summary>
        /// Gets or sets left position of the window.
        /// </summary>
        public double Left
        {
            get => this.left;
            set => this.SetProperty(ref this.left, value);
        }

        /// <summary>
        /// Gets or sets width of the window.
        /// </summary>
        public double Width
        {
            get => this.width;
            set => this.SetProperty(ref this.width, value);
        }

        /// <summary>
        /// Gets or sets height of the window.
        /// </summary>
        public double Height
        {
            get => this.height;
            set => this.SetProperty(ref this.height, value);
        }

        /// <summary>
        /// Gets ball instance.
        /// </summary>
        public BallViewModel Ball { get; }

        /// <summary>
        /// Gets dog instance.
        /// </summary>
        public DogViewModel Dog { get; }

        private void OnUpdate(object sender, GameTimerUpdateEventArgs ev)
        {
            var interval = (double)ev.Elapsed.Ticks / TimeSpan.TicksPerSecond;
            interval = Math.Max(0.001, Math.Min(interval, 0.1));

            this.Ball.Update(interval);
            this.Dog.Update(interval);
        }

        private void UpdatePosition()
        {
            // place at the bottom of the working area
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Left = workingArea.Left;
            this.Top = workingArea.Bottom - 32;
            this.Width = workingArea.Width;
            this.Height = 32;
        }
    }
}
