namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class XpsExportOptions : PageByPageExportOptionsBase
    {
        private XpsCompressionOption compression;
        private XpsDocumentOptions documentOptions = new XpsDocumentOptions();

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            this.Compression = ((XpsExportOptions) source).Compression;
            XpsExportOptions options = (XpsExportOptions) source;
            this.documentOptions.Assign(options.documentOptions);
        }

        protected internal override ExportOptionsBase CloneOptions()
        {
            XpsExportOptions options = new XpsExportOptions();
            options.Assign(this);
            return options;
        }

        protected internal override bool ShouldSerialize() => 
            (this.compression != XpsCompressionOption.Normal) || (this.ShouldSerializeDocumentOptions() || base.ShouldSerialize());

        private bool ShouldSerializeDocumentOptions() => 
            this.documentOptions.ShouldSerialize();

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase =>
            DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

        [Description("Gets or sets a value specifying the compression level of the XPS document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsExportOptions.Compression"), DefaultValue(0), XtraSerializableProperty]
        public XpsCompressionOption Compression
        {
            get => 
                this.compression;
            set => 
                this.compression = value;
        }

        [Description("Gets the options to be embedded as Document Properties of the created XPS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XpsExportOptions.DocumentOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public XpsDocumentOptions DocumentOptions
        {
            get => 
                this.documentOptions;
            set => 
                this.documentOptions = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override bool RasterizeImages
        {
            get => 
                true;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public override int RasterizationResolution
        {
            get => 
                0x60;
            set
            {
            }
        }
    }
}

