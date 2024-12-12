namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.RtfExportOptions")]
    public class RtfExportOptions : FormattedTextExportOptions
    {
        private const RtfExportMode DefaultExportMode = RtfExportMode.SingleFilePageByPage;
        private RtfExportMode exportMode;
        private bool exportToClipboard;

        public RtfExportOptions()
        {
            this.exportMode = RtfExportMode.SingleFilePageByPage;
        }

        protected RtfExportOptions(RtfExportOptions source) : base(source)
        {
            this.exportMode = RtfExportMode.SingleFilePageByPage;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            RtfExportOptions options = (RtfExportOptions) source;
            this.exportMode = options.ExportMode;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new RtfExportOptions(this);

        protected internal override bool ShouldSerialize() => 
            (this.exportMode != RtfExportMode.SingleFilePageByPage) || base.ShouldSerialize();

        [Description("Gets or sets a value indicating how a document is exported to RTF."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.RtfExportOptions.ExportMode"), DefaultValue(1), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public RtfExportMode ExportMode
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
                RtfExportMode exportMode = this.ExportMode;
                if (exportMode == RtfExportMode.SingleFile)
                {
                    return DevExpress.XtraPrinting.ExportModeBase.SingleFile;
                }
                if (exportMode != RtfExportMode.SingleFilePageByPage)
                {
                    throw new NotSupportedException();
                }
                return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;
            }
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

        internal bool ExportToClipboard
        {
            get => 
                this.exportToClipboard;
            set => 
                this.exportToClipboard = value;
        }
    }
}

