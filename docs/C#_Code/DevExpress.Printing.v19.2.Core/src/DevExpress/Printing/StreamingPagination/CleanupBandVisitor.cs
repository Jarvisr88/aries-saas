namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct CleanupBandVisitor : ICleanupBandVisitor
    {
        private PageBuildEngine engine;
        private RootDocumentBand root;
        public CleanupBandVisitor(RootDocumentBand root, PageBuildEngine engine)
        {
            this.engine = engine;
            this.root = root;
        }

        public void Visit(Page page)
        {
            int[] pageBandsIndexes = this.engine.GetPageBandsIndexes(page);
            if (pageBandsIndexes != null)
            {
                this.root.CleanupBands(pageBandsIndexes, 0);
            }
        }
    }
}

