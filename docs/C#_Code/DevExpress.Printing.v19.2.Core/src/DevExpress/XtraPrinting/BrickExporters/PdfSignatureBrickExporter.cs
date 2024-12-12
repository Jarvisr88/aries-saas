namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class PdfSignatureBrickExporter : BrickExporter
    {
        protected internal override void DrawPdf(IPdfGraphics gr, RectangleF rect);
    }
}

