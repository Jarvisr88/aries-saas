namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class PageUpdateData
    {
        public bool IsUpdated;
        public RectangleF Bounds;

        public PageUpdateData();
        public PageUpdateData(RectangleF bounds);
        public PageUpdateData(RectangleF bounds, bool isUpdated);
    }
}

