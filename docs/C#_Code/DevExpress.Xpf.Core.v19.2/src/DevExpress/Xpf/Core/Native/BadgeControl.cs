namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class BadgeControl : ContentControl
    {
        public static readonly DependencyProperty ShapeProperty;
        public static readonly DependencyProperty KindProperty;
        public static readonly DependencyProperty CornerRadiusProperty;
        protected static readonly DependencyPropertyKey BadgePropertyKey;
        public static readonly DependencyProperty BadgeProperty;
        public static readonly DependencyProperty ContentFormatProviderProperty;
        private static double[][] pps;
        private static readonly double[,] alignmentToAnchor;
        private int animationDuration;
        private bool showPending;
        private bool hidePending;

        static BadgeControl();
        public BadgeControl();
        protected virtual Size ArrangeChild(Size arrangeBounds);
        protected override Size ArrangeOverride(Size arrangeBounds);
        private void BeginAnimation(int duration, double to);
        public void BeginHideAnimation(int duration);
        public void BeginShowAnimation(int duration);
        public Rect CalcArrangeRect(Size arrangeBounds);
        public static void CalculateAlignment(BadgeControl badgeControl, out HorizontalAlignment horizontalAlignment, out VerticalAlignment verticalAlignment, out HorizontalAlignment horizontalAnchor, out VerticalAlignment verticalAnchor);
        public static Rect CalculateArrangeBounds(BadgeControl badgeControl, Rect targetBounds);
        public static Rect CalculateArrangeBounds(Size badgeSize, Rect targetBounds, HorizontalAlignment horizontalAlignment, HorizontalAlignment horizontalAnchor, VerticalAlignment verticalAlignment, VerticalAlignment verticalAnchor);
        protected internal virtual Brush CoerceBackground(Brush baseValue);
        protected internal virtual Brush CoerceBorderBrush(Brush baseValue);
        protected internal virtual Thickness CoerceBorderThickness(Thickness baseValue);
        private static object CoerceProperty(DependencyObject dobj, object baseValue, DependencyProperty badgeProperty);
        public override void OnApplyTemplate();
        protected internal virtual void OnBadgeChanged(DevExpress.Xpf.Core.Badge oldValue, DevExpress.Xpf.Core.Badge newValue);
        public void PrepareForAnimation(bool show, int duration);
        protected internal virtual void UpdatePlacement();
        protected internal virtual void UpdateTransform();

        public BadgeShape Shape { get; set; }

        public BadgeKind Kind { get; set; }

        public System.Windows.CornerRadius? CornerRadius { get; set; }

        public DevExpress.Xpf.Core.Badge Badge { get; protected internal set; }

        public IFormatProvider ContentFormatProvider { get; set; }

        public bool ArrangeAtLeftTop { get; set; }

        public BadgeWorkerBase Worker { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BadgeControl.<>c <>9;

            static <>c();
            internal void <.cctor>b__6_0(DependencyObject x, DependencyPropertyChangedEventArgs args);
            internal object <.cctor>b__6_1(DependencyObject d, object value);
            internal object <.cctor>b__6_10(DependencyObject o, object value);
            internal object <.cctor>b__6_11(DependencyObject o, object value);
            internal object <.cctor>b__6_12(DependencyObject o, object value);
            internal object <.cctor>b__6_13(DependencyObject o, object value);
            internal object <.cctor>b__6_14(DependencyObject o, object value);
            internal object <.cctor>b__6_15(DependencyObject o, object value);
            internal object <.cctor>b__6_16(DependencyObject o, object value);
            internal object <.cctor>b__6_2(DependencyObject d, object value);
            internal object <.cctor>b__6_3(DependencyObject d, object value);
            internal object <.cctor>b__6_4(DependencyObject d, object value);
            internal object <.cctor>b__6_5(DependencyObject o, object value);
            internal object <.cctor>b__6_6(DependencyObject o, object value);
            internal object <.cctor>b__6_7(DependencyObject o, object value);
            internal object <.cctor>b__6_8(DependencyObject o, object value);
            internal object <.cctor>b__6_9(DependencyObject o, object value);
        }
    }
}

