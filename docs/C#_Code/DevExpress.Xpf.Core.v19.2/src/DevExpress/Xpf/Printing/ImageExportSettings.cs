namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public static class ImageExportSettings
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyPropertyManager.RegisterAttached("ImageSource", typeof(FrameworkElement), typeof(ImageExportSettings), new PropertyMetadata(ExportSettingDefaultValue.ImageSource));
        public static readonly DependencyProperty ImageRenderModeProperty = DependencyPropertyManager.RegisterAttached("ImageRenderMode", typeof(ImageRenderMode), typeof(ImageExportSettings), new PropertyMetadata(ExportSettingDefaultValue.ImageRenderMode));
        public static readonly DependencyProperty ImageKeyProperty = DependencyPropertyManager.RegisterAttached("ImageKey", typeof(object), typeof(ImageExportSettings), new PropertyMetadata(ExportSettingDefaultValue.ImageKey));
        public static readonly DependencyProperty ForceCenterImageModeProperty = DependencyPropertyManager.RegisterAttached("ForceCenterImageMode", typeof(bool), typeof(ImageExportSettings), new PropertyMetadata(false));

        public static bool GetForceCenterImageMode(DependencyObject obj) => 
            (bool) obj.GetValue(ForceCenterImageModeProperty);

        public static object GetImageKey(DependencyObject obj) => 
            obj.GetValue(ImageKeyProperty);

        public static ImageRenderMode GetImageRenderMode(DependencyObject obj) => 
            (ImageRenderMode) obj.GetValue(ImageRenderModeProperty);

        public static FrameworkElement GetImageSource(DependencyObject obj) => 
            (FrameworkElement) obj.GetValue(ImageSourceProperty);

        public static void SetForceCenterImageMode(DependencyObject obj, bool value)
        {
            obj.SetValue(ForceCenterImageModeProperty, value);
        }

        public static void SetImageKey(DependencyObject obj, object value)
        {
            obj.SetValue(ImageKeyProperty, value);
        }

        public static void SetImageRenderMode(DependencyObject obj, ImageRenderMode value)
        {
            obj.SetValue(ImageRenderModeProperty, value);
        }

        public static void SetImageSource(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }
    }
}

