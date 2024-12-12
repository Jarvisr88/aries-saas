namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;

    public class OfficeImageBrickExporter : ImageBrickExporter
    {
        protected override void DrawObject(IGraphics gr, RectangleF clientRect)
        {
            if (gr is PdfGraphics)
            {
                base.DrawObject(gr, clientRect);
            }
            else
            {
                Rectangle rect = Rectangle.Round((base.Brick as OfficeImageBrick).unitConverter.DocumentsToLayoutUnits(clientRect));
                base.DrawObject(gr, rect);
            }
        }
    }
}

