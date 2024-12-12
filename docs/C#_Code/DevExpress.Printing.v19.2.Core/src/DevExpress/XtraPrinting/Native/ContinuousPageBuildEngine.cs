namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ContinuousPageBuildEngine : PageBuildEngine
    {
        public ContinuousPageBuildEngine(PrintingDocument document);
        public ContinuousPageBuildEngine(PrintingDocument document, XPageContentEngine xContentEngine, bool fillEmptySpace);
        protected override YPageContentEngine CreateContentEngine(PSPage psPage, YPageContentEngine previous);
        protected override PSPage CreatePage(SizeF pageSize);
        protected override PageRowBuilder CreatePageRowBuilder(YPageContentEngine psPage);
        protected override void ResetRootBand(DocumentBand rootBand);

        protected override RectangleF ActualUsefulPageRectF { get; }

        protected class ContinuousContentEngine : YPageContentEngine
        {
            public ContinuousContentEngine(PSPage psPage, PrintingSystemBase ps);
            public override void CorrectPrintAtBottomBricks(List<BandBricksPair> docBands, float pageBottom, bool ignoreBottomSpan);
        }
    }
}

