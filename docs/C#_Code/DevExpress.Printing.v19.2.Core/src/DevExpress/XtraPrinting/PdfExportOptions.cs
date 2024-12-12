namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions")]
    public class PdfExportOptions : PageByPageExportOptionsBase, IXtraSupportShouldSerialize
    {
        private PdfDocumentOptions documentOptions;
        private PdfPasswordSecurityOptions passwordSecurityOptions;
        private PdfJpegImageQuality imageQuality;
        private PdfSignatureOptions signatureOptions;
        private string neverEmbeddedFonts;
        private bool compressed;
        private bool showPrintDialogOnOpen;
        private bool convertImagesToJpeg;
        private bool exportEditingFieldsToAcroForms;
        private DevExpress.XtraPrinting.PdfACompatibility pdfACompatibility;
        private string additionalMetadata;
        private List<PdfAttachment> attachments;

        public PdfExportOptions()
        {
            this.documentOptions = new PdfDocumentOptions();
            this.passwordSecurityOptions = new PdfPasswordSecurityOptions();
            this.imageQuality = PdfJpegImageQuality.Highest;
            this.signatureOptions = new PdfSignatureOptions();
            this.neverEmbeddedFonts = string.Empty;
            this.compressed = true;
            this.convertImagesToJpeg = true;
            this.attachments = new List<PdfAttachment>();
        }

        private PdfExportOptions(PdfExportOptions source) : base(source)
        {
            this.documentOptions = new PdfDocumentOptions();
            this.passwordSecurityOptions = new PdfPasswordSecurityOptions();
            this.imageQuality = PdfJpegImageQuality.Highest;
            this.signatureOptions = new PdfSignatureOptions();
            this.neverEmbeddedFonts = string.Empty;
            this.compressed = true;
            this.convertImagesToJpeg = true;
            this.attachments = new List<PdfAttachment>();
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            PdfExportOptions options = (PdfExportOptions) source;
            this.documentOptions.Assign(options.documentOptions);
            this.passwordSecurityOptions.Assign(options.passwordSecurityOptions);
            this.convertImagesToJpeg = options.convertImagesToJpeg;
            this.exportEditingFieldsToAcroForms = options.ExportEditingFieldsToAcroForms;
            this.compressed = options.compressed;
            this.neverEmbeddedFonts = options.neverEmbeddedFonts;
            this.imageQuality = options.imageQuality;
            this.showPrintDialogOnOpen = options.showPrintDialogOnOpen;
            this.signatureOptions.Assign(options.signatureOptions);
            this.pdfACompatibility = options.PdfACompatibility;
            this.additionalMetadata = options.additionalMetadata;
            this.attachments.Clear();
            foreach (PdfAttachment attachment in options.attachments)
            {
                this.attachments.Add(attachment.Clone());
            }
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new PdfExportOptions(this);

        protected internal override void Correct()
        {
            if (this.pdfACompatibility != DevExpress.XtraPrinting.PdfACompatibility.None)
            {
                this.neverEmbeddedFonts = string.Empty;
                this.showPrintDialogOnOpen = false;
                this.passwordSecurityOptions.OpenPassword = string.Empty;
                this.passwordSecurityOptions.PermissionsPassword = string.Empty;
                if (this.pdfACompatibility != DevExpress.XtraPrinting.PdfACompatibility.PdfA3b)
                {
                    this.attachments.Clear();
                }
            }
            if (this.pdfACompatibility == DevExpress.XtraPrinting.PdfACompatibility.PdfA1b)
            {
                this.exportEditingFieldsToAcroForms = false;
            }
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName == "DocumentOptions") ? this.ShouldSerializeDocumentOptions() : ((propertyName == "PasswordSecurityOptions") ? this.ShouldSerializePasswordSecurityOptions() : ((propertyName == "SignatureOptions") ? this.ShouldSerializeSignatureOptions() : true));

        protected internal override bool ShouldSerialize() => 
            this.ShouldSerializeDocumentOptions() || (this.ShouldSerializePasswordSecurityOptions() || (this.ShouldSerializeSignatureOptions() || ((this.imageQuality != PdfJpegImageQuality.Highest) || (!this.compressed || (this.showPrintDialogOnOpen || ((this.neverEmbeddedFonts != "") || (base.ShouldSerialize() || (!this.convertImagesToJpeg || (this.exportEditingFieldsToAcroForms || (this.pdfACompatibility != DevExpress.XtraPrinting.PdfACompatibility.None))))))))));

        private bool ShouldSerializeDocumentOptions() => 
            this.DocumentOptions.ShouldSerialize();

        private bool ShouldSerializePasswordSecurityOptions() => 
            this.PasswordSecurityOptions.ShouldSerialize();

        private bool ShouldSerializeSignatureOptions() => 
            this.SignatureOptions.ShouldSerialize();

        public IList<string> Validate()
        {
            IList<string> list = new List<string>();
            DevExpress.XtraPrinting.PdfACompatibility pdfACompatibility = this.pdfACompatibility;
            if ((((pdfACompatibility == DevExpress.XtraPrinting.PdfACompatibility.PdfA1b) || (pdfACompatibility == DevExpress.XtraPrinting.PdfACompatibility.PdfA2b)) && (this.attachments != null)) && (this.attachments.Count > 0))
            {
                list.Add("File attachments are not supported in PDF/A-1b and PDF/A-2b documents.");
            }
            if (pdfACompatibility != DevExpress.XtraPrinting.PdfACompatibility.None)
            {
                if (!string.IsNullOrEmpty(this.neverEmbeddedFonts))
                {
                    list.Add("All fonts should be embedded into a PDF/A document.");
                }
                if (this.showPrintDialogOnOpen)
                {
                    list.Add("PDF/A standard implicitly forbids showing the print dialog on opening a document.");
                }
                if ((this.passwordSecurityOptions != null) && (!string.IsNullOrEmpty(this.passwordSecurityOptions.OpenPassword) || !string.IsNullOrEmpty(this.passwordSecurityOptions.PermissionsPassword)))
                {
                    list.Add("Encryption is not supported in a PDF/A document.");
                }
            }
            if ((pdfACompatibility == DevExpress.XtraPrinting.PdfACompatibility.PdfA1b) && this.exportEditingFieldsToAcroForms)
            {
                list.Add("Export editing fields to AcroForms is not supported in a PDF/A-1b document.");
            }
            return list;
        }

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase =>
            DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override bool RasterizeImages
        {
            get => 
                true;
            set
            {
            }
        }

        [Description("Specifies whether to convert the images contained in the document to JPEG format during the export to PDF."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.ConvertImagesToJpeg"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool ConvertImagesToJpeg
        {
            get => 
                this.convertImagesToJpeg;
            set => 
                this.convertImagesToJpeg = value;
        }

        [Description("Specifies whether to convert a report's editing fields to AcroForms on PDF export."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.ExportEditingFieldsToAcroForms"), DefaultValue(false), TypeConverter(typeof(ExportEditingFieldsToAcroFormsConverter)), XtraSerializableProperty]
        public bool ExportEditingFieldsToAcroForms
        {
            get => 
                this.exportEditingFieldsToAcroForms;
            set => 
                this.exportEditingFieldsToAcroForms = value;
        }

        [Description("Gets or sets whether to display the Print dialog when a PDF file is opened."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.ShowPrintDialogOnOpen"), DefaultValue(false), TypeConverter(typeof(PdfShowPrintDialogOnOpenConverter)), XtraSerializableProperty]
        public bool ShowPrintDialogOnOpen
        {
            get => 
                this.showPrintDialogOnOpen;
            set => 
                this.showPrintDialogOnOpen = value;
        }

        [Description("Gets the options to be embedded as Document Properties of the created PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.DocumentOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PdfDocumentOptions DocumentOptions =>
            this.documentOptions;

        [Description("Provides access to the PDF security options of the document, which require specifying a password."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.PasswordSecurityOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content), RefreshProperties(RefreshProperties.Repaint)]
        public PdfPasswordSecurityOptions PasswordSecurityOptions =>
            this.passwordSecurityOptions;

        [Description(""), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.Compressed"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public bool Compressed
        {
            get => 
                this.compressed;
            set => 
                this.compressed = value;
        }

        [Description("Gets or sets a semicolon-delimited string of values with the font names  which should not be embedded in the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.NeverEmbeddedFonts"), DefaultValue(""), XtraSerializableProperty, TypeConverter(typeof(PdfNeverEmbeddedFontsConverter))]
        public string NeverEmbeddedFonts
        {
            get => 
                this.neverEmbeddedFonts;
            set => 
                this.neverEmbeddedFonts = value;
        }

        [Description("Gets or sets the quality of images in the resulting PDF file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.ImageQuality"), DefaultValue(100), XtraSerializableProperty]
        public PdfJpegImageQuality ImageQuality
        {
            get => 
                this.imageQuality;
            set => 
                this.imageQuality = value;
        }

        [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.SignatureOptions"), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PdfSignatureOptions SignatureOptions =>
            this.signatureOptions;

        [Obsolete("This property is now obsolete. Use the PdfACompatibility property instead."), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool PdfACompatible
        {
            get => 
                this.pdfACompatibility != DevExpress.XtraPrinting.PdfACompatibility.None;
            set => 
                this.pdfACompatibility = value ? DevExpress.XtraPrinting.PdfACompatibility.PdfA3b : DevExpress.XtraPrinting.PdfACompatibility.None;
        }

        [Description("Specifies document compatibility with the PDF/A specification."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.PdfACompatibility"), DefaultValue(0), XtraSerializableProperty, RefreshProperties(RefreshProperties.All)]
        public DevExpress.XtraPrinting.PdfACompatibility PdfACompatibility
        {
            get => 
                this.pdfACompatibility;
            set => 
                this.pdfACompatibility = value;
        }

        [Description("Specifies the additional metadata that is added to the PDF document's metadata."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.AdditionalMetadata"), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string AdditionalMetadata
        {
            get => 
                this.additionalMetadata;
            set => 
                this.additionalMetadata = value;
        }

        [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfExportOptions.Attachments"), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public ICollection<PdfAttachment> Attachments =>
            this.attachments;
    }
}

