namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    public static class BaseEditFocusHelper
    {
        public static readonly DependencyProperty FocusOnLoadProperty = DependencyProperty.RegisterAttached("FocusOnLoad", typeof(bool), typeof(BaseEditFocusHelper), new PropertyMetadata(false, new PropertyChangedCallback(BaseEditFocusHelper.OnFocusOnLoadChanged)));

        public static bool GetFocusOnLoad(BaseEdit obj) => 
            (bool) obj.GetValue(FocusOnLoadProperty);

        private static void OnFocusOnLoadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                Interaction.GetBehaviors(d).Add(new FocusBehavior());
            }
        }

        public static void SetFocusOnLoad(BaseEdit obj, bool value)
        {
            obj.SetValue(FocusOnLoadProperty, value);
        }
    }
}

