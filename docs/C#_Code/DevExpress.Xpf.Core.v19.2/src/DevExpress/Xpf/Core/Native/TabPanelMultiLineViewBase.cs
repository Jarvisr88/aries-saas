namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class TabPanelMultiLineViewBase : TabPanelBase
    {
        public static readonly DependencyProperty AutoMoveActiveRowProperty;
        public static readonly DependencyProperty AutoReverseItemsProperty;
        public static readonly DependencyProperty IsStretchedHorizontallyProperty;
        public static readonly DependencyProperty IsStretchedVerticallyProperty;
        private TabPanelMultiLineViewBase.RowManager rows;

        static TabPanelMultiLineViewBase();
        public TabPanelMultiLineViewBase();
        protected override Size ArrangeOverrideCore(Rect avRect);
        protected override Size BeforeMeasureOverride(Size avSize);
        private TabPanelMultiLineViewBase.Row GetActiveRow();
        private int GetRowCount(double avSize);
        protected override Size MeasureOverrideCore(Size avSize);
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e);
        private TabPanelMultiLineViewBase.RowManager Split(double avSize, int rowCount);
        protected override void UpdateControlBoxPosition(Size actualSize);

        public bool AutoMoveActiveRow { get; set; }

        public bool AutoReverseItems { get; set; }

        public bool IsStretchedHorizontally { get; set; }

        public bool IsStretchedVertically { get; set; }

        protected int RowCount { get; private set; }

        private class Row
        {
            private readonly TabPanelMultiLineViewBase.RowManager Owner;
            public readonly IEnumerable<FrameworkElement> Children;

            public Row(TabPanelMultiLineViewBase.RowManager owner);
            public void Add(FrameworkElement child, int? position = new int?());
            public FrameworkElement Remove();

            public FrameworkElement this[int index] { get; }

            public int Count { get; }

            public double Width { get; protected set; }

            public double Height { get; }

            public double Coef { get; }
        }

        private class RowManager
        {
            public readonly TabPanelMultiLineViewBase Panel;
            public readonly double AvSize;

            public RowManager(TabPanelMultiLineViewBase panel, double avSize);
            public void AddNewRow();
            public bool CanMoveChildToRow(int row);
            public TabPanelMultiLineViewBase.RowManager Clone();
            public void MoveChildToRow(int row);
            public void MoveToFirstPosition(TabPanelMultiLineViewBase.Row row);
            public void MoveToLastPosition(TabPanelMultiLineViewBase.Row row);
            public void ReverseRows();

            public IEnumerable<TabPanelMultiLineViewBase.Row> Rows { get; private set; }

            public TabPanelMultiLineViewBase.Row this[int index] { get; }

            public int Count { get; }

            public TabPanelMultiLineViewBase.Row Last { get; }

            public int RowWithMaxCoef { get; }

            public double MaxCoef { get; }

            public double MaxWidth { get; }

            public double Height { get; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly TabPanelMultiLineViewBase.RowManager.<>c <>9;
                public static Func<TabPanelMultiLineViewBase.Row, double> <>9__13_0;
                public static Func<TabPanelMultiLineViewBase.Row, double> <>9__15_0;
                public static Func<TabPanelMultiLineViewBase.Row, double> <>9__17_0;
                public static Func<TabPanelMultiLineViewBase.Row, double> <>9__19_0;

                static <>c();
                internal double <get_Height>b__19_0(TabPanelMultiLineViewBase.Row x);
                internal double <get_MaxCoef>b__15_0(TabPanelMultiLineViewBase.Row x);
                internal double <get_MaxWidth>b__17_0(TabPanelMultiLineViewBase.Row x);
                internal double <get_RowWithMaxCoef>b__13_0(TabPanelMultiLineViewBase.Row x);
            }
        }
    }
}

