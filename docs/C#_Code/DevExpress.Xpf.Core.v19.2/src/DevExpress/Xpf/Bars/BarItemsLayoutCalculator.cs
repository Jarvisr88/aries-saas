namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BarItemsLayoutCalculator
    {
        public static readonly DependencyProperty RowIndexProperty;
        public static readonly DependencyProperty RowHeightProperty;

        static BarItemsLayoutCalculator();
        public BarItemsLayoutCalculator(BarClientPanel panel, DevExpress.Xpf.Bars.BarControl barControl);
        public abstract Size ArrangeBar(Size arrangeBounds);
        public static BarItemsLayoutCalculator CreatePanel(BarClientPanel panel, DevExpress.Xpf.Bars.BarControl barControl);
        protected BarItemAlignment GetAlignment(BarItemLinkInfo info);
        public static double GetRowHeight(DependencyObject obj);
        public static int GetRowIndex(DependencyObject obj);
        public abstract Size MeasureBar(Size constraint);
        protected void SetRowHeight(BarItemLinkInfo info, double rowHeight);
        public static void SetRowHeight(DependencyObject obj, double value);
        protected void SetRowIndex(BarItemLinkInfo info, int rowIndex);
        public static void SetRowIndex(DependencyObject obj, int value);

        public DevExpress.Xpf.Bars.BarControl BarControl { get; private set; }

        public DevExpress.Xpf.Bars.Bar Bar { get; }

        protected BarClientPanel Panel { get; private set; }

        protected IEnumerable<BarItemLinkInfo> Items { get; }

        protected IEnumerable<BarItemLinkInfo> LeftItems { get; }

        protected IEnumerable<BarItemLinkInfo> RightItems { get; }

        protected double HorizontalIndent { get; }

        protected BarQuickCustomizationButton QuickCustomizationButton { get; }

        protected bool? QuickCustomizationButtonShowMode { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemsLayoutCalculator.<>c <>9;
            public static Func<BarClientPanel, double> <>9__24_0;
            public static Func<double> <>9__24_1;
            public static Func<BarClientPanel, BarQuickCustomizationButton> <>9__26_0;
            public static Func<BarClientPanel, bool?> <>9__28_0;
            public static Func<bool?> <>9__28_1;

            static <>c();
            internal double <get_HorizontalIndent>b__24_0(BarClientPanel x);
            internal double <get_HorizontalIndent>b__24_1();
            internal BarQuickCustomizationButton <get_QuickCustomizationButton>b__26_0(BarClientPanel x);
            internal bool? <get_QuickCustomizationButtonShowMode>b__28_0(BarClientPanel x);
            internal bool? <get_QuickCustomizationButtonShowMode>b__28_1();
        }
    }
}

