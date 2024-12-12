namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class ScrollBarAnnotationRowInfo : IComparable<ScrollBarAnnotationRowInfo>
    {
        public ScrollBarAnnotationRowInfo(int rowIndex, ScrollBarAnnotationInfo info) : this(rowIndex, rowIndex, info)
        {
        }

        internal ScrollBarAnnotationRowInfo(int start, int end, ScrollBarAnnotationInfo info)
        {
            this.RowIndex = Math.Min(start, end);
            this.EndRowIndex = Math.Max(start, end);
            this.ScrollAnnotationInfo = info;
        }

        internal void ChangeIndex(int newIndex)
        {
            this.RowIndex = this.EndRowIndex = newIndex;
        }

        public int CompareTo(ScrollBarAnnotationRowInfo other) => 
            this.RowIndex.CompareTo(other.RowIndex);

        public int RowIndex { get; private set; }

        internal int EndRowIndex { get; private set; }

        public ScrollBarAnnotationInfo ScrollAnnotationInfo { get; private set; }
    }
}

