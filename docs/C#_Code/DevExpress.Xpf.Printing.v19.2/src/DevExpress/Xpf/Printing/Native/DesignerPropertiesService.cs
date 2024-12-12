namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class DesignerPropertiesService : IDesignerPropertiesService
    {
        public bool GetIsInDesignMode(DependencyObject element) => 
            DesignerProperties.GetIsInDesignMode(element);
    }
}

