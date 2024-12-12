namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Windows;

    public interface IThemeManager
    {
        void ClearThemeName(DependencyObject d);
        void SetThemeName(DependencyObject d, string value);
    }
}

