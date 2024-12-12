namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfAnnotationStateFactory : PdfVisitorBasedFactory<PdfAnnotation, PdfAnnotationState>, IPdfAnnotationVisitor
    {
        private readonly PdfPageState pageState;

        public PdfAnnotationStateFactory(PdfPageState pageState)
        {
            this.pageState = pageState;
        }

        void IPdfAnnotationVisitor.Visit(PdfAnnotation annotation)
        {
            base.SetResult(new PdfCommonAnnotationState(this.pageState, annotation));
        }

        void IPdfAnnotationVisitor.Visit(PdfInkAnnotation annotation)
        {
            base.SetResult(new PdfInkAnnotationState(this.pageState, annotation));
        }

        void IPdfAnnotationVisitor.Visit(PdfLinkAnnotation link)
        {
            base.SetResult(new PdfLinkAnnotationState(this.pageState, link));
        }

        void IPdfAnnotationVisitor.Visit(PdfMarkupAnnotation markup)
        {
            base.SetResult(new PdfMarkupAnnotationState(this.pageState, markup));
        }

        void IPdfAnnotationVisitor.Visit(PdfPopupAnnotation popup)
        {
        }

        void IPdfAnnotationVisitor.Visit(PdfTextMarkupAnnotation markup)
        {
            base.SetResult(new PdfTextMarkupAnnotationState(this.pageState, markup));
        }

        void IPdfAnnotationVisitor.Visit(PdfWidgetAnnotation widget)
        {
            base.SetResult(new PdfWidgetAnnotationStateFactory(this.pageState).Create(widget));
        }

        protected override void Visit(PdfAnnotation input)
        {
            input.Accept(this);
        }
    }
}

