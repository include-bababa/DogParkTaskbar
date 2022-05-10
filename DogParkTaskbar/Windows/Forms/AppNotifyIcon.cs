// -----------------------------------------------------------------------
// <copyright file="AppNotifyIcon.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Manages application's notify icon.
    /// </summary>
    public class AppNotifyIcon : IDisposable
    {
        private NotifyIcon notifyIcon;
        private ToolStripMenuItem closeItem;

        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppNotifyIcon"/> class.
        /// </summary>
        /// <param name="icon">Icon shown in tray.</param>
        /// <param name="defaultScreenIndex"></param>
        public AppNotifyIcon(Icon icon, int defaultScreenIndex)
        {
            this.notifyIcon = new NotifyIcon()
            {
                Icon = icon,
            };

            var menu = new ContextMenuStrip();

            var screenSelectItem = new ToolStripMenuItem(Resources.Strings.TrayMenuPlacement);
            var index = 0;
            foreach (var screen in Screen.AllScreens)
            {
                var eventArgs = new ChangeScreenEventArgs(index);
                var screenItem = new ToolStripMenuItem(GetOrdinal(index + 1) + " " + Resources.Strings.TrayMenuScreen);
                screenItem.Checked = index == defaultScreenIndex;
                screenItem.Click += (s_, e_) =>
                {
                    foreach (var item in screenSelectItem.DropDownItems)
                    {
                        ((ToolStripMenuItem)item).Checked = item == screenItem;
                    }

                    this.ScreenChanged?.Invoke(this, eventArgs);
                };

                screenSelectItem.DropDownItems.Add(screenItem);
                ++index;
            }

            this.closeItem = new ToolStripMenuItem(Resources.Strings.TrayMenuClose);

            menu.Items.Add(screenSelectItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(this.closeItem);

            this.notifyIcon.ContextMenuStrip = menu;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AppNotifyIcon"/> class.
        /// </summary>
        ~AppNotifyIcon()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Occurs when the user clicks a <see cref="AppNotifyIcon"/>.
        /// </summary>
        public event EventHandler Clicked
        {
            add { this.notifyIcon.Click += value; }
            remove { this.notifyIcon.Click -= value; }
        }

        /// <summary>
        /// Occurs when the user selects "Close".
        /// </summary>
        public event EventHandler Closed
        {
            add { this.closeItem.Click += value; }
            remove { this.closeItem.Click -= value; }
        }

        /// <summary>
        /// Occurs when the selected screen changed.
        /// </summary>
        public event EventHandler<ChangeScreenEventArgs> ScreenChanged;

        /// <summary>
        /// Gets or sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.
        /// </summary>
        public string Text
        {
            get { return this.notifyIcon.Text; }
            set { this.notifyIcon.Text = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the icon is visible in the notification area of the taskbar.
        /// The default value is false.
        /// </summary>
        public bool IsVisible
        {
            get { return this.notifyIcon.Visible; }
            set { this.notifyIcon.Visible = value; }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Dispose managed objects.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                this.notifyIcon.Dispose();
                this.closeItem.Dispose();
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }

        private static string GetOrdinal(int num)
        {
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString() + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString() + "st";
                case 2:
                    return num.ToString() + "nd";
                case 3:
                    return num.ToString() + "rd";
            }

            return num.ToString() + "th";
        }
    }
}
