namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAcroFormCheckBoxField : PdfAcroFormCommonVisualField
    {
        private PdfAcroFormButtonStyle buttonStyle;
        private string exportValue;
        private bool shouldGeneratePressedAppearance;

        public PdfAcroFormCheckBoxField(string name, int pageNumber, PdfRectangle rect) : base(name, pageNumber, rect)
        {
            this.buttonStyle = PdfAcroFormButtonStyle.Check;
            this.exportValue = "Yes";
            this.shouldGeneratePressedAppearance = true;
        }

        protected internal override PdfCommandList CreateAppearanceCommands(IPdfExportFontProvider fontSearch, PdfInteractiveForm interactiveForm) => 
            null;

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) => 
            new PdfButtonFormField(this, fontSearch, document, parent);

        protected internal override PdfWidgetAnnotationBuilder CreateWidgetBuilder(PdfRectangle rect) => 
            base.CreateWidgetBuilder(rect).SetButtonStyle(this.buttonStyle);

        internal static void ValidateExportValue(string exportName)
        {
            if (string.IsNullOrEmpty(exportName))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectAcroFormExportValue));
            }
        }

        public bool IsChecked { get; set; }

        public PdfAcroFormButtonStyle ButtonStyle
        {
            get => 
                this.buttonStyle;
            set => 
                this.buttonStyle = value;
        }

        public bool ShouldGeneratePressedAppearance
        {
            get => 
                this.shouldGeneratePressedAppearance;
            set => 
                this.shouldGeneratePressedAppearance = value;
        }

        public string ExportValue
        {
            get => 
                this.exportValue;
            set
            {
                ValidateExportValue(value);
                this.exportValue = value;
            }
        }
    }
}

