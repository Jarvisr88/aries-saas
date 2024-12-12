namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;

    public interface IDesignerPropertiesService
    {
        bool GetIsInDesignMode(DependencyObject element);
    }
}

