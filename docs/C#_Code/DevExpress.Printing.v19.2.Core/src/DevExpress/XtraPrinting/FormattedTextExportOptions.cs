namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions")]
    public abstract class FormattedTextExportOptions : PageByPageExportOptionsBase
    {
        private const bool DefaultExportWatermarks = true;
        private const bool DefaultExportPageBreaks = true;
        private const bool DefaultEmptyFirstPageHeaderFooter = false;
        protected const bool DefaultKeepRowHeight = false;
        private bool exportWatermarks;
        private bool exportPageBreaks;
        private bool emptyFirstPageHeaderFooter;
        private bool keepRowHeight;

        public FormattedTextExportOptions()
        {
            this.exportWatermarks = true;
            this.exportPageBreaks = true;
        }

        protected FormattedTextExportOptions(FormattedTextExportOptions source) : base(source)
        {
            this.exportWatermarks = true;
            this.exportPageBreaks = true;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            FormattedTextExportOptions options = (FormattedTextExportOptions) source;
            this.exportWatermarks = options.ExportWatermarks;
            this.exportPageBreaks = options.ExportPageBreaks;
            this.emptyFirstPageHeaderFooter = options.EmptyFirstPageHeaderFooter;
            this.keepRowHeight = options.KeepRowHeight;
        }

        protected internal override bool ShouldSerialize() => 
            !this.exportWatermarks || (this.keepRowHeight || (!this.exportPageBreaks || (this.emptyFirstPageHeaderFooter || base.ShouldSerialize())));

        [Description("Specifies whether or not watermarks should be included in the resulting file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.ExportWatermarks"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool ExportWatermarks
        {
            get => 
                this.exportWatermarks;
            set => 
                this.exportWatermarks = value;
        }

        [Description("Specifies whether or not page breaks should be included in the resulting file when a document is exported to RTF/DOCX."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.ExportPageBreaks"), DefaultValue(true), TypeConverter(typeof(FormattedTextSingleFileOptionConverter)), XtraSerializableProperty]
        public virtual bool ExportPageBreaks
        {
            get => 
                this.exportPageBreaks;
            set => 
                this.exportPageBreaks = value;
        }

        [Description("Specifies whether or not the header and footer contents should be displayed on the first page of the final document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.EmptyFirstPageHeaderFooter"), DefaultValue(false), TypeConverter(typeof(FormattedTextSingleFileOptionConverter)), XtraSerializableProperty]
        public virtual bool EmptyFirstPageHeaderFooter
        {
            get => 
                this.emptyFirstPageHeaderFooter;
            set => 
                this.emptyFirstPageHeaderFooter = value;
        }

        [Description("Specifies whether the height of table cells in a resulting document should have fixed values, or adding a new line of text to a cell's content should increase the row height."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.KeepRowHeight"), DefaultValue(false), TypeConverter(typeof(FormattedTextSingleFileOptionConverter)), XtraSerializableProperty]
        public virtual bool KeepRowHeight
        {
            get => 
                this.keepRowHeight;
            set => 
                this.keepRowHeight = value;
        }

        [Description("Specifies the range of pages to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.FormattedTextExportOptions.PageRange"), TypeConverter(typeof(FormattedTextPageRangeConverter)), XtraSerializableProperty]
        public override string PageRange
        {
            get => 
                base.PageRange;
            set => 
                base.PageRange = value;
        }
    }
}

