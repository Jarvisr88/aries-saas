namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class ContinuousPageRowBuilder : PageRowBuilder
    {
        public ContinuousPageRowBuilder(YPageContentEngine pageContentEngine) : base(pageContentEngine)
        {
        }

        protected override bool AreFriendsTogether(DocumentBand docBand, RectangleF bounds) => 
            true;

        protected override bool AreFriendsTogetherRecursive(DocumentBand docBand, RectangleF bounds) => 
            true;

        protected override FillPageResult BuildNOrderMultiColumn(DocumentBand rootBand, RectangleF bounds, MultiColumn mc) => 
            base.FillPageForBands(rootBand, bounds, new PageRowBuilderBase.ProcessBandsDelegate(this.CanProcessDetail));

        protected override bool CanFillPageWithBricks(DocumentBand docBand) => 
            ((base.ProcessState == ProcessState.ReportDetails) || (base.ProcessState == ProcessState.ReportFooter)) ? (!docBand.IsKindOf(DocumentBandKind.PageBand) && base.CanFillPageWithBricks(docBand)) : base.CanFillPageWithBricks(docBand);

        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder() => 
            new ContinuousPageRowBuilder(base.PageContentEngine);

        protected PointD FillPageFooterCore(DocumentBand rootBand, RectangleF bounds)
        {
            DocumentBand pageBand = rootBand.GetPageBand(DocumentBandKind.Footer);
            if ((pageBand == null) || !pageBand.IsKindOf(DocumentBandKind.PageBand))
            {
                return base.Offset;
            }
            pageBand = pageBand.CopyBand(this.GetFooterRowIndex(rootBand));
            if ((pageBand.Bricks.Count == 0) && (pageBand.Bands.Count == 0))
            {
                return base.Offset;
            }
            PageRowBuilder builder1 = new PageRowBuilder(base.PageContentEngine);
            builder1.OffsetX = base.OffsetX;
            builder1.OffsetY = base.OffsetY;
            builder1.CanApplyPageBreaks = false;
            PageRowBuilder builder = builder1;
            if (pageBand.Bricks.Count > 0)
            {
                builder.FillPageWithBricks(rootBand, pageBand, bounds);
            }
            else
            {
                builder.FillPage(pageBand, bounds);
            }
            return builder.Offset;
        }

        protected PointD FillPageHeaderCore(DocumentBand rootBand, RectangleF bounds)
        {
            DocumentBand pageHeaderBand = this.GetPageHeaderBand(rootBand);
            if ((pageHeaderBand == null) || !pageHeaderBand.IsKindOf(DocumentBandKind.PageBand))
            {
                return base.Offset;
            }
            pageHeaderBand = pageHeaderBand.CopyBand(Math.Max(pageHeaderBand.RowIndex, this.GetHeaderRowIndex(rootBand)));
            if ((pageHeaderBand.Bricks.Count == 0) && (pageHeaderBand.Bands.Count == 0))
            {
                return base.Offset;
            }
            PageRowBuilder builder1 = new PageRowBuilder(base.PageContentEngine);
            builder1.OffsetX = base.OffsetX;
            builder1.OffsetY = base.OffsetY;
            builder1.CanApplyPageBreaks = false;
            builder1.ForceSplit = true;
            PageRowBuilder builder = builder1;
            if (pageHeaderBand.Bricks.Count > 0)
            {
                builder.FillPageWithBricks(rootBand, pageHeaderBand, bounds);
            }
            else
            {
                builder.OffsetY += pageHeaderBand.TopSpan;
                builder.FillPage(pageHeaderBand, bounds);
            }
            base.RenderHistory.PageHeaderRendered = true;
            return builder.Offset;
        }

        protected override FillPageResult FillReportDetailsAndFooter(DocumentBand rootBand, RectangleF bounds)
        {
            PointD td = this.FillPageHeaderCore(rootBand, bounds);
            base.OffsetY = td.Y;
            FillPageResult fillPageResult = base.FillReportDetails(rootBand, bounds);
            if (!fillPageResult.IsComplete())
            {
                fillPageResult = base.FillReportFooter(rootBand, bounds);
            }
            td = this.FillPageFooterCore(rootBand, bounds);
            base.OffsetY = td.Y;
            return fillPageResult;
        }

        private int GetFooterRowIndex(DocumentBand rootBand)
        {
            int lastDetailRowIndex = base.RenderHistory.GetLastDetailRowIndex(rootBand);
            DocumentBand pageHeaderBand = this.GetPageHeaderBand(rootBand);
            if (pageHeaderBand != null)
            {
                lastDetailRowIndex = Math.Max(lastDetailRowIndex, pageHeaderBand.RowIndex);
            }
            return lastDetailRowIndex;
        }

        private int GetHeaderRowIndex(DocumentBand rootBand)
        {
            DocumentBand detailContainer = base.BuildInfoContainer.GetDetailContainer(rootBand, this, RectangleF.Empty);
            DocumentBand printingDetail = base.BuildInfoContainer.GetPrintingDetail(detailContainer);
            return ((printingDetail != null) ? printingDetail.RowIndex : base.RenderHistory.GetLastDetailRowIndex((detailContainer != null) ? detailContainer : rootBand));
        }
    }
}

