namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfButtonFormFieldWidgetAnnotationState : PdfWidgetAnnotationState<PdfButtonFormField>
    {
        public PdfButtonFormFieldWidgetAnnotationState(PdfPageState pageState, PdfWidgetAnnotation annotation, PdfButtonFormField formField) : base(pageState, annotation, formField)
        {
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsPushButton =>
            base.FormField.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton);

        protected override bool IsEditableFormField =>
            !this.IsPushButton;
    }
}

