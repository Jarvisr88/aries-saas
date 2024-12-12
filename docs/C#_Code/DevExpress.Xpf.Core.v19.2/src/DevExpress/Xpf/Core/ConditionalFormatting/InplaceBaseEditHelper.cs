namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Windows;

    public class InplaceBaseEditHelper
    {
        public static readonly DependencyProperty EditSettingsProperty = DependencyProperty.RegisterAttached("EditSettings", typeof(BaseEditSettings), typeof(InplaceBaseEditHelper), new PropertyMetadata(null, new PropertyChangedCallback(InplaceBaseEditHelper.OnEditSettingsChanged)));

        public static BaseEditSettings GetEditSettings(InplaceBaseEdit obj) => 
            (BaseEditSettings) obj.GetValue(EditSettingsProperty);

        private static void OnEditSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((InplaceBaseEdit) d).SetSettings((BaseEditSettings) e.NewValue);
        }

        public static void SetEditSettings(InplaceBaseEdit obj, BaseEditSettings value)
        {
            obj.SetValue(EditSettingsProperty, value);
        }
    }
}

