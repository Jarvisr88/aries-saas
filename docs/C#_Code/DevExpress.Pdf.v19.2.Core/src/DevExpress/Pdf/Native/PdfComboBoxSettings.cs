namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfComboBoxSettings : PdfChoiceEditorSettings
    {
        private readonly bool editable;
        private readonly string selectedValue;

        public PdfComboBoxSettings(IPdfExportFontProvider fontSearch, PdfDocumentArea documentArea, PdfChoiceFormField field, int rotationAngle, bool readOnly) : base(fontSearch, documentArea, field, rotationAngle, readOnly)
        {
            this.editable = field.Flags.HasFlag(PdfInteractiveFormFieldFlags.Edit);
            IList<string> selectedValues = field.SelectedValues;
            this.selectedValue = ((selectedValues == null) || (selectedValues.Count == 0)) ? string.Empty : selectedValues[0];
        }

        public bool Editable =>
            this.editable;

        public override object EditValue =>
            this.selectedValue;

        public override PdfEditorType EditorType =>
            PdfEditorType.ComboBox;
    }
}

