namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class PdfAcroFormField
    {
        private const string period = ".";
        private string name;

        protected PdfAcroFormField(string name)
        {
            this.Name = name;
        }

        internal virtual void CollectNameCollisionInfo(IList<PdfAcroFormFieldNameCollision> infos)
        {
        }

        public static PdfAcroFormCheckBoxField CreateCheckBox(string name, int pageNumber, PdfRectangle rect) => 
            new PdfAcroFormCheckBoxField(name, pageNumber, rect);

        public static PdfAcroFormComboBoxField CreateComboBox(string name, int pageNumber, PdfRectangle rect) => 
            new PdfAcroFormComboBoxField(name, pageNumber, rect);

        protected internal abstract PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent);
        public static PdfAcroFormGroupField CreateGroup(string name) => 
            new PdfAcroFormGroupField(name);

        public static PdfAcroFormListBoxField CreateListBox(string name, int pageNumber, PdfRectangle rect) => 
            new PdfAcroFormListBoxField(name, pageNumber, rect);

        public static PdfAcroFormRadioGroupField CreateRadioGroup(string name, int pageNumber) => 
            new PdfAcroFormRadioGroupField(name, pageNumber);

        public static PdfAcroFormSignatureField CreateSignature(string name, int pageNumber, PdfRectangle rect) => 
            new PdfAcroFormSignatureField(name, pageNumber, rect);

        public static PdfAcroFormTextBoxField CreateTextBox(string name, int pageNumber, PdfRectangle rect) => 
            new PdfAcroFormTextBoxField(name, pageNumber, rect);

        internal static void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAcroFormFieldNameCantBeEmpty));
            }
            if (name.Contains("."))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectAcroFormFieldNameContainsPeriod));
            }
        }

        public string Name
        {
            get => 
                this.name;
            set
            {
                ValidateName(value);
                this.name = value;
            }
        }

        public string ToolTip { get; set; }

        protected internal virtual PdfInteractiveFormFieldFlags Flags =>
            PdfInteractiveFormFieldFlags.None;
    }
}

