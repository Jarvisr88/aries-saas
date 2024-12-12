namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal interface IGeneratorHost
    {
        void ClearContainer(DependencyObject container, object item);
        DependencyObject GenerateContainerForItem(object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector);
        DependencyObject GenerateContainerForItem(object item, int index, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector);
        DependencyObject LinkContainerToItem(DependencyObject container, object item, DataTemplate itemTemplate, DataTemplateSelector itemTemplateSelector);
    }
}

