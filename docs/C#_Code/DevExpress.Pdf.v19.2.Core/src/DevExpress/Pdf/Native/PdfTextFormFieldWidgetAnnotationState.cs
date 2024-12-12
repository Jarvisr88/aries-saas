namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextFormFieldWidgetAnnotationState : PdfTextBasedAnnotationState<PdfTextFormField>
    {
        public PdfTextFormFieldWidgetAnnotationState(PdfPageState pageState, PdfWidgetAnnotation annotation, PdfTextFormField formField) : base(pageState, annotation, formField)
        {
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override PdfEditorSettings GetEditorSettings(bool readOnly) => 
            new PdfTextEditSettings(base.FontSearch, base.InteractiveArea, base.FormField, base.SummaryRotationAngle, readOnly);
    }
}

