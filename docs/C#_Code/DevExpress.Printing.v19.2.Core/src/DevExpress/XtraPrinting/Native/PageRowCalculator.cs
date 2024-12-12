namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Native.LayoutAdjustment;
    using System;
    using System.Drawing;

    public class PageRowCalculator : PageRowBuilderBase
    {
        private float usefulPageWidth;
        public float y;
        public float bottomSpan;

        public PageRowCalculator(float usefulPageWidth);
        protected override void ApplyBottomSpan(float bottomSpan, RectangleF bounds);
        protected override FillPageResult BuildZOrderMultiColumnInternal(DocumentBand rootBand, RectangleF bounds, MultiColumn mc);
        protected override bool CanProcessDetail(DocumentBand rootBand, PageBuildInfo pageBuildInfo);
        public override void CopyFrom(PageRowBuilderBase source);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        protected override LayoutAdjuster CreateLayoutAdjuster();
        protected override void ResetTopSpan(FillPageResult fillPageResult, DocumentBand docBand);
        protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);

        public override bool CanApplyPageBreaks { get; set; }
    }
}

