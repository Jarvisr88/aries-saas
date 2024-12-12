namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TabBorder : RotatableBorder
    {
        public static readonly DependencyProperty ViewInfoProperty;
        public static readonly DependencyProperty HoldBorderPaddingProperty;
        public static readonly DependencyProperty HoldBorderThicknessProperty;
        public static readonly DependencyProperty HoldBorderCornerRadiusProperty;
        public static readonly DependencyProperty HoldBorderMarginProperty;
        public static readonly DependencyProperty BorderModeProperty;
        public static readonly DependencyProperty BackgroundModeProperty;
        public static readonly DependencyProperty LeaveOriginBorderColorProperty;
        public static readonly DependencyProperty LeaveOriginBackgroundColorProperty;
        public static readonly DependencyProperty CustomBackgroundBrightnessProperty;
        public static readonly DependencyProperty CustomBorderBrightnessProperty;
        private Grid Layout;
        private TabBorder.InnerTabBorder DecorBorder;
        private TabBorder.InnerTabBorder ColorBorder;
        private TabBorder.InnerTabBorder ChildBorder;

        static TabBorder();
        private void ApplyBackground(Border border, Brush backgroundColor);
        private void ApplyBorderColor(Border border, Brush borderColor);
        protected override void InitLayout();
        private bool IsTransparent(Brush b);
        protected override void OnActualChildChanged(UIElement oldValue, UIElement newValue);
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        protected override void OnLocationChanged();
        private void OnViewInfoChanged();
        protected override void Update();

        public TabViewInfo ViewInfo { get; set; }

        public Thickness? HoldBorderPadding { get; set; }

        public Thickness? HoldBorderThickness { get; set; }

        public CornerRadius? HoldBorderCornerRadius { get; set; }

        public Thickness? HoldBorderMargin { get; set; }

        public TabBorderMode BorderMode { get; set; }

        public TabBackgroundMode BackgroundMode { get; set; }

        public bool LeaveOriginBorderColor { get; set; }

        public bool LeaveOriginBackgroundColor { get; set; }

        public double CustomBackgroundBrightness { get; set; }

        public double CustomBorderBrightness { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dock Location { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabBorder.<>c <>9;
            public static Func<TabViewInfo, bool> <>9__57_0;
            public static Func<bool> <>9__57_1;
            public static Func<SolidColorBrush> <>9__57_3;
            public static Func<SolidColorBrush> <>9__57_5;
            public static Func<SolidColorBrush, bool> <>9__60_0;
            public static Func<bool> <>9__60_1;

            static <>c();
            internal void <.cctor>b__62_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__62_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <IsTransparent>b__60_0(SolidColorBrush x);
            internal bool <IsTransparent>b__60_1();
            internal bool <Update>b__57_0(TabViewInfo x);
            internal bool <Update>b__57_1();
            internal SolidColorBrush <Update>b__57_3();
            internal SolidColorBrush <Update>b__57_5();
        }

        public class InnerTabBorder : DXBorder
        {
        }
    }
}

