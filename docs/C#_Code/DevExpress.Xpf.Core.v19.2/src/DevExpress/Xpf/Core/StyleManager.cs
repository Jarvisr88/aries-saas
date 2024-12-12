namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class StyleManager
    {
        public static readonly DependencyProperty ApplyApplicationThemeProperty = DependencyProperty.RegisterAttached("ApplyApplicationTheme", typeof(bool), typeof(StyleManager), new PropertyMetadata());

        public static bool GetApplyApplicationTheme(FrameworkElement element) => 
            (bool) element.GetValue(ApplyApplicationThemeProperty);

        public static void SetApplyApplicationTheme(FrameworkElement element, bool value)
        {
            element.SetValue(ApplyApplicationThemeProperty, value);
        }
    }
}

