namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public interface IWindowWrapper<T> : ITargetWrapper<T> where T: DependencyObject
    {
        event EventHandler Activated;

        event EventHandler Closed;

        event CancelEventHandler Closing;

        void Activate();
        void Close();
        void Show();
        MessageBoxResult ShowDialog();

        object DataContext { get; set; }

        object Content { get; set; }

        DataTemplate ContentTemplate { get; set; }

        DataTemplateSelector ContentTemplateSelector { get; set; }
    }
}

