namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;

    internal class PagesExportHelper
    {
        private PrintingSystemBase ps;
        private PageByPageExportOptionsBase options;
        private int[] pageIndices;

        public PagesExportHelper(PrintingSystemBase ps, PageByPageExportOptionsBase options);
        public string[] Execute(string filePath, Action1<Stream> export);
        public string[] Execute(int progressRange, string filePath, Action1<Stream> calback);
        private static string GetFilePathWithPageIndex(string fileName, string pageIndex, int maxPageIndex);
        public static string GetStringWithPageIndex(string str, string pageIndex, int maxPageIndex);

        public int PageCount { get; }
    }
}

