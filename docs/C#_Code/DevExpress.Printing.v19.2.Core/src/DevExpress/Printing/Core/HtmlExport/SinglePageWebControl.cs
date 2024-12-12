namespace DevExpress.Printing.Core.HtmlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class SinglePageWebControl : PSWebControlBase
    {
        private readonly int pageIndex;

        public SinglePageWebControl(Document document, IImageRepository imageRepository, int pageIndex, HtmlExportOptionsBase options) : base(document, imageRepository, options)
        {
            this.pageIndex = pageIndex;
        }

        protected override void AddControlsAfterCreatePages()
        {
            this.Controls.Add(base.styleControl);
            this.Controls.Add(base.scriptControl);
        }

        protected override void AddControlsBeforeCreatePages()
        {
        }

        protected virtual HtmlPageLayoutBuilder CreateHtmlPageLayoutBuilder(Page page, HtmlExportContext htmlExportContext) => 
            new HtmlPageLayoutBuilder(page, htmlExportContext);

        protected override void CreatePages()
        {
            using (HtmlPageLayoutBuilder builder = this.CreateHtmlPageLayoutBuilder(base.document.Pages[this.pageIndex], base.htmlExportContext))
            {
                DXHtmlDivision control = new DXHtmlDivision();
                DXHtmlTable child = base.CreateHtmlLayoutTable(builder, base.options.TableLayout) ?? new DXHtmlTable();
                PSPage page = base.document.Pages[this.pageIndex] as PSPage;
                if (PageHasWatermark(base.document, this.pageIndex))
                {
                    int topOffset = !this.NeedClipMargins ? 0 : Convert.ToInt32(GraphicsUnitConverter.Convert((float) (page.MarginsF.Top - page.GetTopMarginOffset()), (float) 300f, (float) 96f));
                    DXHtmlDivision division2 = CreateWatermark(base.document.Pages[this.pageIndex], base.htmlExportContext, topOffset, Convert.ToInt32(GraphicsUnitConverter.Convert(this.NeedClipMargins ? page.GetClippedPageHeight() : base.document.Pages[this.pageIndex].PageSize.Height, (float) 300f, (float) 96f)), this.NeedClipMargins);
                    control.Style["position"] = "relative";
                    if (division2 != null)
                    {
                        control.Controls.Add(division2);
                        division2.CssClass = "page-watermark-container";
                    }
                }
                control.CssClass = "page-background-color-holder";
                control.Style.Add("background-color", HtmlConvert.ToHtml(base.htmlExportContext.PrintingSystem.Graph.PageBackColor));
                if (base.RightToLeftLayout)
                {
                    control.Style.Add("direction", "rtl");
                }
                if (child != null)
                {
                    control.Controls.Add(child);
                }
                base.AddChildrenControl(control);
            }
        }

        protected virtual bool NeedClipMargins =>
            false;
    }
}

