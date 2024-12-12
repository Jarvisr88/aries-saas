namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("ActualChild")]
    public class RotatableBorder : Decorator
    {
        public static readonly DependencyProperty LocationProperty;
        public static readonly DependencyProperty BorderThicknessProperty;
        public static readonly DependencyProperty BorderCornerRadiusProperty;
        public static readonly DependencyProperty BorderMarginProperty;
        public static readonly DependencyProperty BorderPaddingProperty;
        public static readonly DependencyProperty BorderBrushProperty;
        public static readonly DependencyProperty BorderBackgroundProperty;
        public static readonly DependencyProperty BorderOpacityMaskProperty;
        private UIElement actualChild;
        private DXBorder Border;

        static RotatableBorder();
        public RotatableBorder();
        protected virtual void InitLayout();
        protected virtual void OnActualChildChanged(UIElement oldValue, UIElement newValue);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnLocationChanged();
        protected virtual void Update();

        public Dock Location { get; set; }

        public CornerRadius BorderCornerRadius { get; set; }

        public Thickness BorderThickness { get; set; }

        public Thickness BorderMargin { get; set; }

        public Thickness BorderPadding { get; set; }

        public Brush BorderBrush { get; set; }

        public Brush BorderBackground { get; set; }

        public Brush BorderOpacityMask { get; set; }

        public UIElement ActualChild { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RotatableBorder.<>c <>9;

            static <>c();
            internal void <.cctor>b__43_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__43_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

