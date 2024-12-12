namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    public class MVVMHelper
    {
        public static readonly DependencyProperty TargetNameProperty;
        public static readonly DependencyProperty LayoutAdapterProperty;

        static MVVMHelper()
        {
            DependencyPropertyRegistrator<MVVMHelper> registrator = new DependencyPropertyRegistrator<MVVMHelper>();
            registrator.RegisterAttached<string>("TargetName", ref TargetNameProperty, null, new PropertyChangedCallback(MVVMHelper.OnTargetNameChanged), null);
            registrator.RegisterAttached<ILayoutAdapter>("LayoutAdapter", ref LayoutAdapterProperty, null, null, null);
        }

        public static ILayoutAdapter GetLayoutAdapter(DependencyObject target) => 
            (ILayoutAdapter) target.GetValue(LayoutAdapterProperty);

        public static string GetTargetName(DependencyObject target) => 
            (string) target.GetValue(TargetNameProperty);

        internal static string GetTargetNameForItem(object target) => 
            !(target is IMVVMDockingProperties) ? ((target is DependencyObject) ? GetTargetName((DependencyObject) target) : null) : ((IMVVMDockingProperties) target).TargetName;

        private static void OnTargetNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnTargetNameChanged(o, (string) e.OldValue, (string) e.NewValue);
        }

        private static void OnTargetNameChanged(DependencyObject o, string oldValue, string newValue)
        {
        }

        public static void SetLayoutAdapter(DependencyObject target, ILayoutAdapter value)
        {
            target.SetValue(LayoutAdapterProperty, value);
        }

        public static void SetTargetName(DependencyObject target, string value)
        {
            target.SetValue(TargetNameProperty, value);
        }
    }
}

