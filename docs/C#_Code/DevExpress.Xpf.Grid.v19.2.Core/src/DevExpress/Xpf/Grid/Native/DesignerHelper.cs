namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    internal static class DesignerHelper
    {
        public static bool GetValue(DependencyObject source, bool currentValue, bool designerValue) => 
            !DesignerProperties.GetIsInDesignMode(source) ? currentValue : designerValue;
    }
}

