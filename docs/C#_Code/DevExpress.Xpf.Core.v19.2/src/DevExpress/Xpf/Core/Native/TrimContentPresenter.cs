namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TrimContentPresenter : ContentPresenter
    {
        public static readonly DependencyProperty TrimmingModeProperty;
        public static readonly DependencyProperty TrimmingSizeProperty;
        private static readonly DependencyPropertyKey IsTrimmedPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsTrimmedProperty;
        public static readonly DependencyProperty DisableToolTipWhenFullyVisibleProperty;
        public static readonly DependencyProperty ToolTipOwnerProperty;

        static TrimContentPresenter();
        protected override Size ArrangeOverride(Size avSize);
        protected override Size MeasureOverride(Size avSize);
        private void OnDisableToolTipWhenFullyVisibleChanged(bool oldValue, bool newValue);
        private void OnIsTrimmedChanged(bool oldValue, bool newValue);
        private void OnToolTipOwnerChanged(FrameworkElement oldValue, FrameworkElement newValue);
        protected virtual void UpdateOpacityMask(FrameworkElement child, Size actualSize);

        public DevExpress.Xpf.Core.Native.TrimmingMode TrimmingMode { get; set; }

        public double TrimmingSize { get; set; }

        public bool IsTrimmed { get; private set; }

        public bool DisableToolTipWhenFullyVisible { get; set; }

        public FrameworkElement ToolTipOwner { get; set; }

        protected SizeHelperBase OrientationHelper { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TrimContentPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__30_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__30_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__30_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

