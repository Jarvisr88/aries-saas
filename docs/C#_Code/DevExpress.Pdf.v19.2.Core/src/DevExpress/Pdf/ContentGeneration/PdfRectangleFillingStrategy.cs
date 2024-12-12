namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;

    public class PdfRectangleFillingStrategy : PdfShapeFillingStrategy
    {
        private readonly PdfRectangle transformedRectangle;
        private readonly PdfPoint[] points;

        public PdfRectangleFillingStrategy(RectangleF rectangle, PdfRectangle transformedRectangle)
        {
            this.transformedRectangle = transformedRectangle;
            this.points = GetRectanglePoints(rectangle);
        }

        public override void Clip(PdfCommandConstructor constructor)
        {
            constructor.IntersectClip(this.transformedRectangle);
        }

        public override PdfPoint[] ShapePoints =>
            this.points;
    }
}

