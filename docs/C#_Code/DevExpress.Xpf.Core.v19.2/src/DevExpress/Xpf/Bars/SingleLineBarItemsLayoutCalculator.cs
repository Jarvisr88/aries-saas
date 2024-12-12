namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SingleLineBarItemsLayoutCalculator : BarItemsLayoutCalculator
    {
        private bool isQuickCustomizationButtonVisible;
        private Size desiredSize;

        public SingleLineBarItemsLayoutCalculator(BarClientPanel panel, BarControl barControl);
        protected virtual bool AllowFillItem(BarItemLinkInfo elem);
        public override Size ArrangeBar(Size arrangeBounds);
        protected virtual double GetItemWidth(BarItemLinkInfo item, double maxWidthAddition);
        public override Size MeasureBar(Size constraint);
        protected virtual bool ShouldSkipItem(BarItemLinkInfo info);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SingleLineBarItemsLayoutCalculator.<>c <>9;
            public static Func<BarItemLinkInfo, bool> <>9__6_0;
            public static Func<BarItemLinkInfo, bool> <>9__6_2;
            public static Func<BarItemLinkInfo, bool> <>9__6_3;

            static <>c();
            internal bool <ArrangeBar>b__6_0(BarItemLinkInfo x);
            internal bool <ArrangeBar>b__6_2(BarItemLinkInfo x);
            internal bool <ArrangeBar>b__6_3(BarItemLinkInfo x);
        }
    }
}

