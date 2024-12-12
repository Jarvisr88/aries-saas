namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class PageHeaderFooterRowBuilder : PageHeaderFooterRowBuilderBase
    {
        public PageHeaderFooterRowBuilder(YPageContentEngine pageContentEngine) : base(pageContentEngine)
        {
        }

        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder() => 
            new PageHeaderFooterRowBuilder(base.PageContentEngine);

        protected override float GetDocBandHeight(DocumentBand docBand, RectangleF bounds)
        {
            object[] keyParts = new object[] { docBand.ID };
            return base.PageContentEngine.GetBandHeight(new MultiKey(keyParts), () => BuildHelper.GetDocBandHeight(docBand, bounds, this.UsefulPageWidth, true));
        }

        protected override int GetFooterRowIndex(DocumentBand rootBand)
        {
            int lastDetailRowIndex = base.RenderHistory.GetLastDetailRowIndex(rootBand);
            DocumentBand pageHeaderBand = this.GetPageHeaderBand(rootBand);
            if (pageHeaderBand != null)
            {
                lastDetailRowIndex = Math.Max(lastDetailRowIndex, pageHeaderBand.RowIndex);
            }
            return lastDetailRowIndex;
        }

        protected override int GetHeaderRowIndex(DocumentBand rootBand)
        {
            DocumentBand detailContainer = base.BuildInfoContainer.GetDetailContainer(rootBand, this, RectangleF.Empty);
            DocumentBand printingDetail = base.BuildInfoContainer.GetPrintingDetail(detailContainer);
            return ((printingDetail != null) ? printingDetail.RowIndex : base.RenderHistory.GetLastDetailRowIndex((detailContainer != null) ? detailContainer : rootBand));
        }
    }
}

