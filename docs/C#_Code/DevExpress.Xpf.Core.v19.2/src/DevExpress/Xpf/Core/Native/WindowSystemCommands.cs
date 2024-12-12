namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Input;

    public static class WindowSystemCommands
    {
        public static readonly DependencyProperty IsSpecialWindowProperty;

        static WindowSystemCommands();
        [SecurityCritical]
        private static void _PostSystemCommand(Window window, SystemCommandValues command);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public static void CloseWindow(Window window);
        public static bool GetIsSpecialWindow(DependencyObject element);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public static void MaximizeWindow(Window window);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public static void MinimizeWindow(Window window);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public static void RestoreWindow(Window window);
        public static void SetIsSpecialWindow(DependencyObject element, bool value);
        [SecuritySafeCritical, PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public static void ShowSystemMenu(Window window, Point screenLocation);
        [SecurityCritical]
        internal static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation);

        public static RoutedCommand CloseWindowCommand { get; private set; }

        public static RoutedCommand MaximizeWindowCommand { get; private set; }

        public static RoutedCommand MinimizeWindowCommand { get; private set; }

        public static RoutedCommand RestoreWindowCommand { get; private set; }

        public static RoutedCommand ShowSystemMenuCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WindowSystemCommands.<>c <>9;

            static <>c();
            internal void <.cctor>b__23_0(object sender, ExecutedRoutedEventArgs args);
            internal void <.cctor>b__23_1(object sender, ExecutedRoutedEventArgs args);
            internal void <.cctor>b__23_2(object sender, ExecutedRoutedEventArgs args);
            internal void <.cctor>b__23_3(object sender, ExecutedRoutedEventArgs args);
        }
    }
}

