// -----------------------------------------------------------------------
// <copyright file="DogViewModel.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Prism.Mvvm;

    /// <summary>
    /// View model for a dog.
    /// </summary>
    internal class DogViewModel : BindableBase
    {
        private readonly double areaWidth;
        private readonly BallViewModel ball;

        private Dictionary<ImageId, BitmapImage> dogImages;
        private ImageSource currentImage;

        private ImageId currentImageId;
        private double currentImageTimer;

        private StateId currentState;
        private double waitRespawn;

        private int direction;
        private double left;
        private double speedX;

        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="DogViewModel"/> class.
        /// </summary>
        /// <param name="areaWidth">Width of the window.</param>
        /// <param name="ball">The instance of a ball to chase.</param>
        public DogViewModel(double areaWidth, BallViewModel ball)
        {
            this.areaWidth = areaWidth;
            this.ball = ball;

            this.dogImages = new Dictionary<ImageId, BitmapImage>();
            this.dogImages.Add(ImageId.RunNormal0, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_00.png")));
            this.dogImages.Add(ImageId.RunNormal1, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_01.png")));
            this.dogImages.Add(ImageId.RunNormal2, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_02.png")));
            this.dogImages.Add(ImageId.RunNormal3, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_03.png")));
            this.dogImages.Add(ImageId.RunHeadDown0, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_10.png")));
            this.dogImages.Add(ImageId.RunHeadDown1, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_11.png")));
            this.dogImages.Add(ImageId.RunHeadDown2, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_12.png")));
            this.dogImages.Add(ImageId.RunHeadDown3, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_13.png")));
            this.dogImages.Add(ImageId.RunFetched0, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_20.png")));
            this.dogImages.Add(ImageId.RunFetched1, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_21.png")));
            this.dogImages.Add(ImageId.RunFetched2, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_22.png")));
            this.dogImages.Add(ImageId.RunFetched3, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_23.png")));
            this.dogImages.Add(ImageId.Turn0, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_turn_00.png")));
            this.dogImages.Add(ImageId.Turn1, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_turn_01.png")));
            this.dogImages.Add(ImageId.TurnFetched0, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_turn_20.png")));
            this.dogImages.Add(ImageId.TurnFetched1, new BitmapImage(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/dog_turn_21.png")));

            this.currentImage = this.dogImages[ImageId.RunNormal0];
            this.currentImageId = ImageId.RunNormal0;
            this.currentImageTimer = 0.0;

            this.currentState = StateId.Chasing;
            this.waitRespawn = 0;

            this.direction = 1;
            this.left = 20;
            this.speedX = 0;

            this.random = new Random();
        }

        private enum StateId
        {
            Chasing,
            TurningWithBall,
            ReturningWithBall,
        }

        private enum ImageId
        {
            RunNormal0,
            RunNormal1,
            RunNormal2,
            RunNormal3,
            RunHeadDown0,
            RunHeadDown1,
            RunHeadDown2,
            RunHeadDown3,
            RunFetched0,
            RunFetched1,
            RunFetched2,
            RunFetched3,
            Turn0,
            Turn1,
            TurnFetched0,
            TurnFetched1,
        }

        /// <summary>
        /// Gets or sets the current sprite image.
        /// </summary>
        public ImageSource CurrentImage
        {
            get => this.currentImage;
            set => this.SetProperty(ref this.currentImage, value);
        }

        /// <summary>
        /// Gets a value indicating whether the ball is catched.
        /// </summary>
        public bool IsBallCatched => this.currentState == StateId.ReturningWithBall || this.currentState == StateId.TurningWithBall;

        /// <summary>
        /// Gets or sets the direction of the dog.
        /// The value is 1 when facing right, and -1 when facing left.
        /// </summary>
        public int Direction
        {
            get => this.direction;
            set => this.SetProperty(ref this.direction, value);
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
        /// Update method.
        /// </summary>
        /// <param name="interval">Interval time from previous frame.</param>
        public void Update(double interval)
        {
            if (this.IsBallCatched)
            {
                this.UpdatePositionFetched(interval);
                this.UpdateSpriteFetched(interval);
            }
            else
            {
                this.UpdatePositionNormal(interval);
                this.UpdateSpriteNormal(interval);
            }
        }

        private void UpdatePositionNormal(double interval)
        {
            var kick = false;

            var diffX = this.ball.Left - this.left;
            if (Math.Abs(diffX) < 16)
            {
                var catchProbability = this.Direction > 0 ? 0.3 : 0.7;
                if (this.GetRandomValue(0.0, 1.0) < catchProbability)
                {
                    // catch
                    this.SetState(StateId.TurningWithBall);
                    this.ball.SpeedX = 0;
                    this.ball.SpeedY = 0;

                    this.speedX *= 0.2;
                }
                else
                {
                    // kick
                    this.speedX *= 0.25;
                    kick = true;
                }
            }
            else
            {
                // run toward ball
                var sign = Math.Sign(diffX);
                this.speedX += sign * 240 * interval;
                if (sign > 0)
                {
                    this.speedX = Math.Max(0, Math.Min(this.speedX, 160));
                }
                else
                {
                    this.speedX = Math.Max(-160, Math.Min(this.speedX, 0));
                }
            }

            var left = this.left + (this.speedX * interval);
            this.Left = left;

            // kick
            if (kick)
            {
                this.ball.Left = left + (this.Direction * 16);
                this.ball.SpeedX = this.GetRandomValue(160, 240) * this.Direction;
                this.ball.SpeedY = 40;
            }
        }

        private void UpdatePositionFetched(double interval)
        {
            if (this.waitRespawn > 0)
            {
                this.waitRespawn -= interval;
                if (this.waitRespawn <= 0)
                {
                    // respawn
                    this.waitRespawn = 0;

                    if (this.GetRandomValue(0.0, 1.0) < 0.15)
                    {
                        // spawn in right side (relatively rare case)
                        this.Left = this.areaWidth + 100;
                        this.Direction = -1;
                        this.SetState(StateId.Chasing);
                        this.ball.Left = this.Left - 60;
                        this.ball.SpeedX = -250;
                        this.ball.SpeedY = 40;
                    }
                    else
                    {
                        // spawn in left side
                        this.Left = -100;
                        this.Direction = 1;
                        this.SetState(StateId.Chasing);
                        this.ball.Left = this.Left + 60;
                        this.ball.SpeedX = 200;
                        this.ball.SpeedY = 40;
                    }
                }
            }
            else
            {
                if (this.currentState == StateId.TurningWithBall)
                {
                    // turning
                    this.speedX -= this.speedX * 5.0 * interval;
                }
                else if (this.currentState == StateId.ReturningWithBall)
                {
                    // return
                    this.speedX += this.Direction * 200 * interval;
                    if (this.Direction > 0)
                    {
                        this.speedX = Math.Max(0, Math.Min(this.speedX, 160));
                    }
                    else
                    {
                        this.speedX = Math.Max(-160, Math.Min(this.speedX, 0));
                    }
                }
                else
                {
                    throw new InvalidOperationException("invalid state.");
                }

                var left = this.left + (this.speedX * interval);
                this.Left = left;

                // reach end
                if (this.Left < -64)
                {
                    this.waitRespawn = this.GetRandomValue(3.0, 10.0);
                }
                else if (this.Left > this.areaWidth + 300)
                {
                    this.waitRespawn = 1.0;
                }
            }
        }

        private void UpdateSpriteNormal(double interval)
        {
            const double RunInterval = 0.125;

            this.currentImageTimer += interval;

            // run
            var diffX = this.ball.Left - this.left;
            if (Math.Abs(diffX) < 40)
            {
                // run when close
                if (this.currentImageTimer >= RunInterval)
                {
                    if (this.currentImageId == ImageId.RunNormal0 ||
                        this.currentImageId == ImageId.RunHeadDown0)
                    {
                        this.currentImageId = ImageId.RunHeadDown1;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal1 ||
                        this.currentImageId == ImageId.RunHeadDown1)
                    {
                        this.currentImageId = ImageId.RunHeadDown2;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal2 ||
                        this.currentImageId == ImageId.RunHeadDown2)
                    {
                        this.currentImageId = ImageId.RunHeadDown3;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal3 ||
                        this.currentImageId == ImageId.RunHeadDown3)
                    {
                        this.currentImageId = ImageId.RunHeadDown0;
                        this.currentImageTimer -= RunInterval;
                    }
                    else
                    {
                        this.currentImageId = ImageId.RunHeadDown0;
                        this.currentImageTimer = 0;
                    }
                }
            }
            else
            {
                // normal run
                if (this.currentImageTimer >= RunInterval)
                {
                    if (this.currentImageId == ImageId.RunNormal0 ||
                        this.currentImageId == ImageId.RunHeadDown0)
                    {
                        this.currentImageId = ImageId.RunNormal1;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal1 ||
                        this.currentImageId == ImageId.RunHeadDown1)
                    {
                        this.currentImageId = ImageId.RunNormal2;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal2 ||
                        this.currentImageId == ImageId.RunHeadDown2)
                    {
                        this.currentImageId = ImageId.RunNormal3;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunNormal3 ||
                        this.currentImageId == ImageId.RunHeadDown3)
                    {
                        this.currentImageId = ImageId.RunNormal0;
                        this.currentImageTimer -= RunInterval;
                    }
                    else
                    {
                        this.currentImageId = ImageId.RunNormal0;
                        this.currentImageTimer = 0;
                    }
                }
            }

            this.CurrentImage = this.dogImages[this.currentImageId];
        }

        private void UpdateSpriteFetched(double interval)
        {
            const double RunInterval = 0.15;

            this.currentImageTimer += interval;

            if (this.currentState == StateId.TurningWithBall)
            {
                // turning
                if (this.currentImageId == ImageId.TurnFetched0)
                {
                    if (this.currentImageTimer >= 0.2)
                    {
                        this.currentImageId = ImageId.TurnFetched1;
                        this.currentImageTimer = 0;
                    }
                }
                else if (this.currentImageId == ImageId.TurnFetched1)
                {
                    if (this.currentImageTimer >= 0.2)
                    {
                        this.currentImageId = ImageId.RunFetched0;
                        this.currentImageTimer = 0;
                        this.SetState(StateId.ReturningWithBall);
                        this.Direction *= -1;
                    }
                }
                else
                {
                    this.currentImageId = ImageId.TurnFetched0;
                    this.currentImageTimer = 0;
                }
            }
            else if (this.currentState == StateId.ReturningWithBall)
            {
                // run toward left
                if (this.currentImageTimer >= RunInterval)
                {
                    if (this.currentImageId == ImageId.RunFetched0)
                    {
                        this.currentImageId = ImageId.RunFetched1;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunFetched1)
                    {
                        this.currentImageId = ImageId.RunFetched2;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunFetched2)
                    {
                        this.currentImageId = ImageId.RunFetched3;
                        this.currentImageTimer -= RunInterval;
                    }
                    else if (this.currentImageId == ImageId.RunFetched3)
                    {
                        this.currentImageId = ImageId.RunFetched0;
                        this.currentImageTimer -= RunInterval;
                    }
                    else
                    {
                        this.currentImageId = ImageId.RunFetched0;
                        this.currentImageTimer = 0;
                    }
                }
            }
            else
            {
                this.currentImageId = ImageId.RunNormal0;
                this.currentImageTimer = 0;
            }

            this.CurrentImage = this.dogImages[this.currentImageId];
        }

        private void SetState(StateId state)
        {
            this.currentState = state;
            this.RaisePropertyChanged(nameof(this.IsBallCatched));
        }

        private double GetRandomValue(double minValue, double maxValue)
        {
            return (this.random.NextDouble() * (maxValue - minValue)) + minValue;
        }

        private int GetRandomValue(int minValue, int maxValue)
        {
            return this.random.Next(minValue, maxValue);
        }
    }
}
