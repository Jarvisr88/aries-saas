namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class XamlThemeProvider : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public XamlThemeProvider()
        {
            this.ThemeName = ThemeManager.ActualApplicationThemeName;
            ThemeManager.AddThemeChangedHandler(Application.Current.MainWindow, new ThemeChangedRoutedEventHandler(this.ThemeManager_ThemeChanged));
        }

        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ThemeManager_ThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            this.ThemeName = ThemeManager.ActualApplicationThemeName;
            this.RaisePropertyChanged("ThemeName");
        }

        public string ThemeName { get; private set; }
    }
}

