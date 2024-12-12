namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [TypeConverter("DevExpress.XtraPrinting.Design.HeaderFooterConverter,DevExpress.XtraPrinting.v19.2.Design")]
    public class PageHeaderFooter
    {
        private PageArea pageHeader;
        private PageArea pageFooter;

        public PageHeaderFooter()
        {
        }

        public PageHeaderFooter(PageHeaderArea pageHeader, PageFooterArea pageFooter)
        {
            this.pageHeader = pageHeader;
            this.pageFooter = pageFooter;
        }

        internal void CreateMarginalFooter(BrickGraphics graph, Image[] images)
        {
            if (this.pageFooter != null)
            {
                this.pageFooter.CreateArea(graph, images);
            }
        }

        internal void CreateMarginalHeader(BrickGraphics graph, Image[] images)
        {
            if (this.pageHeader != null)
            {
                this.pageHeader.CreateArea(graph, images);
            }
        }

        internal SizeF MeasureMarginalFooter(BrickGraphics graph, Image[] images) => 
            (this.pageFooter != null) ? this.pageFooter.MeasureArea(graph, images) : SizeF.Empty;

        internal SizeF MeasureMarginalHeader(BrickGraphics graph, Image[] images) => 
            (this.pageHeader != null) ? this.pageHeader.MeasureArea(graph, images) : SizeF.Empty;

        internal bool ShouldSerialize() => 
            this.ShouldSerialize(this.pageHeader) || this.ShouldSerialize(this.pageFooter);

        protected bool ShouldSerialize(PageArea pageArea) => 
            (pageArea != null) && pageArea.ShouldSerialize();

        public override string ToString() => 
            "";

        public bool IncreaseMarginsByContent { get; set; }

        [Description("Provides access to the page header area."), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PageArea Header
        {
            get
            {
                this.pageHeader ??= new PageHeaderArea();
                return this.pageHeader;
            }
        }

        [Description("Provides access to the page footer area."), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public PageArea Footer
        {
            get
            {
                this.pageFooter ??= new PageFooterArea();
                return this.pageFooter;
            }
        }
    }
}

