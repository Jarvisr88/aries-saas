namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class ChromePropertyHelper
    {
        private static void CheckDObjTypeHelper<TDependencyObject>(DependencyObject dObj);
        public static TResult GetProperty<TResult, TDependencyObject>(DependencyObject dObj, DependencyProperty property);
        public static void SetProperty<TResult, TDependencyObject>(DependencyObject dObj, TResult value, DependencyProperty property);
    }
}

