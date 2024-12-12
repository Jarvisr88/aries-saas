namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class WindowServiceHelper
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IWindowServiceProperty;

        static WindowServiceHelper();
        public static object GetIWindowService(DependencyObject dObj);
        public static void SetIWindowService(DependencyObject dObj, object value);
    }
}

