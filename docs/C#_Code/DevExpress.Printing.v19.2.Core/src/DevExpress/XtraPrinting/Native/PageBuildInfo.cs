namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class PageBuildInfo
    {
        public static PageBuildInfo Empty;
        private int index;
        private RectangleF bounds;
        private PointF offset;

        static PageBuildInfo();
        public PageBuildInfo(int index, RectangleF bounds, PointF offset);

        public int Index { get; }

        public RectangleF Bounds { get; }

        public PointF Offset { get; }
    }
}

