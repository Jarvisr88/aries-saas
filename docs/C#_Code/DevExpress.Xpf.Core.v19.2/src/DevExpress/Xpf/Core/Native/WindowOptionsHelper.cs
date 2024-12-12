namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Security;
    using System.Windows;

    internal static class WindowOptionsHelper
    {
        private const int flags = 0x13;

        private static IntPtr GetWindowHandle(Window window);
        public static void SetShowOverPopups(Window window);
        [SecuritySafeCritical]
        private static void SetWindowNonTopmost(IntPtr windowHwnd);
        [SecuritySafeCritical]
        private static void SetWindowNonTopmost(object sender);
        [SecuritySafeCritical]
        private static void SetWindowNonTopmost(Window window);
        private static void SubscribeEvents(Window window);
        private static void UnsubscribeEvents(Window window);
        private static void WindowOnActivated(object sender, EventArgs e);
        private static void WindowOnClosed(object sender, EventArgs e);
        private static void WindowOnDeactivated(object sender, EventArgs e);
        private static void WindowOnLoaded(object sender, RoutedEventArgs e);
    }
}

