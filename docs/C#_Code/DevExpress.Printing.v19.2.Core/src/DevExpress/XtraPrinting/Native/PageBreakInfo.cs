namespace DevExpress.XtraPrinting.Native
{
    using System;

    public class PageBreakInfo : ValueInfo
    {
        private CustomPageData nextPageData;

        public PageBreakInfo(float pageBreakValue);
        public PageBreakInfo(float pageBreakValue, CustomPageData nextPageData);
        public static PageBreakInfo CreateMaxPageBreak();

        public CustomPageData NextPageData { get; }
    }
}

