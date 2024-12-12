namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class WindowShowHelper
    {
        private static readonly Func<Window, bool> get_IsSourceWindowNull;

        static WindowShowHelper();
        private static Theme GetThemeOfDefault(FrameworkElement element);
        public static T InitializeThemedWindowFromOwner<T>(FrameworkElement owner) where T: Window;
        public static T InitializeThemedWindowFromOwner<T>(FrameworkElement owner, string title) where T: Window;
        internal static void InitializeThemedWindowFromOwner(this Window window, FrameworkElement owner);
        internal static void InitializeThemedWindowFromOwner(this Window window, FrameworkElement owner, string title);
        public static void SetOwner(this Window window, FrameworkElement owner);
        internal static void SetThemeFromOwner(this Window window, FrameworkElement owner);
        private static void SetThemeWithOptions(Window window, FrameworkElement owner);
    }
}

