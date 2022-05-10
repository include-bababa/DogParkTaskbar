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

    using DogParkTaskbar.Controllers;
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
            containerRegistry.RegisterSingleton<IScreenController, ScreenController>();
        }

        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs ev)
        {
            base.OnStartup(ev);

            var screenController = this.Container.Resolve<IScreenController>();

            var iconStream = GetResourceStream(new Uri("pack://application:,,,/DogParkTaskbar;component/Resources/icon_16x16.ico")).Stream;
            var icon = new Icon(iconStream);

            this.notifyIcon = new AppNotifyIcon(icon, screenController.CurrentScreen);
            this.notifyIcon.ScreenChanged += this.NotifyIcon_ScreenChanged;
            this.notifyIcon.Closed += this.NotifyIcon_Closed;
            this.notifyIcon.Text = DogParkTaskbar.Resources.Strings.TrayToolTip;
            this.notifyIcon.IsVisible = true;
        }

        /// <inheritdoc/>
        protected override void OnExit(ExitEventArgs ev)
        {
            this.notifyIcon.Dispose();

            base.OnExit(ev);
        }

        private void NotifyIcon_ScreenChanged(object sender, Windows.Forms.ChangeScreenEventArgs ev)
        {
            var screenController = this.Container.Resolve<IScreenController>();
            screenController.CurrentScreen = ev.ScreenIndex;
        }

        private void NotifyIcon_Closed(object sender, EventArgs ev)
        {
            this.Shutdown();
        }
    }
}
