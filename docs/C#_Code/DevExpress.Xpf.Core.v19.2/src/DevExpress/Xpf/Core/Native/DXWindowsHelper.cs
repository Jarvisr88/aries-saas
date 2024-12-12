namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class DXWindowsHelper
    {
        public const string DXWindow = "DXWindow";
        public const string ThemedWindow = "ThemedWindow";
        public const string DXTabbedWindow = "DXTabbedWindow";
        public const string DXRibbonWindow = "DXRibbonWindow";
        public static readonly DependencyProperty WindowProperty;
        public static readonly DependencyProperty WindowKindProperty;

        static DXWindowsHelper();
        public static Window GetWindow(DependencyObject element);
        public static string GetWindowKind(DependencyObject element);
        public static void SetWindow(DependencyObject element, Window value);
        public static void SetWindowKind(DependencyObject element, string value);
    }
}

