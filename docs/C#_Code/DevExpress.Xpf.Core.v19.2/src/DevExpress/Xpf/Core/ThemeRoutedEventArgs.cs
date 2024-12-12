namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ThemeRoutedEventArgs : RoutedEventArgs
    {
        public ThemeRoutedEventArgs(string themeName)
        {
            this.ThemeName = themeName;
        }

        public string ThemeName { get; private set; }
    }
}

