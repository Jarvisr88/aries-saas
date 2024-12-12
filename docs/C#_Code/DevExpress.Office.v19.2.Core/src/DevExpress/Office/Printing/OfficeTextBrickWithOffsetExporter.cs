namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;

    public class OfficeTextBrickWithOffsetExporter : TextBrickExporter
    {
        protected override unsafe void DrawBackground(IGraphics gr, RectangleF rect)
        {
            RectangleF* efPtr1 = &rect;
            efPtr1.Y += this.OfficeTextBrick.VerticalTextOffset;
            if (gr is PdfGraphics)
            {
                base.DrawBackground(gr, rect);
            }
            else
            {
                Rectangle rectangle = Rectangle.Round(this.OfficeTextBrick.unitConverter.DocumentsToLayoutUnits(rect));
                base.DrawBackground(gr, rectangle);
            }
        }

        protected override unsafe void DrawText(IGraphics gr, RectangleF clientRectangle, StringFormat sf, Brush brush)
        {
            RectangleF* efPtr1 = &clientRectangle;
            efPtr1.Y += this.OfficeTextBrick.VerticalTextOffset;
            base.DrawText(gr, clientRectangle, sf, brush);
        }

        private OfficeTextBrickWithOffset OfficeTextBrick =>
            (OfficeTextBrickWithOffset) base.Brick;
    }
}

