namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IItemsControlWrapper<T> : ITargetWrapper<T> where T: DependencyObject
    {
        object ItemsSource { get; set; }

        DataTemplate ItemTemplate { get; set; }

        DataTemplateSelector ItemTemplateSelector { get; set; }
    }
}

