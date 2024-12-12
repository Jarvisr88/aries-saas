namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public static class StreamingExt
    {
        public static void CleanupDocument(this IStreamingDocument document, PageBuildEngine engine, Page page, bool clearPageContent)
        {
            new CleanupBandVisitor(document.Root, engine).Visit(page);
            document.RemovePageBandsIndexes(page);
            if (clearPageContent)
            {
                page.ClearContent();
            }
        }

        public static void ClearContent(this Page page)
        {
            PSPage page2 = page as PSPage;
            if (page2 != null)
            {
                page2.ClearContent();
            }
        }
    }
}

