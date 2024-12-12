namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class PageRangeIndexMapper
    {
        private bool isIdentity;

        public PageRangeIndexMapper(PageByPageExportOptionsBase pageByPageOptions, int[] pageIndices);
        private PageRangeIndexMapper(bool isIdentity, int[] pageIndices);
        public static PageRangeIndexMapper CreateIdentityMapper();
        public int GetPageRangeIndex(int pageIndex);

        public virtual int[] PageIndices { get; protected set; }
    }
}

