// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar
{
    using System;
    using System.Drawing;
    using System.Windows;

    using DogParkTaskbar.Views;
    using DogParkTaskbar.Windows.Forms;

    using Prism.Ioc;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App
    {
        private AppNotifyIcon notifyIcon;

        /// <inheritdoc/>
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        /// <inheritdoc/>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs ev)
        {
            var iconStream = GetResourceStream(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/icon_16x16.ico")).Stream;
            var icon = new Icon(iconStream);

            // TODO: make app icon
            this.notifyIcon = new AppNotifyIcon(icon);
            this.notifyIcon.Closed += (s_, e_) =>
            {
                this.Shutdown();
            };
            this.notifyIcon.Text = DogParkTaskbar.Resources.Strings.TrayToolTip;
            this.notifyIcon.IsVisible = true;

            base.OnStartup(ev);
        }

        /// <inheritdoc/>
        protected override void OnExit(ExitEventArgs ev)
        {
            this.notifyIcon.Dispose();

            base.OnExit(ev);
        }
    }
}
