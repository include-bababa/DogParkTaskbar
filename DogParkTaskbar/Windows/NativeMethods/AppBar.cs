// -----------------------------------------------------------------------
// <copyright file="AppBar.cs" company="Bababa">
// Copyright (c) Bababa. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace DogParkTaskbar.Windows.NativeMethods
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Native methods related to AppBar.
    /// </summary>
    internal static class AppBar
    {
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1310 // Field names should not contain underscore
        public const uint ABM_GETTASKBARPOS = 0x00000005;

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
#pragma warning restore SA1310 // Field names should not contain underscore

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter
#pragma warning restore SA1600 // Elements should be documented
    }
}
