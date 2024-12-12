namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public static class BooleanExportSettings
    {
        public static readonly DependencyProperty BooleanValueProperty = DependencyPropertyManager.RegisterAttached("BooleanValue", typeof(bool?), typeof(BooleanExportSettings), new PropertyMetadata(ExportSettingDefaultValue.BooleanValue));
        public static readonly DependencyProperty CheckTextProperty = DependencyPropertyManager.RegisterAttached("CheckText", typeof(string), typeof(BooleanExportSettings), new PropertyMetadata(ExportSettingDefaultValue.CheckText));

        public static bool? GetBooleanValue(DependencyObject obj) => 
            (bool?) obj.GetValue(BooleanValueProperty);

        public static string GetCheckText(DependencyObject obj) => 
            (string) obj.GetValue(CheckTextProperty);

        public static void SetBooleanValue(DependencyObject obj, bool? value)
        {
            obj.SetValue(BooleanValueProperty, value);
        }

        public static void SetCheckText(DependencyObject obj, string value)
        {
            obj.SetValue(CheckTextProperty, value);
        }
    }
}

