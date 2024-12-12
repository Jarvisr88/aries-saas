namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ImageColorizer
    {
        public static readonly DependencyProperty IsEnabledProperty;
        public static readonly DependencyProperty ColorProperty;

        static ImageColorizer();
        public static Color GetColor(DependencyObject obj);
        public static bool GetIsEnabled(DependencyObject obj);
        protected static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetColor(DependencyObject obj, Color value);
        public static void SetIsEnabled(DependencyObject obj, bool value);
        protected internal static void UpdateImageColor(Image image, Color color, bool enabled);
    }
}

