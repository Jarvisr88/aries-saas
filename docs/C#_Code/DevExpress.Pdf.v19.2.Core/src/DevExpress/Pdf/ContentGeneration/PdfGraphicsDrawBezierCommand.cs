namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawBezierCommand : PdfGraphicsStrokingCommand
    {
        private readonly PointF pt1;
        private readonly PointF pt2;
        private readonly PointF pt3;
        private readonly PointF pt4;

        public PdfGraphicsDrawBezierCommand(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4) : base(pen)
        {
            this.pt1 = pt1;
            this.pt2 = pt2;
            this.pt3 = pt3;
            this.pt4 = pt4;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawBezier(this.pt1, this.pt2, this.pt3, this.pt4);
        }
    }
}

