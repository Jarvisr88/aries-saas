namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxExportOptions")]
    public class DocxExportOptions : FormattedTextExportOptions, IXtraSupportShouldSerialize
    {
        private const DocxExportMode DefaultExportMode = DocxExportMode.SingleFilePageByPage;
        private const bool DefaultTableLayout = false;
        private const bool DefaultAllowFloatingPictures = false;
        private DocxExportMode exportMode;
        private bool tableLayout;
        private bool allowFloatingPictures;
        private DocxDocumentOptions documentOptions;

        public DocxExportOptions()
        {
            this.exportMode = DocxExportMode.SingleFilePageByPage;
            this.documentOptions = new DocxDocumentOptions();
        }

        protected DocxExportOptions(DocxExportOptions source) : base(source)
        {
            this.exportMode = DocxExportMode.SingleFilePageByPage;
            this.documentOptions = new DocxDocumentOptions();
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            DocxExportOptions options = (DocxExportOptions) source;
            this.exportMode = options.ExportMode;
            this.tableLayout = options.TableLayout;
            this.allowFloatingPictures = options.AllowFloatingPictures;
            this.documentOptions = options.DocumentOptions;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new DocxExportOptions(this);

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName != "DocumentOptions") || this.ShouldSerializeDocumentOptions();

        protected internal override bool ShouldSerialize() => 
            (this.exportMode != DocxExportMode.SingleFilePageByPage) || (this.tableLayout || (this.allowFloatingPictures || (this.ShouldSerializeDocumentOptions() || base.ShouldSerialize())));

        private bool ShouldSerializeDocumentOptions() => 
            this.DocumentOptions.ShouldSerialize();

        [Description("Gets or sets a value indicating how a document is exported to DOCX."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxExportOptions.ExportMode"), DefaultValue(1), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public DocxExportMode ExportMode
        {
            get => 
                this.exportMode;
            set => 
                this.exportMode = value;
        }

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase
        {
            get
            {
                DocxExportMode exportMode = this.ExportMode;
                if (exportMode == DocxExportMode.SingleFile)
                {
                    return DevExpress.XtraPrinting.ExportModeBase.SingleFile;
                }
                if (exportMode != DocxExportMode.SingleFilePageByPage)
                {
                    throw new NotSupportedException();
                }
                return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;
            }
        }

        [Description("Specifies whether the height of table cells in a resulting document should have fixed values, or adding a new line of text to a cell's content should increase the row height."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.KeepRowHeight"), DefaultValue(false), TypeConverter(typeof(DocxKeepRowHeightConverter)), XtraSerializableProperty]
        public override bool KeepRowHeight
        {
            get => 
                base.KeepRowHeight;
            set => 
                base.KeepRowHeight = value;
        }

        [Description("Specifies whether to use the table or frame layout in the resulting DOCX file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxExportOptions.TableLayout"), DefaultValue(false), TypeConverter(typeof(DocxTableLayoutConverter)), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public bool TableLayout
        {
            get => 
                this.tableLayout;
            set => 
                this.tableLayout = value;
        }

        [Description("Specifies whether to embed floating pictures into exported DOCX files."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxExportOptions.AllowFloatingPictures"), DefaultValue(false), TypeConverter(typeof(DocxAllowFloatingPicturesConverter)), XtraSerializableProperty]
        public bool AllowFloatingPictures
        {
            get => 
                this.allowFloatingPictures;
            set => 
                this.allowFloatingPictures = value;
        }

        [Description("Provides access to an object, specifying the exported document's options."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.DocxExportOptions.DocumentOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public DocxDocumentOptions DocumentOptions =>
            this.documentOptions;
    }
}

