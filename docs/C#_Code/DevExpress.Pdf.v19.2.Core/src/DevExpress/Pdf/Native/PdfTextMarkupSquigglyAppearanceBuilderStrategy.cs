namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextMarkupSquigglyAppearanceBuilderStrategy : PdfTextMarkupAppearanceBuilderStrategy
    {
        private const double radToDegFactor = 57.295779513082323;
        private const double widthToHeightFactor = 1.8;
        private const double patternHeight = 12.0;
        private readonly PdfTilingPattern pattern;

        public PdfTextMarkupSquigglyAppearanceBuilderStrategy(PdfTextMarkupAnnotation annotation, PdfFormCommandConstructor constructor) : base(annotation, constructor)
        {
            this.pattern = new PdfTilingPattern(new PdfTransformationMatrix(), new PdfRectangle(0.0, 0.0, 10.0, 12.0), 10.0, 13.0, false, constructor.DocumentCatalog);
            using (PdfCommandConstructor constructor2 = new PdfCommandConstructor(this.pattern.Resources))
            {
                constructor2.SaveGraphicsState();
                constructor2.SetLineCapStyle(PdfLineCapStyle.Round);
                constructor2.SetLineJoinStyle(PdfLineJoinStyle.Round);
                constructor2.SetLineWidth(1.0);
                constructor2.SetLineStyle(PdfLineStyle.CreateSolid());
                PdfPoint[] points = new PdfPoint[] { new PdfPoint(0.0, 1.0), new PdfPoint(5.0, 11.0), new PdfPoint(10.0, 1.0) };
                constructor2.DrawLines(points);
                constructor2.RestoreGraphicsState();
                this.pattern.ReplaceCommands(constructor2.Commands);
            }
        }

        protected override void RebuildQuad(PdfQuadrilateral quad)
        {
            PdfTextMarkupAnnotation annotation = base.Annotation;
            PdfCommandConstructor constructor = base.Constructor;
            PdfDocumentCatalog documentCatalog = constructor.DocumentCatalog;
            PdfRectangle rect = annotation.Rect;
            PdfColor color = annotation.Color;
            double scaleY = PdfPoint.Distance(quad.P2, quad.P4) / 72.0;
            double scaleX = 1.8 * scaleY;
            double right = PdfPoint.Distance(quad.P3, quad.P4) / scaleX;
            PdfTransformationMatrix matrix = PdfTransformationMatrix.CreateRotate(Math.Atan2(quad.P4.Y - quad.P3.Y, quad.P4.X - quad.P3.X) * 57.295779513082323);
            PdfTransformationMatrix matrix3 = PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(PdfTransformationMatrix.CreateScale(scaleX, scaleY), matrix), new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, quad.P3.X - rect.Left, (quad.P3.Y - rect.Bottom) + (1.285 * scaleY)));
            PdfForm form = new PdfForm(documentCatalog, new PdfRectangle(-0.5, -0.5, right + 0.5, 12.5)) {
                Matrix = new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, 0.5, 0.5)
            };
            using (PdfFormCommandConstructor constructor2 = new PdfFormCommandConstructor(form))
            {
                constructor2.SaveGraphicsState();
                constructor2.SetColorForNonStrokingOperations(new PdfColor(this.pattern, (color == null) ? new double[0] : color.Components));
                constructor2.FillRectangle(new PdfRectangle(0.0, 0.0, right, 12.0));
                constructor2.RestoreGraphicsState();
                form.ReplaceCommands(constructor2.Commands);
            }
            constructor.DrawForm(documentCatalog.Objects.AddResolvedObject(form), matrix3);
        }
    }
}

