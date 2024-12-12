namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public interface IHwndHostEventListener
    {
        void OnMouseDown(DependencyObject hwndHost, MouseButtonEventArgs eventArgs);
    }
}

