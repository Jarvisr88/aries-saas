namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfTextBasedAnnotationState<T> : PdfWidgetAnnotationState<T> where T: PdfInteractiveFormField
    {
        private bool editorVisible;

        protected PdfTextBasedAnnotationState(PdfPageState pageState, PdfWidgetAnnotation annotation, T formField) : base(pageState, annotation, formField)
        {
        }

        public abstract PdfEditorSettings GetEditorSettings(bool readOnly);
        protected override bool ShouldDrawAnnotation(bool isPrinting) => 
            base.ShouldDrawAnnotation(isPrinting) && !this.EditorVisible;

        public bool EditorVisible
        {
            get => 
                this.editorVisible;
            set
            {
                if (this.editorVisible != value)
                {
                    this.editorVisible = value;
                    base.RaiseStateChanged();
                }
            }
        }

        protected override bool ShouldPaintFocusRect =>
            false;
    }
}

