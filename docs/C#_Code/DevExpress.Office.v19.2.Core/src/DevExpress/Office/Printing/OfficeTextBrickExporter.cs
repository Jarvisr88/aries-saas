namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;

    public class OfficeTextBrickExporter : TextBrickExporter
    {
        protected override void DrawBackground(IGraphics gr, RectangleF rect)
        {
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

        private DevExpress.Office.Printing.OfficeTextBrick OfficeTextBrick =>
            (DevExpress.Office.Printing.OfficeTextBrick) base.Brick;
    }
}

