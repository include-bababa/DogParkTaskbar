// -----------------------------------------------------------------------
// <copyright file="TaskbarHelper.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Windows
{
    using System;
    using System.Runtime.InteropServices;

    using DogParkTaskbar.Windows.NativeMethods;

    /// <summary>
    /// Helper methods related to taskbar.
    /// </summary>
    internal static class TaskbarHelper
    {
        /// <summary>
        /// Identifies the position of taskbar.
        /// </summary>
        public enum TaskbarPosition
        {
            Bottom,
            Left,
            Right,
            Top,
        }

        /// <summary>
        /// Identifies the direction of taskbar.
        /// </summary>
        public enum TaskbarDirection
        {
            Horizontal,
            Vertical,
        }

        /// <summary>
        /// Gets taskbar area.
        /// </summary>
        /// <returns>Taskbar area.</returns>
        public static AppBar.RECT GetTaskbarRect()
        {
            var data = new AppBar.APPBARDATA()
            {
                cbSize = Marshal.SizeOf<AppBar.APPBARDATA>(),
            };
            AppBar.SHAppBarMessage(AppBar.ABM_GETTASKBARPOS, ref data);

            return data.rc;
        }

        /// <summary>
        /// Gets taskbar position.
        /// </summary>
        /// <returns>Taskbar position.</returns>
        public static TaskbarPosition GetTaskbarPosition()
        {
            var rect = GetTaskbarRect();

            var taskbarX = (rect.Left + rect.Right) / 2;
            var taskbarY = (rect.Top + rect.Bottom) / 2;

            var screenWidth = AppBar.GetSystemMetrics(AppBar.SM_CXSCREEN);
            var screenHeight = AppBar.GetSystemMetrics(AppBar.SM_CYSCREEN);

            var x = (float)taskbarX / screenWidth;
            var y = (float)taskbarY / screenHeight;

            if (x > y)
            {
                if ((1.0f - x) > y)
                {
                    return TaskbarPosition.Top;
                }
                else
                {
                    return TaskbarPosition.Right;
                }
            }
            else
            {
                if ((1.0f - x) > y)
                {
                    return TaskbarPosition.Left;
                }
                else
                {
                    return TaskbarPosition.Bottom;
                }
            }
        }

        /// <summary>
        /// Gets taskbar direction.
        /// </summary>
        /// <returns>Taskbar direction.</returns>
        public static TaskbarDirection GetTaskbarDirection()
        {
            switch (GetTaskbarPosition())
            {
                case TaskbarPosition.Top:
                case TaskbarPosition.Bottom:
                    return TaskbarDirection.Horizontal;
                case TaskbarPosition.Left:
                case TaskbarPosition.Right:
                    return TaskbarDirection.Vertical;
                default:
                    throw new InvalidOperationException("unknown position");
            }
        }
    }
}
