namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PdfInkAnnotationState : PdfMarkupAnnotationState
    {
        public PdfInkAnnotationState(PdfPageState pageState, PdfInkAnnotation annotation) : base(pageState, annotation)
        {
            PdfRectangle rect = annotation.Rect;
            Func<PdfPoint[], IEnumerable<PdfPoint>> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<PdfPoint[], IEnumerable<PdfPoint>> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = (Func<PdfPoint[], IEnumerable<PdfPoint>>) (ink => ink);
            }
            PdfRectangle rectangle1 = PdfRectangle.CreateBoundingBox(annotation.Inks.SelectMany<PdfPoint[], PdfPoint>(selector).ToArray<PdfPoint>());
            PdfRectangle rectangle3 = rectangle1;
            if (rectangle1 == null)
            {
                PdfRectangle local2 = rectangle1;
                rectangle3 = rect;
            }
            PdfRectangle rectangle2 = rectangle3;
            if (annotation.BorderStyle != null)
            {
                rectangle2 = PdfRectangle.Inflate(rectangle2, -annotation.BorderStyle.Width);
            }
            if (!rect.Contains(rectangle2))
            {
                annotation.Rect = rectangle2;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfInkAnnotationState.<>c <>9 = new PdfInkAnnotationState.<>c();
            public static Func<PdfPoint[], IEnumerable<PdfPoint>> <>9__0_0;

            internal IEnumerable<PdfPoint> <.ctor>b__0_0(PdfPoint[] ink) => 
                ink;
        }
    }
}

