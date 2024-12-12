namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IContentPresenterWrapper<T> : ITargetWrapper<T> where T: DependencyObject
    {
        object Content { get; set; }

        DataTemplate ContentTemplate { get; set; }

        DataTemplateSelector ContentTemplateSelector { get; set; }
    }
}

