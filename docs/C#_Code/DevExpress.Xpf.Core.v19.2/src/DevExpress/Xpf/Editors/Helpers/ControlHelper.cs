namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public static class ControlHelper
    {
        public static readonly DependencyProperty IsFocusedProperty;
        public static readonly DependencyProperty ShowFocusedStateProperty;
        public static readonly DependencyProperty IsReadOnlyProperty;

        static ControlHelper()
        {
            Type ownerType = typeof(ControlHelper);
            IsFocusedProperty = DependencyPropertyManager.RegisterAttached("IsFocused", typeof(bool), typeof(ControlHelper), new PropertyMetadata(false));
            ShowFocusedStateProperty = DependencyPropertyManager.RegisterAttached("ShowFocusedState", typeof(bool), ownerType, new PropertyMetadata(new PropertyChangedCallback(ControlHelper.PropertyChangedShowFocusedState)));
            IsReadOnlyProperty = DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), ownerType);
        }

        public static bool GetIsFocused(DependencyObject obj) => 
            (bool) obj.GetValue(IsFocusedProperty);

        public static bool GetIsReadOnly(DependencyObject d) => 
            (bool) d.GetValue(IsReadOnlyProperty);

        public static bool GetShowFocusedState(DependencyObject obj) => 
            (bool) obj.GetValue(ShowFocusedStateProperty);

        private static void PropertyChangedShowFocusedState(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((FrameworkElement) d, ((bool) e.NewValue) ? "InternalFocused" : "InternalUnfocused", true);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static void SetIsReadOnly(DependencyObject d, bool value)
        {
            d.SetValue(IsReadOnlyProperty, value);
        }

        public static void SetShowFocusedState(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowFocusedStateProperty, value);
        }
    }
}

