namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public interface INavigationItem : INotifyPropertyChanged, ICommandSource
    {
        string Header { get; }

        object DataContext { get; }

        DataTemplate PeekFormTemplate { get; }

        DataTemplateSelector PeekFormTemplateSelector { get; }
    }
}

