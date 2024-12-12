namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class ApplicationThemeNameChangedEventArgs : EventArgs
    {
        public ApplicationThemeNameChangedEventArgs(string themeName)
        {
            this.ThemeName = themeName;
        }

        public string ThemeName { get; private set; }
    }
}

