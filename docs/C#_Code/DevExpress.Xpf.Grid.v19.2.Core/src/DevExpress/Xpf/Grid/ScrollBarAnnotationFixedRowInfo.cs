namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    internal class ScrollBarAnnotationFixedRowInfo : ScrollBarAnnotationRowInfo
    {
        public ScrollBarAnnotationFixedRowInfo(int fixedTopRowCount, int fixedBottomRowCount, int visibleRowCount) : base(0, null)
        {
            this.FixedTopRowCount = fixedTopRowCount;
            this.FixedBottomRowCount = fixedBottomRowCount;
            this.VisibleRowCount = visibleRowCount;
        }

        public int FixedTopRowCount { get; private set; }

        public int FixedBottomRowCount { get; private set; }

        public int VisibleRowCount { get; set; }
    }
}

