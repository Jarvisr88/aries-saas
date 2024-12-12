namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawPolygonCommand : PdfGraphicsStrokingCommand
    {
        private readonly PointF[] points;

        public PdfGraphicsDrawPolygonCommand(Pen pen, PointF[] points) : base(pen)
        {
            this.points = points;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            if ((this.points != null) && (this.points.Length > 2))
            {
                base.Execute(constructor, page);
                constructor.DrawPolygon(this.points);
            }
        }
    }
}

