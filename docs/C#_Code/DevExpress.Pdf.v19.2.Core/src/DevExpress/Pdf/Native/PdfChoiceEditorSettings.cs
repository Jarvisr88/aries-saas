namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfChoiceEditorSettings : PdfEditorSettings
    {
        private readonly IList<PdfOptionsFormFieldOption> options;
        private readonly int topIndex;

        protected PdfChoiceEditorSettings(IPdfExportFontProvider fontSearch, PdfDocumentArea area, PdfChoiceFormField formField, int rotationAngle, bool readOnly) : base(fontSearch, area, formField, rotationAngle, readOnly)
        {
            this.options = formField.Options;
            this.topIndex = formField.TopIndex;
        }

        public IList<PdfOptionsFormFieldOption> Values =>
            this.options;

        public int TopIndex =>
            this.topIndex;

        public override PdfEditorType EditorType =>
            PdfEditorType.ListBox;
    }
}

