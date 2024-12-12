namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Native.LayoutAdjustment;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class PageRowBuilderBase
    {
        private bool forceSplit;
        private RectangleF pageBreakRect;
        private CustomPageData nextPageData;
        private DevExpress.XtraPrinting.Native.BuildInfoContainer buildInfoContainer;
        private PointD offset;
        private double negativeOffsetY;
        private bool canApplyPageBreaks;
        private DevExpress.XtraPrinting.Native.RenderHistory fRenderHistory;
        private DevExpress.XtraPrinting.Native.ProcessState fProcessState;
        private bool canUpdatePageContent;

        protected PageRowBuilderBase();
        protected virtual void AfterDocumentBandFill(DocumentBand docBand);
        protected virtual void ApplyBottomSpan(float bottomSpan, RectangleF bounds);
        private float ApplyPageBreaksInternal(DocumentBand docBand, RectangleF bounds, float offsetY);
        protected bool AreBoundsOverfull(RectangleF bounds, float bandHeight, float offsetY);
        protected virtual bool AreFriendsTogether(DocumentBand docBand, RectangleF bounds);
        protected virtual bool AreFriendsTogetherRecursive(DocumentBand docBand, RectangleF bounds);
        protected virtual FillPageResult BuildNOrderMultiColumn(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected FillPageResult BuildZOrderMultiColumn(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected virtual FillPageResult BuildZOrderMultiColumnInternal(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected virtual bool CanApplyPageBreak(PageBreakInfo pageBreak);
        protected virtual bool CanFillPageRecursive(DocumentBand docBand);
        protected virtual bool CanFillPageWithBricks(DocumentBand docBand);
        protected bool CanFillPageWithBricksInternal(DocumentBand docBand);
        private static bool CanProceedInternal(DocumentBand rootBand, PageRowBuilderBase.ProcessBandsDelegate process, PageBuildInfo pageBuildInfo);
        protected virtual bool CanProcessDetail(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        private bool CanProcessFooter(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        private bool CanProcessHeader(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        private static bool CanProcessReportFooter(DocumentBand rootBand, PageBuildInfo bi);
        private static bool CanProcessReportFooterCore(DocumentBand rootBand, int index);
        private bool CanProcessReportHeader(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        public virtual void CopyFrom(PageRowBuilderBase source);
        protected virtual void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom);
        protected internal abstract PageRowBuilderBase CreateInternalPageRowBuilder();
        protected virtual LayoutAdjuster CreateLayoutAdjuster();
        protected virtual void EndPageSection(IPageSection pageSection);
        public FillPageResult FillPage(DocumentBand rootBand, RectangleF bounds);
        public virtual FillPageResult FillPageForBand(DocumentBand rootBand, RectangleF bounds, RectangleF newBounds);
        private FillPageResult FillPageForBandCore(DocumentBand rootBand, RectangleF bounds, RectangleF newBounds);
        protected FillPageResult FillPageForBands(DocumentBand rootBand, RectangleF bounds, PageRowBuilderBase.ProcessBandsDelegate process);
        internal FillPageResult FillPageRecursive(DocumentBand docBand, RectangleF bounds);
        private FillPageResult FillPageSideBySide(DocumentBand rootBand, List<DocumentBand> bands, RectangleF bounds);
        public FillPageResult FillPageWithBricks(DocumentBand docBand, RectangleF bounds);
        protected FillPageResult FillReportDetails(DocumentBand rootBand, RectangleF bounds);
        protected virtual FillPageResult FillReportDetailsAndFooter(DocumentBand rootBand, RectangleF bounds);
        protected FillPageResult FillReportFooter(DocumentBand rootBand, RectangleF bounds);
        private FillPageResult FillReportHeader(DocumentBand rootBand, RectangleF bounds);
        public virtual List<BandBricksPair> GetAddedBands();
        public int GetBuildInfo(DocumentBand band);
        protected virtual RectangleF GetCorrectedBounds(DocumentBand rootBand, RectangleF bounds);
        public Pair<int, int> GetDetailRowIndexes(DocumentBand docBand);
        protected static FillPageResult GetFillResult(DocumentBand docBand, RectangleF bounds, double offsetY);
        private FillPageResult GetFillResult(DocumentBand rootBand, int index, bool canProceed);
        protected static FillPageResult GetFillResult(RectangleF bounds, float bandHeight, double offsetY);
        protected virtual DocumentBand GetPageFooterBand(DocumentBand rootBand);
        protected virtual DocumentBand GetPageHeaderBand(DocumentBand rootBand);
        private static float GetPageRelativeY(float pageOffset, float value, float offsetY);
        private static RectangleF GetRectangle(ISubreportDocumentBand docBand);
        private List<DocumentBand> GetSideBySideDocumentBands(IListWrapper<DocumentBand> bands, int index);
        protected virtual void IncreaseBuildInfo(DocumentBand rootBand, int bi, int value);
        public void IncreaseBuildInfoPure(DocumentBand rootBand, int bi, int value);
        public bool IsBuildCompleted(DocumentBand rootBand);
        private static bool IsPrintAtBottomInnerBand(List<DocumentBand> bands, DocumentBand docBand);
        private static bool IsSubreport(DocumentBand docBand);
        public void ResetBuildInfo();
        protected virtual void ResetTopSpan(FillPageResult fillPageResult, DocumentBand docBand);
        protected virtual bool ShouldOverFulfill(DocumentBand docBand, RectangleF bounds);
        protected virtual bool ShouldSplitContent(RectangleF bounds, float bandHeight, float offsetY);
        protected virtual void StartPageSection(IPageSection pageSection);
        protected abstract PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);
        private void UpdateRenderHistory(PageUpdateData pageUpdateData, DocumentBand docBand);
        protected virtual RectangleF ValidateBounds(DocumentBand rootBand, RectangleF bounds, RectangleF newBounds);

        public bool CanUpdatePageContent { get; set; }

        protected virtual bool Stopped { get; }

        protected virtual int SectionID { get; }

        protected int ProcessingSectionID { get; }

        protected Stack<int> ProcessingSectionIDs { get; set; }

        protected RectangleF PageBreakRect { get; set; }

        public CustomPageData NextPageData { get; set; }

        public IPageSection PageSection { get; set; }

        protected DevExpress.XtraPrinting.Native.BuildInfoContainer BuildInfoContainer { get; }

        public Dictionary<int, float> NegativeOffsets { get; }

        public virtual bool CanApplyPageBreaks { get; set; }

        public DevExpress.XtraPrinting.Native.ProcessState ProcessState { get; protected set; }

        public DevExpress.XtraPrinting.Native.RenderHistory RenderHistory { get; }

        public double FullOffsetY { get; set; }

        public virtual int PageBricksCount { get; }

        public double NegativeOffsetY { get; set; }

        public bool ForceSplit { get; set; }

        public PointD Offset { get; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        protected class OffsetHelperXY : OffsetHelperY
        {
            private float offsetX;

            public OffsetHelperXY(float offsetX);
            public override void UpdateBuilder(PageRowBuilderBase builder);
        }

        protected delegate bool ProcessBandsDelegate(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
    }
}

