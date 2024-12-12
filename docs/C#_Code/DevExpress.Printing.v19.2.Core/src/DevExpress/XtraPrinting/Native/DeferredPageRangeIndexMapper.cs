namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public class DeferredPageRangeIndexMapper : PageRangeIndexMapper
    {
        public DeferredPageRangeIndexMapper(PageByPageExportOptionsBase pageByPageOptions);
        public void SetPageIndices(int[] pageIndices);

        public override int[] PageIndices { get; }
    }
}

