namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class ValueSetter : DependencyObject
    {
        public static readonly DependencyProperty ThicknessProperty;
        public static readonly DependencyProperty VisibilityProperty;
        public static readonly DependencyProperty CornerRadiusProperty;
        public static readonly DependencyProperty HideBorderSideProperty;
        public static readonly DependencyProperty VerticalAlignmentProperty;
        public static readonly DependencyProperty HorizontalAlignmentProperty;
        public static readonly DependencyProperty FontWeightProperty;
        private static KeyTime ZeroTime;

        static ValueSetter();
        public static CornerRadius GetCornerRadius(DependencyObject d);
        public static FontWeight GetFontWeight(DependencyObject d);
        public static HideBorderSide? GetHideBorderSide(DependencyObject d);
        public static HorizontalAlignment? GetHorizontalAlignment(DependencyObject d);
        public static Thickness GetThickness(DependencyObject d);
        public static VerticalAlignment? GetVerticalAlignment(DependencyObject d);
        public static Visibility? GetVisibility(DependencyObject d);
        private static void OnCornerRadiusChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnFontWeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnHideBorderSideChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnHorizontalAlignmentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnThicknessChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnVerticalAlignmentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        private static void OnVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e);
        public static void SetCornerRadius(DependencyObject d, CornerRadius value);
        public static void SetFontWeight(DependencyObject d, FontWeight value);
        public static void SetHideBorderSide(DependencyObject d, HideBorderSide value);
        public static void SetHorizontalAlignment(DependencyObject d, HorizontalAlignment value);
        public static void SetThickness(DependencyObject d, Thickness value);
        public static void SetVerticalAlignment(DependencyObject d, VerticalAlignment value);
        public static void SetVisibility(DependencyObject d, Visibility value);
    }
}

