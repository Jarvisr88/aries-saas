namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfMarkupAnnotationDataFactory : PdfVisitorBasedFactory<PdfAnnotationState, PdfMarkupAnnotationData>, IPdfAnnotationStateVisitor
    {
        void IPdfAnnotationStateVisitor.Visit(PdfButtonFormFieldWidgetAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfChoiceFormFieldWidgetAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfCommonAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfLinkAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfMarkupAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfTextFormFieldWidgetAnnotationState state)
        {
        }

        void IPdfAnnotationStateVisitor.Visit(PdfTextMarkupAnnotationState state)
        {
            base.SetResult(new PdfTextMarkupAnnotationData(state));
        }

        protected override void Visit(PdfAnnotationState input)
        {
            input.Accept(this);
        }
    }
}

