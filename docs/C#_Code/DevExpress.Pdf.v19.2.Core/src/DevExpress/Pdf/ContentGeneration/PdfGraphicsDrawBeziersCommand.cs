namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawBeziersCommand : PdfGraphicsStrokingCommand
    {
        private readonly PointF[] points;

        public PdfGraphicsDrawBeziersCommand(Pen pen, PointF[] points) : base(pen)
        {
            this.points = (PointF[]) points.Clone();
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawBeziers(this.points);
        }
    }
}

