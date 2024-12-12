namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCommonAnnotationState : PdfAnnotationState
    {
        private readonly PdfAnnotation annotation;

        public PdfCommonAnnotationState(PdfPageState pageState, PdfAnnotation annotation) : base(pageState, annotation)
        {
            this.annotation = annotation;
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override PdfAnnotation Annotation =>
            this.annotation;
    }
}

