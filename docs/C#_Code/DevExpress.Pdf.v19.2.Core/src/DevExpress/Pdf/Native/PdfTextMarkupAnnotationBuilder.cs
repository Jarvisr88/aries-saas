namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PdfTextMarkupAnnotationBuilder : PdfMarkupAnnotationBuilder, IPdfTextMarkupAnnotationBuilder, IPdfMarkupAnnotationBuilder, IPdfAnnotationBuilder
    {
        private const double xScaleFactor = 0.26696;
        private PdfTextMarkupAnnotationType style;
        private IList<PdfQuadrilateral> quads;

        public PdfTextMarkupAnnotationBuilder(PdfTextMarkupAnnotation annotation) : base(annotation)
        {
            this.quads = annotation.Quads;
            this.style = annotation.Type;
        }

        private PdfTextMarkupAnnotationBuilder(PdfRectangle bounds, PdfTextMarkupAnnotationType style) : base(bounds)
        {
            this.style = style;
            base.Subject = PdfTextMarkupAnnotationData.GetSubject(style);
            PdfColor color = (style == PdfTextMarkupAnnotationType.Highlight) ? PdfTextMarkupAnnotationData.HighlightDefaultColor : ((style == PdfTextMarkupAnnotationType.StrikeOut) ? PdfTextMarkupAnnotationData.StrikeOutDefaultColor : PdfTextMarkupAnnotationData.UnderlineDefaultColor);
            base.Color = new PdfRGBColor(color);
        }

        public PdfTextMarkupAnnotationBuilder(IEnumerable<PdfOrientedRectangle> rectangles, PdfTextMarkupAnnotationType style) : this(GetTextMarkupBounds(rectangles), style)
        {
            this.quads = new List<PdfQuadrilateral>();
            foreach (PdfOrientedRectangle rectangle in rectangles)
            {
                this.quads.Add(new PdfQuadrilateral(rectangle));
            }
        }

        public PdfTextMarkupAnnotationBuilder(IEnumerable<PdfQuadrilateral> quads, PdfTextMarkupAnnotationType style) : this(GetTextMarkupBounds(quads), style)
        {
            this.quads = new List<PdfQuadrilateral>(quads);
        }

        public void Apply(PdfTextMarkupAnnotation annotation)
        {
            annotation.Type = this.style;
            annotation.Opacity = base.Opacity;
            annotation.CreationDate = base.CreationDate;
            annotation.Title = base.Title;
            annotation.Subject = base.Subject;
            annotation.Name = base.Name;
            PdfRGBColor color = base.Color;
            double[] components = new double[] { color.R, color.G, color.B };
            annotation.Color = new PdfColor(components);
            annotation.Modified = base.ModificationDate;
            annotation.Contents = base.Contents;
        }

        private static PdfRectangle GetTextMarkupBounds(IEnumerable<PdfOrientedRectangle> textRectangles)
        {
            List<PdfPoint> points = new List<PdfPoint>();
            foreach (PdfOrientedRectangle rectangle in textRectangles)
            {
                points.AddRange(rectangle.GetBoundingBoxPoints(0.26696, 0.031264));
            }
            return PdfRectangle.CreateBoundingBox(points);
        }

        private static PdfRectangle GetTextMarkupBounds(IEnumerable<PdfQuadrilateral> quads)
        {
            List<PdfPoint> points = new List<PdfPoint>();
            foreach (PdfQuadrilateral quadrilateral in quads)
            {
                PdfPoint[] pointArray1 = new PdfPoint[] { quadrilateral.P1, quadrilateral.P2, quadrilateral.P3, quadrilateral.P4 };
                PdfRectangle rectangle = PdfRectangle.CreateBoundingBox(pointArray1);
                double num = PdfMathUtils.Max(PdfPoint.Distance(quadrilateral.P1, quadrilateral.P3), PdfPoint.Distance(quadrilateral.P2, quadrilateral.P4)) * 0.26696;
                double x = rectangle.Left - num;
                double y = rectangle.Bottom - num;
                double num4 = rectangle.Right + num;
                double num5 = rectangle.Top + num;
                points.Add(new PdfPoint(x, y));
                points.Add(new PdfPoint(num4, y));
                points.Add(new PdfPoint(num4, num5));
                points.Add(new PdfPoint(x, num5));
            }
            return PdfRectangle.CreateBoundingBox(points);
        }

        public PdfTextMarkupAnnotationType Style
        {
            get => 
                this.style;
            set => 
                this.style = value;
        }

        public IList<PdfQuadrilateral> Quads =>
            new ReadOnlyCollection<PdfQuadrilateral>(this.quads);
    }
}

