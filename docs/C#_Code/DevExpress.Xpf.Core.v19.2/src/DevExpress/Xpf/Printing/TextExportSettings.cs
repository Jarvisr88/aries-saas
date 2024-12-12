namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class TextExportSettings
    {
        public static readonly DependencyProperty HorizontalAlignmentProperty = RegisterAttached("HorizontalAlignment", typeof(HorizontalAlignment), ExportSettingDefaultValue.HorizontalAlignment);
        public static readonly DependencyProperty VerticalAlignmentProperty = RegisterAttached("VerticalAlignment", typeof(VerticalAlignment), ExportSettingDefaultValue.VerticalAlignment);
        public static readonly DependencyProperty TextProperty = RegisterAttached("Text", typeof(string), ExportSettingDefaultValue.Text);
        public static readonly DependencyProperty TextValueProperty = RegisterAttached("TextValue", typeof(object), ExportSettingDefaultValue.TextValue);
        public static readonly DependencyProperty TextValueFormatStringProperty = RegisterAttached("TextValueFormatString", typeof(string), ExportSettingDefaultValue.TextValueFormatString);
        public static readonly DependencyProperty FontFamilyProperty = RegisterAttached("FontFamily", typeof(FontFamily), ExportSettingDefaultValue.FontFamily);
        public static readonly DependencyProperty FontStyleProperty = RegisterAttached("FontStyle", typeof(FontStyle), ExportSettingDefaultValue.FontStyle);
        public static readonly DependencyProperty FontWeightProperty = RegisterAttached("FontWeight", typeof(FontWeight), ExportSettingDefaultValue.FontWeight);
        public static readonly DependencyProperty FontSizeProperty = RegisterAttached("FontSize", typeof(double), ExportSettingDefaultValue.FontSize);
        public static readonly DependencyProperty PaddingProperty = RegisterAttached("Padding", typeof(Thickness), ExportSettingDefaultValue.Padding);
        public static readonly DependencyProperty TextWrappingProperty = RegisterAttached("TextWrapping", typeof(TextWrapping), ExportSettingDefaultValue.TextWrapping);
        public static readonly DependencyProperty NoTextExportProperty = RegisterAttached("NoTextExport", typeof(bool), ExportSettingDefaultValue.NoTextExport);
        public static readonly DependencyProperty XlsExportNativeFormatProperty = RegisterAttached("XlsExportNativeFormat", typeof(bool?), ExportSettingDefaultValue.XlsExportNativeFormat);
        public static readonly DependencyProperty XlsxFormatStringProperty = RegisterAttached("XlsxFormatString", typeof(string), ExportSettingDefaultValue.XlsxFormatString);
        public static readonly DependencyProperty TextDecorationsProperty = RegisterAttached("TextDecorations", typeof(TextDecorationCollection), ExportSettingDefaultValue.TextDecorations);
        public static readonly DependencyProperty TextTrimmingProperty = RegisterAttached("TextTrimming", typeof(TextTrimming), TextTrimming.None);

        public static FontFamily GetFontFamily(DependencyObject obj) => 
            (FontFamily) obj.GetValue(FontFamilyProperty);

        public static double GetFontSize(DependencyObject obj) => 
            (double) obj.GetValue(FontSizeProperty);

        public static FontStyle GetFontStyle(DependencyObject obj) => 
            (FontStyle) obj.GetValue(FontStyleProperty);

        public static FontWeight GetFontWeight(DependencyObject obj) => 
            (FontWeight) obj.GetValue(FontWeightProperty);

        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject obj) => 
            (HorizontalAlignment) obj.GetValue(HorizontalAlignmentProperty);

        public static bool GetNoTextExport(DependencyObject obj) => 
            (bool) obj.GetValue(NoTextExportProperty);

        public static Thickness GetPadding(DependencyObject obj) => 
            (Thickness) obj.GetValue(PaddingProperty);

        public static string GetText(DependencyObject obj) => 
            (string) obj.GetValue(TextProperty);

        public static TextDecorationCollection GetTextDecorations(DependencyObject obj) => 
            (TextDecorationCollection) obj.GetValue(TextDecorationsProperty);

        public static TextTrimming GetTextTrimming(DependencyObject obj) => 
            (TextTrimming) obj.GetValue(TextTrimmingProperty);

        public static object GetTextValue(DependencyObject obj) => 
            obj.GetValue(TextValueProperty);

        public static string GetTextValueFormatString(DependencyObject obj) => 
            (string) obj.GetValue(TextValueFormatStringProperty);

        public static TextWrapping GetTextWrapping(DependencyObject obj) => 
            (TextWrapping) obj.GetValue(TextWrappingProperty);

        public static VerticalAlignment GetVerticalAlignment(DependencyObject obj) => 
            (VerticalAlignment) obj.GetValue(VerticalAlignmentProperty);

        public static bool? GetXlsExportNativeFormat(DependencyObject obj) => 
            (bool?) obj.GetValue(XlsExportNativeFormatProperty);

        public static string GetXlsxFormatString(DependencyObject obj) => 
            (string) obj.GetValue(XlsxFormatStringProperty);

        private static DependencyProperty RegisterAttached(string name, Type propertyType, object defaultValue) => 
            DependencyPropertyManager.RegisterAttached(name, propertyType, typeof(TextExportSettings), new PropertyMetadata(defaultValue));

        public static void SetFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(FontFamilyProperty, value);
        }

        public static void SetFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(FontSizeProperty, value);
        }

        public static void SetFontStyle(DependencyObject obj, FontStyle value)
        {
            obj.SetValue(FontStyleProperty, value);
        }

        public static void SetFontWeight(DependencyObject obj, FontWeight value)
        {
            obj.SetValue(FontWeightProperty, value);
        }

        public static void SetHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(HorizontalAlignmentProperty, value);
        }

        public static void SetNoTextExport(DependencyObject obj, bool value)
        {
            obj.SetValue(NoTextExportProperty, value);
        }

        public static void SetPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PaddingProperty, value);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static void SetTextDecorations(DependencyObject obj, TextDecorationCollection value)
        {
            obj.SetValue(TextDecorationsProperty, value);
        }

        public static void SetTextTrimming(DependencyObject obj, TextTrimming value)
        {
            obj.SetValue(TextTrimmingProperty, value);
        }

        public static void SetTextValue(DependencyObject obj, object value)
        {
            obj.SetValue(TextValueProperty, value);
        }

        public static void SetTextValueFormatString(DependencyObject obj, string value)
        {
            obj.SetValue(TextValueFormatStringProperty, value);
        }

        public static void SetTextWrapping(DependencyObject obj, TextWrapping value)
        {
            obj.SetValue(TextWrappingProperty, value);
        }

        public static void SetVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(VerticalAlignmentProperty, value);
        }

        public static void SetXlsExportNativeFormat(DependencyObject obj, bool? value)
        {
            obj.SetValue(XlsExportNativeFormatProperty, value);
        }

        public static void SetXlsxFormatString(DependencyObject obj, string value)
        {
            obj.SetValue(XlsxFormatStringProperty, value);
        }
    }
}

