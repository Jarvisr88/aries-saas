namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BadgeBorder : Border
    {
        public static readonly DependencyProperty IgnoreCornerRadiusProperty;
        public static readonly DependencyProperty ShapeProperty;

        static BadgeBorder();
        private object CoerceCornerRadius(CornerRadius value);
        protected override Size MeasureOverride(Size constraint);
        protected internal virtual void OnIgnoreCornerRadiusChanged(bool oldValue, bool newValue);
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo);
        protected internal virtual void OnShapeChanged(BadgeShape oldValue, BadgeShape newValue);

        public bool IgnoreCornerRadius { get; set; }

        public BadgeShape Shape { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BadgeBorder.<>c <>9;

            static <>c();
            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs args);
            internal void <.cctor>b__0_1(DependencyObject d, DependencyPropertyChangedEventArgs args);
            internal object <.cctor>b__0_2(DependencyObject o, object value);
        }
    }
}

