namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class PageRowBuilder : PageRowBuilderBase
    {
        private int markedBandID;
        private YPageContentEngine pageContentEngine;

        public PageRowBuilder(YPageContentEngine pageContentEngine);
        protected override void AfterDocumentBandFill(DocumentBand docBand);
        protected override bool AreFriendsTogether(DocumentBand docBand, RectangleF bounds);
        protected override bool AreFriendsTogetherRecursive(DocumentBand docBand, RectangleF bounds);
        public void BeforeFillPage(YPageContentEngine pageContentEngine);
        protected override FillPageResult BuildNOrderMultiColumn(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected override FillPageResult BuildZOrderMultiColumnInternal(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected override bool CanApplyPageBreak(PageBreakInfo pageBreak);
        protected override void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        public void FillPageWithBricks(DocumentBand rootBand, DocumentBand docBand, RectangleF bounds);
        public override List<BandBricksPair> GetAddedBands();
        protected override void IncreaseBuildInfo(DocumentBand rootBand, int bi, int value);
        protected void MarkLastAddedPage();
        protected override bool ShouldOverFulfill(DocumentBand docBand, RectangleF bounds);
        protected bool ShouldOverFulfillCore(DocumentBand docBand, RectangleF bounds, BandContentAnalyzer analyzer);
        protected override bool ShouldSplitContent(RectangleF bounds, float bandHeight, float offsetY);
        protected override void StartPageSection(IPageSection pageSection);
        protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);

        protected override bool Stopped { get; }

        protected virtual float UsefulPageWidth { get; }

        protected override int SectionID { get; }

        protected YPageContentEngine PageContentEngine { get; }

        public override int PageBricksCount { get; }

        protected PrintingSystemBase PrintingSystem { get; }

        protected RectangleF PageRect { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageRowBuilder.<>c <>9;
            public static Func<Brick, bool> <>9__19_1;

            static <>c();
            internal bool <AfterDocumentBandFill>b__19_1(Brick item);
        }
    }
}

