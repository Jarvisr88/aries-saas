namespace DevExpress.XtraPrinting.Native.PageBuilder
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class SinglePageBuildEngine : ContinuousPageBuildEngine
    {
        public SinglePageBuildEngine(PrintingDocument document);
        protected SinglePageBuildEngine(PrintingDocument document, XPageContentEngine xContentEngine);
        protected override void AfterBuildPage(PSPage page, RectangleF usefulPageArea);

        private class CustomXEngine : XPageContentEngine
        {
            public override List<PSPage> CreatePages(PSPage source, RectangleF usefulArea);
            private Size GetPageSize(ReadonlyPageData pageData, Size bricksSize);
        }
    }
}

