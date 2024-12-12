namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class MVVMFocusBehavior : DependencyObject
    {
        public static readonly DependencyProperty IsFocusedProperty;

        static MVVMFocusBehavior()
        {
            Type ownerType = typeof(MVVMFocusBehavior);
            IsFocusedProperty = DependencyPropertyManager.RegisterAttached("IsFocused", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(MVVMFocusBehavior.IsFocusedPropertyChanged)));
        }

        public static bool GetIsFocused(DependencyObject d) => 
            (bool) d.GetValue(IsFocusedProperty);

        private static void IsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;
            if ((element != null) && ((bool) e.NewValue))
            {
                element.Focus();
            }
        }

        public static void SetIsFocused(DependencyObject d, bool focused)
        {
            d.SetValue(IsFocusedProperty, focused);
        }
    }
}

