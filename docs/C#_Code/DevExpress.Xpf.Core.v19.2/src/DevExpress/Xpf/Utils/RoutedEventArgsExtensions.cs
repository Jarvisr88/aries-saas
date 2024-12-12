namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class RoutedEventArgsExtensions
    {
        public static bool GetHandled(this RoutedEventArgs e) => 
            e.Handled;

        public static void SetHandled(this RoutedEventArgs e, bool value)
        {
            e.Handled = value;
        }
    }
}

