namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfTextEditSettings : PdfEditorSettings
    {
        private const string separator = "\r\n";
        private readonly string initialText;
        private readonly bool multiline;
        private readonly int maxLen;
        private readonly bool autoTextSize;
        private readonly PdfTextFieldMeasurer measurer;

        public PdfTextEditSettings(IPdfExportFontProvider fontSearch, PdfDocumentArea area, PdfTextFormField formField, int rotationAngle, bool readOnly) : base(fontSearch, area, formField, rotationAngle, readOnly)
        {
            string text1;
            string text = formField.Text;
            if (string.IsNullOrEmpty(text))
            {
                text1 = string.Empty;
            }
            else
            {
                string[] separator = new string[] { "\r\n", "\r", "\n" };
                text1 = string.Join("\r\n", text.Split(separator, StringSplitOptions.None));
            }
            this.initialText = text1;
            this.multiline = formField.Flags.HasFlag(PdfInteractiveFormFieldFlags.Multiline);
            int? maxLen = formField.MaxLen;
            this.maxLen = (maxLen != null) ? maxLen.GetValueOrDefault() : 0;
            this.<DoNotScroll>k__BackingField = formField.Flags.HasFlag(PdfInteractiveFormFieldFlags.DoNotScroll);
            this.measurer = new PdfTextFormFieldAppearanceBuilder(formField.Widget, formField, fontSearch, new PdfRgbaColor(0.0, 0.0, 0.0, 0.0)).CreateMeasurer();
            this.autoTextSize = formField.TextState.FontSize == 0.0;
        }

        public bool OnEditValueChanging(string newValue)
        {
            if (this.autoTextSize)
            {
                base.FontSize = this.measurer.CalcFontSize(newValue, base.FontData, (float) base.FontSize);
            }
            return (!this.DoNotScroll || this.measurer.IsTextFit(newValue, new PdfExportFontInfo(base.FontData, (float) base.FontSize)));
        }

        public string InitialText =>
            this.initialText;

        public bool Multiline =>
            this.multiline;

        public int MaxLen =>
            this.maxLen;

        public bool DoNotScroll { get; }

        public override PdfEditorType EditorType =>
            PdfEditorType.TextEdit;

        public override object EditValue =>
            this.initialText;
    }
}

