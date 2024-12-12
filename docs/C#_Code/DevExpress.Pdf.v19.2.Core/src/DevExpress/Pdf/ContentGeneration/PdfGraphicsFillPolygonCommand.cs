namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsFillPolygonCommand : PdfGraphicsNonStrokingCommand
    {
        private readonly PointF[] points;

        public PdfGraphicsFillPolygonCommand(Brush brush, PointF[] points) : base(brush)
        {
            this.points = points;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            if ((this.points != null) && (this.points.Length > 2))
            {
                base.Execute(constructor, page);
                constructor.FillPolygon(this.points, true);
            }
        }
    }
}

