namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class MultilineBarItemsLayoutCalculator : BarItemsLayoutCalculator
    {
        public MultilineBarItemsLayoutCalculator(BarClientPanel panel, BarControl barControl);
        public override Size ArrangeBar(Size arrangeBounds);
        private Size ArrangeBarItemLine(IEnumerable<BarItemLinkInfo> line, double vOffset, Size arrangeBounds, double zero = 0.0);
        private void ArrangeItemsCore(Size arrangeBounds, IEnumerable<IEnumerable<BarItemLinkInfo>> lines, Action<IEnumerable<BarItemLinkInfo>, double, Size> arrangeAction);
        protected virtual void ArrangeLeftBarItemLine(IEnumerable<BarItemLinkInfo> line, double vOffset, Size arrangeBounds);
        protected virtual void ArrangeRightBarItemLine(IEnumerable<BarItemLinkInfo> line, double vOffset, Size arrangeBounds);
        protected virtual Size CalcLinesSize(IEnumerable<IEnumerable<BarItemLinkInfo>> lines);
        protected bool CheckGetLineHeight(IEnumerable<BarItemLinkInfo> line, BarItemLinkInfo linkInfo, ref double resultValue);
        protected virtual void CheckSingleLineSeparatorOrientation(IEnumerable<BarItemLinkInfo> list);
        protected Size GetDesiredSize(Size left, Size right);
        protected virtual double GetLeftMinWidth();
        protected virtual double GetLineHeight(IEnumerable<BarItemLinkInfo> line);
        protected virtual IEnumerable<IEnumerable<BarItemLinkInfo>> GetLinesForLeftItems(double availWidth);
        protected virtual IEnumerable<IEnumerable<BarItemLinkInfo>> GetLinesForRightItems(double availWidth);
        protected virtual double GetLineWidth(IEnumerable<BarItemLinkInfo> line);
        protected virtual double GetOccupiedMinArea(IEnumerable<BarItemLinkInfo> barItems);
        protected virtual double GetRightMinWidth();
        protected double GetRowHeight(BarItemLinkInfo info);
        protected void HideBarEditItemsInVerticalOrientation();
        protected virtual Size MeassureLeftBarItems(Size constraint);
        protected virtual Size MeassureRightBarItems(Size constraint);
        public override Size MeasureBar(Size constraint);
        private void SetLineRowHeight(IEnumerable<BarItemLinkInfo> line, double rowHeight);
        private IEnumerable<IEnumerable<BarItemLinkInfo>> SplitToLeftLines(double availWidth, IEnumerable<BarItemLinkInfo> line);
        private IEnumerable<IEnumerable<BarItemLinkInfo>> SplitToRightLines(double availWidth, IEnumerable<BarItemLinkInfo> line);
        protected virtual void UpdateItemsRowHeight();

        protected IEnumerable<IEnumerable<BarItemLinkInfo>> RightLines { get; set; }

        protected IEnumerable<IEnumerable<BarItemLinkInfo>> LeftLines { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MultilineBarItemsLayoutCalculator.<>c <>9;
            public static Action<BarItemLinkInfo> <>9__9_0;
            public static Action<BarQuickCustomizationButton> <>9__9_1;
            public static Action<BarItemLinkInfo> <>9__9_2;
            public static Func<BarItemLinkInfo, double> <>9__18_1;
            public static Func<BarItemLinkInfo, double> <>9__18_3;
            public static Func<BarItemLinkInfo, bool> <>9__25_0;
            public static Func<BarItemLinkInfo, bool> <>9__33_0;
            public static Func<BarItemLinkInfo, double> <>9__33_1;

            static <>c();
            internal bool <GetLineWidth>b__25_0(BarItemLinkInfo elem);
            internal bool <GetOccupiedMinArea>b__33_0(BarItemLinkInfo item);
            internal double <GetOccupiedMinArea>b__33_1(BarItemLinkInfo item);
            internal void <MeasureBar>b__9_0(BarItemLinkInfo x);
            internal void <MeasureBar>b__9_1(BarQuickCustomizationButton x);
            internal void <MeasureBar>b__9_2(BarItemLinkInfo x);
            internal double <UpdateItemsRowHeight>b__18_1(BarItemLinkInfo info);
            internal double <UpdateItemsRowHeight>b__18_3(BarItemLinkInfo info);
        }
    }
}

