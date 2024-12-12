namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public class PagePaintEventArgs : EventArgs
    {
        private DevExpress.XtraPrinting.Page page;
        private IGraphics graphics;
        private RectangleF pageRectangle;

        internal PagePaintEventArgs(DevExpress.XtraPrinting.Page page, IGraphics graphics, RectangleF pageRectangle)
        {
            this.page = page;
            this.graphics = graphics;
            this.pageRectangle = pageRectangle;
        }

        public DevExpress.XtraPrinting.Page Page =>
            this.page;

        public RectangleF PageRectangle =>
            this.pageRectangle;

        public IGraphics Graphics =>
            this.graphics;
    }
}

