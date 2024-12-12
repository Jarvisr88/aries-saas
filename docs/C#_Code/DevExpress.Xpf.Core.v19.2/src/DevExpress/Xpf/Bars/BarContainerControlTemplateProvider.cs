namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BarContainerControlTemplateProvider : DependencyObject
    {
        public static readonly DependencyProperty BorderTemplateProperty;
        public static readonly DependencyProperty EmptyBorderTemplateProperty;
        public static readonly DependencyProperty BackgroundTemplateProperty;
        public static readonly DependencyProperty HorizontalPaddingProperty;
        public static readonly DependencyProperty VerticalPaddingProperty;

        static BarContainerControlTemplateProvider();
        public static ControlTemplate GetBackgroundTemplate(DependencyObject target);
        public static ControlTemplate GetBorderTemplate(DependencyObject target);
        public static ControlTemplate GetEmptyBorderTemplate(DependencyObject target);
        public static Thickness GetHorizontalPadding(DependencyObject obj);
        public static Thickness GetVerticalPadding(DependencyObject obj);
        public static void SetBackgroundTemplate(DependencyObject target, ControlTemplate value);
        public static void SetBorderTemplate(DependencyObject target, ControlTemplate value);
        public static void SetEmptyBorderTemplate(DependencyObject target, ControlTemplate value);
        public static void SetHorizontalPadding(DependencyObject obj, Thickness value);
        public static void SetVerticalPadding(DependencyObject obj, Thickness value);
    }
}

