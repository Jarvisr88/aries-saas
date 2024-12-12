namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions")]
    public class XlsExportOptions : XlExportOptionsBase
    {
        private const XlsExportMode DefaultExportMode = XlsExportMode.SingleFile;
        private const DevExpress.XtraPrinting.WorkbookColorPaletteCompliance DefaultWorkbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        private bool suppress256ColumnsWarning;
        private bool suppress65536RowsWarning;
        private XlsExportMode exportMode;
        private DevExpress.XtraPrinting.WorkbookColorPaletteCompliance workbookColorPaletteCompliance;

        public XlsExportOptions() : this(TextExportMode.Value)
        {
        }

        public XlsExportOptions(TextExportMode textExportMode) : base(textExportMode)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        }

        private XlsExportOptions(XlsExportOptions source) : base(source)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        }

        public XlsExportOptions(TextExportMode textExportMode, bool showGridLines) : base(textExportMode, showGridLines)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        }

        public XlsExportOptions(TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks) : base(textExportMode, showGridLines, exportHyperlinks)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        }

        public XlsExportOptions(TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks, bool fitToPrintedPageWidth) : base(textExportMode, showGridLines, exportHyperlinks, fitToPrintedPageWidth)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
        }

        public XlsExportOptions(TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks, bool suppress256ColumnsWarning, bool suppress65536RowsWarning, DevExpress.XtraPrinting.WorkbookColorPaletteCompliance workbookColorPaletteCompliance) : base(textExportMode, showGridLines, exportHyperlinks)
        {
            this.workbookColorPaletteCompliance = DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
            this.suppress256ColumnsWarning = suppress256ColumnsWarning;
            this.suppress65536RowsWarning = suppress65536RowsWarning;
            this.workbookColorPaletteCompliance = workbookColorPaletteCompliance;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            XlsExportOptions options = (XlsExportOptions) source;
            this.suppress256ColumnsWarning = options.Suppress256ColumnsWarning;
            this.suppress65536RowsWarning = options.Suppress65536RowsWarning;
            this.workbookColorPaletteCompliance = options.workbookColorPaletteCompliance;
            this.exportMode = options.ExportMode;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new XlsExportOptions(this);

        protected internal override bool ShouldSerialize() => 
            this.suppress256ColumnsWarning || (this.suppress65536RowsWarning || ((this.exportMode != XlsExportMode.SingleFile) || ((this.workbookColorPaletteCompliance != DevExpress.XtraPrinting.WorkbookColorPaletteCompliance.ReducePaletteForExactColors) || base.ShouldSerialize())));

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase
        {
            get
            {
                switch (this.ExportMode)
                {
                    case XlsExportMode.SingleFile:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFile;

                    case XlsExportMode.SingleFilePageByPage:
                        return DevExpress.XtraPrinting.ExportModeBase.SingleFilePageByPage;

                    case XlsExportMode.DifferentFiles:
                        return DevExpress.XtraPrinting.ExportModeBase.DifferentFiles;
                }
                throw new NotSupportedException();
            }
        }

        [Description("Specifies the color palette compatibility mode with different workbooks versions."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.WorkbookColorPaletteCompliance"), DefaultValue(1), XtraSerializableProperty]
        public DevExpress.XtraPrinting.WorkbookColorPaletteCompliance WorkbookColorPaletteCompliance
        {
            get => 
                this.workbookColorPaletteCompliance;
            set => 
                this.workbookColorPaletteCompliance = value;
        }

        [Description("Gets or sets a value indicating whether to suppress the exception that raises when trying to export a document to an XLS file with more than 256 columns."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.Suppress256ColumnsWarning"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool Suppress256ColumnsWarning
        {
            get => 
                this.suppress256ColumnsWarning;
            set => 
                this.suppress256ColumnsWarning = value;
        }

        [Description("Gets or sets a value indicating whether to suppress the exception that raises when trying to export a document to an XLS file with more than 65,536 rows."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.Suppress65536RowsWarning"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool Suppress65536RowsWarning
        {
            get => 
                this.suppress65536RowsWarning;
            set => 
                this.suppress65536RowsWarning = value;
        }

        [Description("Specifies whether the document should be exported to a single or different XLS files, each page in a separate file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.ExportMode"), DefaultValue(0), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public XlsExportMode ExportMode
        {
            get => 
                this.exportMode;
            set => 
                this.exportMode = value;
        }

        [Description("Gets or sets the range of pages to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.PageRange"), TypeConverter(typeof(XlsPageRangeConverter)), XtraSerializableProperty]
        public override string PageRange
        {
            get => 
                base.PageRange;
            set => 
                base.PageRange = value;
        }
    }
}

