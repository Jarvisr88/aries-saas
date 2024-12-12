namespace DevExpress.Xpf.Core
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class FocusHelper2
    {
        public static readonly DependencyProperty FocusableProperty;

        static FocusHelper2()
        {
            FocusableProperty = DependencyProperty.RegisterAttached("Focusable", typeof(bool), typeof(FocusHelper2), new PropertyMetadata(true, (d, e) => PropertyChangedFocusable(d, e)));
        }

        public static bool GetFocusable(DependencyObject o) => 
            (bool) o.GetValue(FocusableProperty);

        private static void PropertyChangedFocusable(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PropertyInfo property = d.GetType().GetProperty("Focusable", BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                property.SetValue(d, e.NewValue, null);
            }
        }

        public static void SetFocusable(DependencyObject o, bool value)
        {
            o.SetValue(FocusableProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FocusHelper2.<>c <>9 = new FocusHelper2.<>c();

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                FocusHelper2.PropertyChangedFocusable(d, e);
            }
        }
    }
}

