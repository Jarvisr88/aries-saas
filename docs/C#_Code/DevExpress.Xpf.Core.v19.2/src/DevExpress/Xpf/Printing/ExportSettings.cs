namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class ExportSettings
    {
        public static readonly DependencyProperty TargetTypeProperty;
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty BorderColorProperty;
        public static readonly DependencyProperty BorderThicknessProperty;
        public static readonly DependencyProperty UrlProperty;
        public static readonly DependencyProperty OnPageUpdaterProperty;
        public static readonly DependencyProperty PropertiesHintMaskProperty;
        public static readonly DependencyProperty ElementTagProperty;
        public static readonly DependencyProperty BookmarkProperty;
        public static readonly DependencyProperty BookmarkParentNameProperty;
        public static readonly DependencyProperty BorderDashStyleProperty;
        public static readonly DependencyProperty MergeValueProperty;
        public static readonly DependencyProperty FlowDirectionProperty;

        static ExportSettings()
        {
            Type ownerType = typeof(ExportSettings);
            TargetTypeProperty = DependencyPropertyManager.RegisterAttached("TargetType", typeof(TargetType), ownerType, new PropertyMetadata(ExportSettingDefaultValue.TargetType));
            BackgroundProperty = DependencyPropertyManager.RegisterAttached("Background", typeof(Color), ownerType, new FrameworkPropertyMetadata(ExportSettingDefaultValue.Background, FrameworkPropertyMetadataOptions.Inherits));
            ForegroundProperty = DependencyPropertyManager.RegisterAttached("Foreground", typeof(Color), ownerType, new PropertyMetadata(ExportSettingDefaultValue.Foreground));
            BorderColorProperty = DependencyPropertyManager.RegisterAttached("BorderColor", typeof(Color), ownerType, new PropertyMetadata(ExportSettingDefaultValue.BorderColor));
            BorderThicknessProperty = DependencyPropertyManager.RegisterAttached("BorderThickness", typeof(Thickness), ownerType, new PropertyMetadata(ExportSettingDefaultValue.BorderThickness));
            UrlProperty = DependencyPropertyManager.RegisterAttached("Url", typeof(string), ownerType, new PropertyMetadata(ExportSettingDefaultValue.Url));
            OnPageUpdaterProperty = DependencyPropertyManager.RegisterAttached("OnPageUpdater", typeof(IOnPageUpdater), ownerType, new PropertyMetadata(ExportSettingDefaultValue.OnPageUpdater));
            PropertiesHintMaskProperty = DependencyPropertyManager.RegisterAttached("PropertiesHintMask", typeof(ExportSettingsProperties), ownerType, new PropertyMetadata(ExportSettingsProperties.None));
            ElementTagProperty = DependencyPropertyManager.RegisterAttached("ElementTag", typeof(string), typeof(ExportSettings), new PropertyMetadata(null));
            BookmarkProperty = DependencyPropertyManager.RegisterAttached("Bookmark", typeof(string), typeof(ExportSettings), new PropertyMetadata(null));
            BookmarkParentNameProperty = DependencyPropertyManager.RegisterAttached("BookmarkParentName", typeof(string), typeof(ExportSettings), new PropertyMetadata(null));
            BorderDashStyleProperty = DependencyPropertyManager.RegisterAttached("BorderDashStyle", typeof(BorderDashStyle), typeof(ExportSettings), new PropertyMetadata(ExportSettingDefaultValue.BorderDashStyle));
            MergeValueProperty = DependencyPropertyManager.RegisterAttached("MergeValue", typeof(object), typeof(ExportSettings), new PropertyMetadata(ExportSettingDefaultValue.MergeValue));
            FlowDirectionProperty = DependencyPropertyManager.RegisterAttached("FlowDirection", typeof(FlowDirection), ownerType, new PropertyMetadata(ExportSettingDefaultValue.FlowDirection));
        }

        public static Color GetBackground(DependencyObject obj) => 
            (Color) obj.GetValue(BackgroundProperty);

        public static string GetBookmark(DependencyObject obj) => 
            (string) obj.GetValue(BookmarkProperty);

        public static string GetBookmarkParentName(DependencyObject obj) => 
            (string) obj.GetValue(BookmarkParentNameProperty);

        public static Color GetBorderColor(DependencyObject obj) => 
            (Color) obj.GetValue(BorderColorProperty);

        public static BorderDashStyle GetBorderDashStyle(DependencyObject obj) => 
            (BorderDashStyle) obj.GetValue(BorderDashStyleProperty);

        public static Thickness GetBorderThickness(DependencyObject obj) => 
            (Thickness) obj.GetValue(BorderThicknessProperty);

        public static string GetElementTag(DependencyObject obj) => 
            (string) obj.GetValue(ElementTagProperty);

        public static FlowDirection GetFlowDirection(DependencyObject obj) => 
            (FlowDirection) obj.GetValue(FlowDirectionProperty);

        public static Color GetForeground(DependencyObject obj) => 
            (Color) obj.GetValue(ForegroundProperty);

        public static object GetMergeValue(DependencyObject obj) => 
            obj.GetValue(MergeValueProperty);

        public static IOnPageUpdater GetOnPageUpdater(DependencyObject obj) => 
            (IOnPageUpdater) obj.GetValue(OnPageUpdaterProperty);

        public static ExportSettingsProperties GetPropertiesHintMask(DependencyObject obj) => 
            (ExportSettingsProperties) obj.GetValue(PropertiesHintMaskProperty);

        public static TargetType GetTargetType(DependencyObject obj) => 
            (TargetType) obj.GetValue(TargetTypeProperty);

        public static string GetUrl(DependencyObject obj) => 
            (string) obj.GetValue(UrlProperty);

        public static void SetBackground(DependencyObject obj, Color value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        public static void SetBookmark(DependencyObject obj, string value)
        {
            obj.SetValue(BookmarkProperty, value);
        }

        public static void SetBookmarkParentName(DependencyObject obj, string value)
        {
            obj.SetValue(BookmarkParentNameProperty, value);
        }

        public static void SetBorderColor(DependencyObject obj, Color value)
        {
            obj.SetValue(BorderColorProperty, value);
        }

        public static void SetBorderDashStyle(DependencyObject obj, BorderDashStyle value)
        {
            obj.SetValue(BorderDashStyleProperty, value);
        }

        public static void SetBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(BorderThicknessProperty, value);
        }

        public static void SetElementTag(DependencyObject obj, string value)
        {
            obj.SetValue(ElementTagProperty, value);
        }

        public static void SetFlowDirection(DependencyObject obj, FlowDirection value)
        {
            obj.SetValue(FlowDirectionProperty, value);
        }

        public static void SetForeground(DependencyObject obj, Color value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        public static void SetMergeValue(DependencyObject obj, object value)
        {
            obj.SetValue(MergeValueProperty, value);
        }

        public static void SetOnPageUpdater(DependencyObject obj, IOnPageUpdater value)
        {
            obj.SetValue(OnPageUpdaterProperty, value);
        }

        public static void SetPropertiesHintMask(DependencyObject obj, ExportSettingsProperties value)
        {
            obj.SetValue(PropertiesHintMaskProperty, value);
        }

        public static void SetTargetType(DependencyObject obj, TargetType value)
        {
            obj.SetValue(TargetTypeProperty, value);
        }

        public static void SetUrl(DependencyObject obj, string value)
        {
            obj.SetValue(UrlProperty, value);
        }
    }
}

