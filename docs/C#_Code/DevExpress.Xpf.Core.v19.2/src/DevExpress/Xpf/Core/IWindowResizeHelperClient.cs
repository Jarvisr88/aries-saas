namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface IWindowResizeHelperClient
    {
        void ActivePartMouseDown(object sender, MouseButtonEventArgs e);
        FrameworkElement GetVisualByName(string name);
    }
}

