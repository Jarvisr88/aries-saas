namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawLinesCommand : PdfGraphicsStrokingCommand
    {
        private readonly PointF[] points;

        public PdfGraphicsDrawLinesCommand(Pen pen, PointF[] points) : base(pen)
        {
            this.points = points;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            if ((this.points != null) && (this.points.Length > 1))
            {
                base.Execute(constructor, page);
                constructor.DrawLines(this.points);
            }
        }
    }
}

