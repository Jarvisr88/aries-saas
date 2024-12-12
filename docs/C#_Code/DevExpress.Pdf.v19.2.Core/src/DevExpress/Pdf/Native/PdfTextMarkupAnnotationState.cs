namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfTextMarkupAnnotationState : PdfMarkupAnnotationState
    {
        private const double selectionXFactor = 0.26696;
        private const double selectionYFactor = 0.1;
        private readonly PdfTextMarkupAnnotation markupAnnotation;
        private readonly IList<PdfPoint[]> selectionPolygon;

        public PdfTextMarkupAnnotationState(PdfPageState pageState, PdfTextMarkupAnnotation annotation) : base(pageState, annotation)
        {
            this.markupAnnotation = annotation;
            if (this.markupAnnotation.Quads != null)
            {
                this.selectionPolygon = new List<PdfPoint[]>();
                foreach (PdfQuadrilateral quadrilateral in this.markupAnnotation.Quads)
                {
                    double height = PdfMathUtils.Max(PdfPoint.Distance(quadrilateral.P1, quadrilateral.P3), PdfPoint.Distance(quadrilateral.P2, quadrilateral.P4));
                    PdfPoint point = CalcOffsetPoint(quadrilateral.P1, quadrilateral.P2, quadrilateral.P3, height);
                    PdfPoint point2 = CalcOffsetPoint(quadrilateral.P2, quadrilateral.P1, quadrilateral.P4, height);
                    PdfPoint point3 = CalcOffsetPoint(quadrilateral.P3, quadrilateral.P4, quadrilateral.P1, height);
                    PdfPoint point4 = CalcOffsetPoint(quadrilateral.P4, quadrilateral.P3, quadrilateral.P2, height);
                    PdfPoint[] item = new PdfPoint[] { point, point2, point4, point3 };
                    this.selectionPolygon.Add(item);
                }
            }
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        private static PdfPoint CalcOffsetPoint(PdfPoint point, PdfPoint horizontalPoint, PdfPoint verticalPoint, double height)
        {
            double t = 1.0 + ((height * 0.26696) / PdfPoint.Distance(horizontalPoint, point));
            double num2 = 1.0 + ((height * 0.1) / PdfPoint.Distance(verticalPoint, point));
            return new PdfPoint(PdfPoint.Lerp(horizontalPoint, point, t).X, PdfPoint.Lerp(verticalPoint, point, num2).Y);
        }

        protected override bool ContainsPoint(PdfPoint point)
        {
            bool flag;
            if (this.markupAnnotation.Quads == null)
            {
                return base.ContainsPoint(point);
            }
            using (IEnumerator<PdfQuadrilateral> enumerator = this.markupAnnotation.Quads.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfQuadrilateral current = enumerator.Current;
                        if (!current.Contains(point))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public PdfTextMarkupAnnotationData CreateMarkupAnnotationData() => 
            new PdfTextMarkupAnnotationData(this);

        public override IList<PdfPoint[]> GetSelectionPolygon() => 
            (this.selectionPolygon != null) ? this.selectionPolygon : base.GetSelectionPolygon();

        public override void RebuildAppearance()
        {
            PdfForm appearanceForm = this.markupAnnotation.GetAppearanceForm(PdfAnnotationAppearanceState.Normal);
            appearanceForm ??= this.markupAnnotation.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal);
            this.markupAnnotation.CreateAppearanceBuilder(base.FontSearch).RebuildAppearance(appearanceForm);
            base.RaiseAppearanceChanged();
        }

        protected override PdfAnnotation Annotation =>
            this.markupAnnotation;

        public override bool AcceptsTabStop =>
            base.Visible;

        public PdfTextMarkupAnnotation MarkupAnnotation =>
            this.markupAnnotation;
    }
}

