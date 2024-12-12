namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.ExportOptions"), SerializationContext(typeof(PrintingSystemSerializationContext))]
    public class ExportOptions : IXtraSupportShouldSerialize
    {
        private List<ExportOptionKind> hiddenOptions = new List<ExportOptionKind>();
        protected Dictionary<Type, ExportOptionsBase> options = new Dictionary<Type, ExportOptionsBase>();
        private EmailOptions emailOptions = new EmailOptions();
        private PrintPreviewOptions printPreviewOptions = new PrintPreviewOptions();
        private PrintingSystemXmlSerializer serializer;

        public ExportOptions()
        {
            this.options.Add(typeof(PdfExportOptions), new PdfExportOptions());
            this.options.Add(typeof(XlsExportOptions), new XlsExportOptions());
            this.options.Add(typeof(TextExportOptions), new TextExportOptions());
            this.options.Add(typeof(CsvExportOptions), new CsvExportOptions());
            this.options.Add(typeof(ImageExportOptions), new ImageExportOptions());
            this.options.Add(typeof(HtmlExportOptions), new HtmlExportOptions());
            this.options.Add(typeof(MhtExportOptions), new MhtExportOptions());
            this.options.Add(typeof(RtfExportOptions), new RtfExportOptions());
            this.options.Add(typeof(DocxExportOptions), new DocxExportOptions());
            this.options.Add(typeof(NativeFormatOptions), new NativeFormatOptions());
            this.options.Add(typeof(XlsxExportOptions), new XlsxExportOptions());
            this.options.Add(typeof(MailMessageExportOptions), new MailMessageExportOptions());
        }

        public void Assign(ExportOptions source)
        {
            foreach (Type type in source.options.Keys)
            {
                this.options[type].Assign(source.options[type]);
            }
            this.emailOptions.Assign(source.Email);
            this.printPreviewOptions.Assign(source.PrintPreview);
            foreach (ExportOptionKind kind in typeof(ExportOptionKind).GetValues())
            {
                this.SetOptionVisibility(kind, source.GetOptionVisibility(kind));
            }
        }

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName)
        {
            uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyName);
            return ((num > 0x590ca79a) ? ((num > 0x9be5ff9a) ? ((num > 0xa7747869) ? ((num == 0xce52c9b5) ? ((propertyName == "Pdf") ? this.ShouldSerializePdf() : true) : ((num != 0xe274816c) || ((propertyName != "PrintPreview") || this.ShouldSerializePrintPreview()))) : ((num == 0xa75a4def) ? ((propertyName != "Rtf") || this.ShouldSerializeRtf()) : ((num != 0xa7747869) || ((propertyName != "NativeFormat") || this.ShouldSerializeNativeFormat())))) : ((num == 0x5ad350ff) ? ((propertyName != "Csv") || this.ShouldSerializeCsv()) : ((num == 0x96cad6f7) ? ((propertyName != "Docx") || this.ShouldSerializeDocx()) : ((num != 0x9be5ff9a) || ((propertyName != "Xlsx") || this.ShouldSerializeXlsx()))))) : ((num > 0x32920dac) ? ((num > 0x43352167) ? ((num == 0x57a1f3f0) ? ((propertyName != "Html") || this.ShouldSerializeHtml()) : ((num != 0x590ca79a) || ((propertyName != "Image") || this.ShouldSerializeImage()))) : ((num == 0x3e142d5e) ? ((propertyName != "Text") || this.ShouldSerializeText()) : ((num != 0x43352167) || ((propertyName != "Email") || this.ShouldSerializeEmail())))) : ((num == 0x1967520d) ? ((propertyName != "MailMessage") || this.ShouldSerializeMailMessage()) : ((num == 0x1ffeaa46) ? ((propertyName != "Xls") || this.ShouldSerializeXls()) : ((num != 0x32920dac) || ((propertyName != "Mht") || this.ShouldSerializeMht()))))));
        }

        public bool GetOptionVisibility(ExportOptionKind optionKind) => 
            !this.hiddenOptions.Contains(optionKind);

        private void RestoreCore(XtraSerializer serializer, object path)
        {
            this.Assign(new ExportOptions());
            serializer.DeserializeObject(this, path, "ExportOptions");
        }

        public void RestoreFromRegistry(string path)
        {
            this.RestoreCore(new RegistryXtraSerializer(), path);
        }

        public void RestoreFromStream(Stream stream)
        {
            long position = 0L;
            if (stream.CanSeek)
            {
                position = stream.Position;
            }
            this.RestoreCore(this.Serializer, stream);
            if (stream.CanSeek)
            {
                stream.Position = position;
            }
        }

        public void RestoreFromXml(string xmlFile)
        {
            this.RestoreCore(this.Serializer, xmlFile);
        }

        private void SaveCore(XtraSerializer serializer, object path)
        {
            serializer.SerializeObject(this, path, "ExportOptions");
        }

        public void SaveToRegistry(string path)
        {
            this.SaveCore(new RegistryXtraSerializer(), path);
        }

        public void SaveToStream(Stream stream)
        {
            long position = 0L;
            if (stream.CanSeek)
            {
                position = stream.Position;
            }
            this.SaveCore(this.Serializer, stream);
            if (stream.CanSeek)
            {
                stream.Position = position;
            }
        }

        public void SaveToXml(string xmlFile)
        {
            this.SaveCore(this.Serializer, xmlFile);
        }

        public void SetOptionsVisibility(ExportOptionKind[] optionKinds, bool visible)
        {
            foreach (ExportOptionKind kind in optionKinds)
            {
                this.SetOptionVisibility(kind, visible);
            }
        }

        public void SetOptionVisibility(ExportOptionKind optionKind, bool visible)
        {
            if (visible && !this.GetOptionVisibility(optionKind))
            {
                this.hiddenOptions.Remove(optionKind);
            }
            if (!visible && this.GetOptionVisibility(optionKind))
            {
                this.hiddenOptions.Add(optionKind);
            }
        }

        internal bool ShouldSerialize() => 
            this.ShouldSerializeCsv() || (this.ShouldSerializeEmail() || (this.ShouldSerializeHtml() || (this.ShouldSerializeImage() || (this.ShouldSerializeMht() || (this.ShouldSerializeNativeFormat() || (this.ShouldSerializePdf() || (this.ShouldSerializePrintPreview() || (this.ShouldSerializeRtf() || (this.ShouldSerializeDocx() || (this.ShouldSerializeText() || (this.ShouldSerializeXls() || (this.ShouldSerializeXlsx() || this.ShouldSerializeMailMessage()))))))))))));

        private bool ShouldSerializeCsv() => 
            this.Csv.ShouldSerialize();

        private bool ShouldSerializeDocx() => 
            this.Docx.ShouldSerialize();

        private bool ShouldSerializeEmail() => 
            this.Email.ShouldSerialize();

        private bool ShouldSerializeHtml() => 
            this.Html.ShouldSerialize();

        private bool ShouldSerializeImage() => 
            this.Image.ShouldSerialize();

        private bool ShouldSerializeMailMessage() => 
            this.MailMessage.ShouldSerialize();

        private bool ShouldSerializeMht() => 
            this.Mht.ShouldSerialize();

        private bool ShouldSerializeNativeFormat() => 
            this.NativeFormat.ShouldSerialize();

        private bool ShouldSerializePdf() => 
            this.Pdf.ShouldSerialize();

        private bool ShouldSerializePrintPreview() => 
            this.PrintPreview.ShouldSerialize();

        private bool ShouldSerializeRtf() => 
            this.Rtf.ShouldSerialize();

        private bool ShouldSerializeText() => 
            this.Text.ShouldSerialize();

        private bool ShouldSerializeXls() => 
            this.Xls.ShouldSerialize();

        private bool ShouldSerializeXlsx() => 
            this.Xlsx.ShouldSerialize();

        internal void UpdateDefaultFileName(string oldValue, string newValue)
        {
            if (oldValue == this.printPreviewOptions.DefaultFileName)
            {
                this.printPreviewOptions.DefaultFileName = newValue;
            }
            if (oldValue == this.Html.Title)
            {
                this.Html.Title = newValue;
            }
            if (oldValue == this.Mht.Title)
            {
                this.Mht.Title = newValue;
            }
            if (oldValue == this.MailMessage.Title)
            {
                this.MailMessage.Title = newValue;
            }
        }

        private PrintingSystemXmlSerializer Serializer
        {
            get
            {
                this.serializer ??= new PrintingSystemXmlSerializer();
                return this.serializer;
            }
        }

        [Description("Gets the settings used to specify exporting parameters when a document is exported to PDF format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PdfExportOptions Pdf =>
            (PdfExportOptions) this.options[typeof(PdfExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to XLS format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public XlsExportOptions Xls =>
            (XlsExportOptions) this.options[typeof(XlsExportOptions)];

        [Description("Gets the settings used to specify export parameters when a document is exported to XLSX format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public XlsxExportOptions Xlsx =>
            (XlsxExportOptions) this.options[typeof(XlsxExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to text."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public TextExportOptions Text =>
            (TextExportOptions) this.options[typeof(TextExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to CSV format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public CsvExportOptions Csv =>
            (CsvExportOptions) this.options[typeof(CsvExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to image."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public ImageExportOptions Image =>
            (ImageExportOptions) this.options[typeof(ImageExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to HTML format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public HtmlExportOptions Html =>
            (HtmlExportOptions) this.options[typeof(HtmlExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to MHT format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public MhtExportOptions Mht =>
            (MhtExportOptions) this.options[typeof(MhtExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to RTF format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public RtfExportOptions Rtf =>
            (RtfExportOptions) this.options[typeof(RtfExportOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported to DOCX format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public DocxExportOptions Docx =>
            (DocxExportOptions) this.options[typeof(DocxExportOptions)];

        [Description("Provides access to the object, which contains settings for saving the PrintingSystem document in native format."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public NativeFormatOptions NativeFormat =>
            (NativeFormatOptions) this.options[typeof(NativeFormatOptions)];

        [Description("Gets the settings used to specify exporting parameters when a document is exported and sent via e-mail from the Print Preview."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public EmailOptions Email =>
            this.emailOptions;

        [Description("Gets the settings used to specify how a document is exported from the Print Preview."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PrintPreviewOptions PrintPreview =>
            this.printPreviewOptions;

        [Description("Provides access to an object that contains options which define how a document is exported to a mail message."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public MailMessageExportOptions MailMessage =>
            (MailMessageExportOptions) this.options[typeof(MailMessageExportOptions)];

        internal Dictionary<Type, ExportOptionsBase> Options =>
            this.options;

        internal List<ExportOptionKind> HiddenOptions =>
            this.hiddenOptions;
    }
}

