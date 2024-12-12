namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsxExportOptions")]
    public class XlsxExportOptions : XlExportOptionsBase
    {
        private const XlsxExportMode DefaultExportMode = XlsxExportMode.SingleFile;
        private XlsxExportMode exportMode;

        public XlsxExportOptions() : this(TextExportMode.Value)
        {
        }

        public XlsxExportOptions(TextExportMode textExportMode) : base(textExportMode)
        {
        }

        private XlsxExportOptions(XlsxExportOptions source) : base(source)
        {
        }

        public XlsxExportOptions(TextExportMode textExportMode, bool showGridLines) : base(textExportMode, showGridLines)
        {
        }

        public XlsxExportOptions(TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks) : base(textExportMode, showGridLines, exportHyperlinks)
        {
        }

        public XlsxExportOptions(TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks, bool fitToPrintedPageWidth) : base(textExportMode, showGridLines, exportHyperlinks, fitToPrintedPageWidth)
        {
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            XlsxExportOptions options = (XlsxExportOptions) source;
            this.exportMode = options.ExportMode;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new XlsxExportOptions(this);

        protected internal override bool ShouldSerialize() => 
            (this.exportMode != XlsxExportMode.SingleFile) || base.ShouldSerialize();

        [Description("Specifies whether the source is exported as a single XLSX file or multiple files, and whether each page is exported as a separate worksheet."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsxExportOptions.ExportMode"), DefaultValue(0), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public XlsxExportMode ExportMode
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
                switch (this.ExportMode)
                {
                    case XlsxExportMode.SingleFile:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFile;

                    case XlsxExportMode.SingleFilePageByPage:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

                    case XlsxExportMode.DifferentFiles:
                        return DevExpress.XtraPrinting.ExportModeBase.DifferentFiles;
                }
                throw new NotSupportedException();
            }
        }

        [Description("Gets or sets the range of pages to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsxExportOptions.PageRange"), TypeConverter(typeof(XlsxPageRangeConverter)), XtraSerializableProperty]
        public override string PageRange
        {
            get => 
                base.PageRange;
            set => 
                base.PageRange = value;
        }
    }
}

