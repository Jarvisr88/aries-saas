namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    internal static class WindowServiceHelper
    {
        public static readonly DependencyProperty IWindowServiceProperty = DependencyProperty.RegisterAttached("IWindowService", typeof(object), typeof(DevExpress.Xpf.Docking.WindowServiceHelper), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DevExpress.Xpf.Docking.WindowServiceHelper.OnIWindowServiceChanged)));
        public static readonly DependencyProperty RootWindowProperty = DependencyProperty.RegisterAttached("RootWindow", typeof(Window), typeof(DevExpress.Xpf.Docking.WindowServiceHelper));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty _IWindowServiceProperty = (typeof(Window).GetField("IWindowServiceProperty", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as DependencyProperty);

        public static object GetIWindowService(DependencyObject target) => 
            target.GetValue(IWindowServiceProperty);

        public static Window GetRootWindow(DependencyObject target) => 
            (Window) target.GetValue(RootWindowProperty);

        public static Window GetWindow(DependencyObject dObj)
        {
            Window target = Window.GetWindow(dObj);
            return ((target != null) ? (GetRootWindow(target) ?? target) : null);
        }

        private static void OnIWindowServiceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnIWindowServiceChanged(o, e.OldValue, e.NewValue);
        }

        private static void OnIWindowServiceChanged(DependencyObject o, object oldValue, object newValue)
        {
            if (_IWindowServiceProperty != null)
            {
                if (newValue != null)
                {
                    o.SetValue(_IWindowServiceProperty, newValue);
                }
                else
                {
                    o.ClearValue(_IWindowServiceProperty);
                }
            }
        }

        public static void SetIWindowService(DependencyObject target, object value)
        {
            target.SetValue(IWindowServiceProperty, value);
        }

        public static void SetRootWindow(DependencyObject target, Window value)
        {
            target.SetValue(RootWindowProperty, value);
        }
    }
}

