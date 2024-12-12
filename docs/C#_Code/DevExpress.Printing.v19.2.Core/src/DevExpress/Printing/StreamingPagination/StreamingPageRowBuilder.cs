namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class StreamingPageRowBuilder : ContinuousPageRowBuilder
    {
        private bool headerProcessed;

        public StreamingPageRowBuilder(YPageContentEngine pageContentEngine) : base(pageContentEngine)
        {
        }

        private bool BandInProgress(DocumentBand band)
        {
            int builtBandIndex = this.ExternalBuildEngine.GetBuiltBandIndex(band.Parent);
            if ((builtBandIndex < 0) || (builtBandIndex >= band.Parent.Bands.Count))
            {
                return false;
            }
            DocumentBand objB = band.Parent.Bands[builtBandIndex];
            return ReferenceEquals(band, objB);
        }

        protected override bool CanProcessDetail(DocumentBand rootBand, PageBuildInfo pageBuildInfo)
        {
            DocumentBand band = this.GetBand(rootBand, pageBuildInfo.Index);
            if (band == null)
            {
                return false;
            }
            DocumentBandKind[] kinds = new DocumentBandKind[] { DocumentBandKind.TopMargin, DocumentBandKind.BottomMargin, DocumentBandKind.ReportHeader, DocumentBandKind.ReportFooter };
            return !band.IsKindOf(kinds);
        }

        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder()
        {
            StreamingPageRowBuilder builder1 = new StreamingPageRowBuilder(base.PageContentEngine);
            builder1.CanApplyPageBreaks = false;
            builder1.ExternalBuildEngine = this.ExternalBuildEngine;
            return builder1;
        }

        public void FillPageFooter(DocumentBand rootBand, RectangleF bounds)
        {
            PointD td = base.FillPageFooterCore(rootBand, bounds);
            base.OffsetY = td.Y;
        }

        public override FillPageResult FillPageForBand(DocumentBand rootBand, RectangleF bounds, RectangleF newBounds) => 
            (((this.ExternalBuildEngine == null) || this.ExternalBuildEngine.Stopped) || ((base.BuildInfoContainer.GetBuildInfo(rootBand) != this.ExternalBuildEngine.GetBuiltBandIndex(rootBand)) || !this.BandInProgress(rootBand))) ? base.FillPageForBand(rootBand, bounds, newBounds) : FillPageResult.OverFulfill;

        protected override FillPageResult FillReportDetailsAndFooter(DocumentBand rootBand, RectangleF bounds)
        {
            if (!this.headerProcessed)
            {
                PointD td = base.FillPageHeaderCore(rootBand, bounds);
                base.OffsetY = td.Y;
                this.headerProcessed = true;
            }
            FillPageResult fillPageResult = base.FillReportDetails(rootBand, bounds);
            if (!fillPageResult.IsComplete())
            {
                fillPageResult = base.FillReportFooter(rootBand, bounds);
            }
            return fillPageResult;
        }

        private DocumentBand GetBand(DocumentBand rootBand, int index)
        {
            if ((this.ExternalBuildEngine != null) && !this.ExternalBuildEngine.Stopped)
            {
                int builtBandIndex = this.ExternalBuildEngine.GetBuiltBandIndex(rootBand);
                if (index > builtBandIndex)
                {
                    return null;
                }
            }
            return ((index < rootBand.Bands.Count) ? rootBand.Bands[index] : null);
        }

        public IPageBuildEngine ExternalBuildEngine { get; set; }
    }
}

