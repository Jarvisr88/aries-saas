namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    internal static class BarManagerPropertyHelper
    {
        public static void ClearBarManager(DependencyObject dObj)
        {
            dObj.ClearValue(BarManager.BarManagerProperty);
            dObj.ClearValue(BarManager.BarManagerInfoProperty);
        }

        public static BarManager GetBarManager(DependencyObject dObj) => 
            BarManager.GetBarManager(dObj);

        public static void SetBarManager(DependencyObject dObj, BarManager manager)
        {
            BarManager.SetBarManager(dObj, manager);
        }
    }
}

