namespace DevExpress.XtraPrinting.Native.PageBuilder
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class RollPageBuildEngine : SinglePageBuildEngine
    {
        public RollPageBuildEngine(PrintingDocument document);
        protected override void AfterBuildPage(PSPage page, RectangleF usefulPageArea);
        protected override void Build();
        protected override PageRowBuilder CreatePageRowBuilder(YPageContentEngine psPage);
        protected override void InitializeContentEngine(YPageContentEngine contentEngine);

        protected override RectangleF ActualUsefulPageRectF { get; }

        private class CustomXEngine : SimpleXPageContentEngine
        {
            public override List<PSPage> CreatePages(PSPage source, RectangleF usefulArea);
            private Size GetPageSize(ReadonlyPageData pageData, Size bricksSize);
        }
    }
}

