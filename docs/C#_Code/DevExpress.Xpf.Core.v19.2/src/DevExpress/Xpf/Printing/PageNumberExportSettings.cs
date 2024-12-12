namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public static class PageNumberExportSettings
    {
        public static readonly DependencyProperty FormatProperty = DependencyPropertyManager.RegisterAttached("Format", typeof(string), typeof(PageNumberExportSettings), new PropertyMetadata(ExportSettingDefaultValue.PageNumberFormat));
        public static readonly DependencyProperty KindProperty = DependencyPropertyManager.RegisterAttached("Kind", typeof(PageNumberKind), typeof(PageNumberExportSettings), new PropertyMetadata(ExportSettingDefaultValue.PageNumberKind));

        public static string GetFormat(DependencyObject obj) => 
            (string) obj.GetValue(FormatProperty);

        public static PageNumberKind GetKind(DependencyObject obj) => 
            (PageNumberKind) obj.GetValue(KindProperty);

        public static void SetFormat(DependencyObject obj, string value)
        {
            obj.SetValue(FormatProperty, value);
        }

        public static void SetKind(DependencyObject obj, PageNumberKind value)
        {
            obj.SetValue(KindProperty, value);
        }
    }
}

