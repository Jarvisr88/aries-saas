namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Security;
    using System.Windows;
    using System.Windows.Media;

    public static class WindowBlurBehindHelper
    {
        [SecuritySafeCritical]
        public static void DisableBlurBehind(Window window);
        [SecuritySafeCritical]
        public static void EnableBlurBehind(Window window, Color accentColor);
        [SecuritySafeCritical]
        public static void EnableGradient(Window window, Color accentColor);
        [SecuritySafeCritical]
        private static void SetAccent(Window window, Color accentColor, AccentState accentState, uint accentFlags);

        public static bool IsBlurBehindSupported { get; }
    }
}

