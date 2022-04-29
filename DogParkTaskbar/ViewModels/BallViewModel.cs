// -----------------------------------------------------------------------
// <copyright file="BallViewModel.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.ViewModels
{
    using System;

    using Prism.Mvvm;

    /// <summary>
    /// View model for a ball.
    /// </summary>
    internal class BallViewModel : BindableBase
    {
        private double left;
        private double bottom;

        private double speedX;
        private double speedY;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallViewModel"/> class.
        /// </summary>
        public BallViewModel()
        {
            this.left = 48;
            this.bottom = 16;
            this.speedX = 250;
            this.speedY = 20;
        }

        /// <summary>
        /// Gets or sets the left value of the position.
        /// </summary>
        public double Left
        {
            get => this.left;
            set => this.SetProperty(ref this.left, value);
        }

        /// <summary>
        /// Gets or sets the bottom value of the position.
        /// </summary>
        public double Bottom
        {
            get => this.bottom;
            set => this.SetProperty(ref this.bottom, value);
        }

        /// <summary>
        /// Gets or sets the x value of the speed.
        /// </summary>
        public double SpeedX
        {
            get => this.speedX;
            set => this.SetProperty(ref this.speedX, value);
        }

        /// <summary>
        /// Gets or sets the y value of the speed.
        /// </summary>
        public double SpeedY
        {
            get => this.speedY;
            set => this.SetProperty(ref this.speedY, value);
        }

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="interval">Interval time from previous frame.</param>
        public void Update(double interval)
        {
            this.speedX -= this.speedX * interval * 0.25;
            this.speedY -= 100.0 * interval;

            if (Math.Abs(this.speedY) < 1.0)
            {
                this.speedY = 0.0;
            }

            var left = this.left + (this.speedX * interval);
            var bottom = this.bottom + (this.speedY * interval);

            if (bottom <= 0.0)
            {
                bottom = -bottom;
                if (this.speedY < 0.0)
                {
                    this.speedY *= -0.8;
                    this.speedX *= 0.9;
                }
            }

            this.Left = left;
            this.Bottom = bottom;
        }
    }
}
