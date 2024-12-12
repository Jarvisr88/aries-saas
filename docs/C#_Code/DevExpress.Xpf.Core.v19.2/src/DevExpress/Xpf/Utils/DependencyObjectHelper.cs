namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;

    public static class DependencyObjectHelper
    {
        public static object GetCoerceValue(DependencyObject o, DependencyProperty p) => 
            o.GetValue(p);

        public static object GetValueWithInheritance(DependencyObject o, DependencyProperty p) => 
            GetCoerceValue(o, p);
    }
}

