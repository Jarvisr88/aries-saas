namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;

    public class PdfEllipseFillingStrategy : PdfShapeFillingStrategy
    {
        private readonly PdfPoint[] points;
        private readonly PdfRectangle transformedBBox;

        public PdfEllipseFillingStrategy(RectangleF bBox, PdfRectangle transformedBBox)
        {
            this.transformedBBox = transformedBBox;
            this.points = GetRectanglePoints(bBox);
        }

        public override void Clip(PdfCommandConstructor constructor)
        {
            constructor.IntersectClipByEllipse(this.transformedBBox);
        }

        public override PdfPoint[] ShapePoints =>
            this.points;
    }
}

