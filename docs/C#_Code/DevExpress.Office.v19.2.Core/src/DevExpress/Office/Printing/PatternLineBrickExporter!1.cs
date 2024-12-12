namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Model;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;

    public abstract class PatternLineBrickExporter<T> : VisualBrickExporter where T: struct
    {
        protected PatternLineBrickExporter()
        {
        }

        protected abstract IPatternLinePainter<T> CreateLinePainter(IGraphicsPainter painter, DocumentLayoutUnitConverter unitConverter);
        protected override void DrawClientContent(IGraphics gr, RectangleF clientRect)
        {
            using (IGraphicsPainter painter = new IGraphicsPainter(gr))
            {
                this.PatternLineBrick.GetPatternLine().Draw(this.CreateLinePainter(painter, this.PatternLineBrick.GetPainterUnitConverter(gr)), this.PatternLineBrick.CalculateLineBounds(gr, clientRect), this.PatternLineBrick.BorderColor);
            }
        }

        protected PatternLineBrick<T> PatternLineBrick =>
            base.Brick as PatternLineBrick<T>;
    }
}

