namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfWidgetAnnotationStateFactory : PdfVisitorBasedFactory<PdfWidgetAnnotation, PdfAnnotationState>, IPdfInteractiveFormFieldVisitor
    {
        private readonly PdfPageState pageState;
        private PdfWidgetAnnotation widget;

        public PdfWidgetAnnotationStateFactory(PdfPageState pageState)
        {
            this.pageState = pageState;
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfButtonFormField formField)
        {
            base.SetResult(new PdfButtonFormFieldWidgetAnnotationState(this.pageState, this.widget, formField));
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfChoiceFormField formField)
        {
            base.SetResult(new PdfChoiceFormFieldWidgetAnnotationState(this.pageState, this.widget, formField));
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfInteractiveFormField formField)
        {
            base.SetResult(new PdfCommonAnnotationState(this.pageState, this.widget));
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfTextFormField formField)
        {
            base.SetResult(new PdfTextFormFieldWidgetAnnotationState(this.pageState, this.widget, formField));
        }

        protected override void Visit(PdfWidgetAnnotation widget)
        {
            this.widget = widget;
            if (widget.InteractiveFormField != null)
            {
                widget.InteractiveFormField.Accept(this);
            }
            else
            {
                base.SetResult(new PdfCommonAnnotationState(this.pageState, widget));
            }
        }
    }
}

