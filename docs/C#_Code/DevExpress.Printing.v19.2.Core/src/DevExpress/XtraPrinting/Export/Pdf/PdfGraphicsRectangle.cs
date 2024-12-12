namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public class PdfGraphicsRectangle
    {
        private RectangleF rect;

        public PdfGraphicsRectangle(RectangleF rect, SizeF pageSize)
        {
            this.rect = rect;
            this.rect.Y = pageSize.Height - rect.Bottom;
        }

        public float Width =>
            this.rect.Width;

        public float Height =>
            this.rect.Height;

        public float Top =>
            this.rect.Bottom;

        public float Bottom =>
            this.rect.Top;

        public float Left =>
            this.rect.Left;

        public float Right =>
            this.rect.Right;
    }
}

