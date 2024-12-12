namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Printing.Native;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [Serializable]
    public abstract class PageByPageExportOptionsBase : ExportOptionsBase
    {
        protected internal const int DefaultRasterizationResolution = 0x60;
        private const bool DefaultRasterizeImages = true;
        private int rasterizationResolution;
        private bool rasterizeImages;
        [NonSerialized]
        private string pageRange;

        protected PageByPageExportOptionsBase()
        {
            this.rasterizationResolution = 0x60;
            this.rasterizeImages = true;
            this.pageRange = string.Empty;
        }

        protected PageByPageExportOptionsBase(PageByPageExportOptionsBase source) : base(source)
        {
            this.rasterizationResolution = 0x60;
            this.rasterizeImages = true;
            this.pageRange = string.Empty;
        }

        public override void Assign(ExportOptionsBase source)
        {
            PageByPageExportOptionsBase base2 = (PageByPageExportOptionsBase) source;
            this.pageRange = base2.PageRange;
            this.rasterizeImages = base2.RasterizeImages;
            this.rasterizationResolution = base2.RasterizationResolution;
        }

        internal int[] GetPageIndices(int pageCount) => 
            PageRangeParser.GetIndices(this.pageRange, pageCount);

        protected internal override bool ShouldSerialize() => 
            (this.pageRange != string.Empty) || (!this.rasterizeImages || (this.rasterizationResolution != 0x60));

        [Description("Gets or sets the page range to be exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PageByPageExportOptionsBase.PageRange"), DefaultValue(""), XtraSerializableProperty]
        public virtual string PageRange
        {
            get => 
                this.pageRange;
            set => 
                this.pageRange = PageRangeParser.ValidateString(value);
        }

        [Description("Specifies whether or not vector images should be rasterized on export to the corresponding document format."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PageByPageExportOptionsBase.RasterizeImages"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public virtual bool RasterizeImages
        {
            get => 
                this.rasterizeImages;
            set => 
                this.rasterizeImages = value;
        }

        [Description("Specifies the resolution (in DPI) used to rasterize vector images on export to the corresponding document format."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PageByPageExportOptionsBase.RasterizationResolution"), DefaultValue(0x60), TypeConverter(typeof(RasterizationResolutionConverter)), XtraSerializableProperty]
        public virtual int RasterizationResolution
        {
            get => 
                this.rasterizationResolution;
            set => 
                this.rasterizationResolution = value;
        }
    }
}

