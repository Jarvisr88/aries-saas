namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Windows;

    public static class DependencyObjectPropertyHelper
    {
        public static bool IsPropertyAssigned(DependencyObject o, DependencyProperty property) => 
            o.ReadLocalValue(property) != DependencyProperty.UnsetValue;
    }
}

