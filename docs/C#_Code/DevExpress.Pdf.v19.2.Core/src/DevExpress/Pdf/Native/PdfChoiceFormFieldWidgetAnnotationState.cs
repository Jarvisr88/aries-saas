namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfChoiceFormFieldWidgetAnnotationState : PdfTextBasedAnnotationState<PdfChoiceFormField>
    {
        public PdfChoiceFormFieldWidgetAnnotationState(PdfPageState pageState, PdfWidgetAnnotation annotation, PdfChoiceFormField formField) : base(pageState, annotation, formField)
        {
            pageState.DocumentState.DocumentStateChanged += new PdfDocumentStateChangedEventHandler(this.DocumentStateChanged);
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void DocumentStateChanged(object sender, PdfDocumentStateChangedEventArgs e)
        {
            if (base.FormField.IsCombo && e.Flags.HasFlag(PdfDocumentStateChangedFlags.AllContent))
            {
                base.RaiseAppearanceChanged();
            }
        }

        public override PdfEditorSettings GetEditorSettings(bool readOnly)
        {
            PdfChoiceFormField formField = base.FormField;
            return (!formField.IsCombo ? ((PdfEditorSettings) new PdfListBoxSettings(base.FontSearch, base.InteractiveArea, formField, base.SummaryRotationAngle, readOnly)) : ((PdfEditorSettings) new PdfComboBoxSettings(base.FontSearch, base.InteractiveArea, formField, base.SummaryRotationAngle, readOnly)));
        }

        public override bool CanOpenEditorInReadOnlyMode =>
            base.FormField.IsCombo;
    }
}

