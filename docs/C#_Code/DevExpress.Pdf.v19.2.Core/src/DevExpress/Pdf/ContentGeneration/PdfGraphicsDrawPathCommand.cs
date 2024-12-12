namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfGraphicsDrawPathCommand : PdfGraphicsStrokingCommand
    {
        private readonly PointF[] pathPoints;
        private readonly byte[] pathTypes;

        public PdfGraphicsDrawPathCommand(Pen pen, GraphicsPath path) : base(pen)
        {
            this.pathPoints = path.PathPoints;
            this.pathTypes = path.PathTypes;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.DrawPath(this.pathPoints, this.pathTypes);
        }
    }
}

