namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PdfGraphicsFillPathCommand : PdfGraphicsNonStrokingCommand
    {
        private readonly PointF[] pathPoints;
        private readonly byte[] pathTypes;
        private readonly FillMode fillMode;

        public PdfGraphicsFillPathCommand(Brush brush, GraphicsPath path) : base(brush)
        {
            this.fillMode = FillMode.Winding;
            this.pathPoints = path.PathPoints;
            this.pathTypes = path.PathTypes;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            base.Execute(constructor, page);
            constructor.FillPath(this.pathPoints, this.pathTypes, this.fillMode == FillMode.Winding);
        }
    }
}

