namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class TabPanelScrollViewBase : TabPanelBase
    {
        public static readonly DependencyProperty IsStretchedHorizontallyProperty;

        static TabPanelScrollViewBase();
        protected TabPanelScrollViewBase();
        protected override Size AfterArrangeOverride(Size avSize);
        protected override Size ArrangeOverrideCore(Rect rect);
        protected override Size MeasureOverrideCore(Size avSize);
        protected override void UpdateControlBoxPosition(Size actualSize);

        public bool IsStretchedHorizontally { get; set; }

        protected double FullWidth { get; private set; }

        protected Size VisibleSize { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelScrollViewBase.<>c <>9;
            public static Action<FrameworkElement> <>9__13_0;

            static <>c();
            internal void <ArrangeOverrideCore>b__13_0(FrameworkElement x);
        }
    }
}

