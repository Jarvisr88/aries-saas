namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class ContentPresenterExtensions
    {
        public static FrameworkElement GetUIElement(this ContentPresenter presenter) => 
            VisualTreeHelper.GetChild(presenter, 0) as FrameworkElement;
    }
}

