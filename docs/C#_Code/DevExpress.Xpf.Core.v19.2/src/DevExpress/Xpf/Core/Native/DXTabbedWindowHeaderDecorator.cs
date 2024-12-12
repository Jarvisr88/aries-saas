namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXTabbedWindowHeaderDecorator : Decorator
    {
        public static readonly DependencyProperty TabbedWindowModeProperty;

        static DXTabbedWindowHeaderDecorator();
        public DXTabbedWindowHeaderDecorator();
        protected override Size ArrangeOverride(Size arrangeSize);
        private bool ChildDesiredSizeIsZero();
        protected override Size MeasureOverride(Size constraint);
        private void OnTabbedWindowModeChanged();
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);

        public DevExpress.Xpf.Core.TabbedWindowMode TabbedWindowMode { get; set; }

        public FrameworkElement Child { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabbedWindowHeaderDecorator.<>c <>9;

            static <>c();
            internal void <.cctor>b__13_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

