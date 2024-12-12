namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Drawing;

    public class PageHeaderFooterRowBuilderBase : PageRowBuilder
    {
        public PageHeaderFooterRowBuilderBase(YPageContentEngine pageContentEngine);
        private bool CanFillFooterBand(DocumentBand footerBand);
        private bool CanFillHeaderBand(DocumentBand rootBand);
        protected override bool CanFillPageWithBricks(DocumentBand docBand);
        protected override bool CanProcessDetail(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        private bool CanSubtractFooterBandHeight(DocumentBand footerBand);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        private void FillPageFooter(DocumentBand rootBand, RectangleF bounds, float pos);
        private RectangleF FillPageHeader(DocumentBand rootBand, RectangleF bounds);
        protected override FillPageResult FillReportDetailsAndFooter(DocumentBand rootBand, RectangleF bounds);
        protected override RectangleF GetCorrectedBounds(DocumentBand rootBand, RectangleF bounds);
        protected virtual float GetDocBandHeight(DocumentBand docBand, RectangleF bounds);
        protected virtual int GetFooterRowIndex(DocumentBand rootBand);
        protected virtual int GetHeaderRowIndex(DocumentBand rootBand);
        private RectangleF GetPageFooterBounds(DocumentBand docBand, RectangleF bounds, float initialPos);
        private void HidePageHeaderBrick(Brick brick);
        private static bool IsNextPageBreak(DocumentBand footerBand);
        private bool PrintOnPages(DocumentBand docBand, DevExpress.XtraReports.UI.PrintOnPages printOnPages);
        protected override RectangleF ValidateBounds(DocumentBand rootBand, RectangleF bounds, RectangleF newBounds);
    }
}

