namespace DevExpress.Office.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;

    public class OfficePanelBrickExporter : PanelBrickExporter
    {
        protected override void DrawBackground(IGraphics gr, RectangleF rect)
        {
            if (gr is PdfGraphics)
            {
                base.DrawBackground(gr, rect);
            }
            else
            {
                Rectangle rectangle = Rectangle.Round(this.OfficePanelBrick.unitConverter.DocumentsToLayoutUnits(rect));
                base.DrawBackground(gr, rectangle);
            }
        }

        protected override RectangleF GetClipRect(RectangleF rect, IGraphics gr) => 
            !(gr is PdfGraphics) ? this.OfficePanelBrick.Padding.Deflate(this.OfficePanelBrick.unitConverter.DocumentsToLayoutUnits(base.BrickPaint.GetClientRect(rect, false)), this.OfficePanelBrick.unitConverter.Dpi) : base.GetClipRect(rect, gr);

        private DevExpress.Office.Printing.OfficePanelBrick OfficePanelBrick =>
            (DevExpress.Office.Printing.OfficePanelBrick) base.Brick;
    }
}

