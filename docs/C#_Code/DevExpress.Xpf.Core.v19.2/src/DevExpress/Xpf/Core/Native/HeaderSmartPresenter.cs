namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class HeaderSmartPresenter : Panel
    {
        public static double CollapseCoef;
        public static readonly DependencyProperty HideControlProperty;
        private Border icon;
        private Border header;
        private Border control;

        static HeaderSmartPresenter();
        public HeaderSmartPresenter();
        private void ArrangeChild(FrameworkElement child, double x, double width, double height);
        protected override Size ArrangeOverride(Size finalSize);
        protected virtual double GetWidthForHideControl(double fullWidth);
        private void MeasuerChild(FrameworkElement child, double width, double height, bool hideChild = false);
        protected override Size MeasureOverride(Size availableSize);
        protected virtual void OnHideControlChanged(bool? oldValue, bool? newValue);

        public bool? HideControl { get; set; }

        public FrameworkElement IconBox { get; set; }

        public FrameworkElement ContentBox { get; set; }

        public FrameworkElement ControlBox { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HeaderSmartPresenter.<>c <>9;
            public static Func<Border, FrameworkElement> <>9__6_0;
            public static Func<Border, FrameworkElement> <>9__9_0;
            public static Func<Border, FrameworkElement> <>9__12_0;

            static <>c();
            internal void <.cctor>b__24_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal FrameworkElement <get_ContentBox>b__9_0(Border x);
            internal FrameworkElement <get_ControlBox>b__12_0(Border x);
            internal FrameworkElement <get_IconBox>b__6_0(Border x);
        }
    }
}

