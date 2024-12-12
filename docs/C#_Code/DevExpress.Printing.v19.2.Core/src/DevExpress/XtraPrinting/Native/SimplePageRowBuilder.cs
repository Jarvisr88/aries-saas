namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class SimplePageRowBuilder : PageRowBuilderBase
    {
        private Page currentPage;

        public SimplePageRowBuilder(PrintingSystemBase ps, Page page);
        protected override void AfterDocumentBandFill(DocumentBand docBand);
        protected override bool CanFillPageWithBricks(DocumentBand docBand);
        public override void CopyFrom(PageRowBuilderBase source);
        protected internal override PageRowBuilderBase CreateInternalPageRowBuilder();
        public void FillPageBricks(DocumentBand docBand, RectangleF bounds);
        protected override void IncreaseBuildInfo(DocumentBand rootBand, int bi, int value);
        protected override PageUpdateData UpdatePageContent(DocumentBand docBand, RectangleF bounds);

        protected PrintingSystemBase PrintingSystem { get; private set; }

        public Dictionary<Brick, RectangleF> PageBricks { get; private set; }

        public DocumentBand StartBand { get; private set; }
    }
}

