namespace DevExpress.XtraPrinting.InternalAccess
{
    using DevExpress.XtraPrinting;
    using System;
    using System.IO;

    public static class PrintingSystemAccessor
    {
        public static void ForceLoadDocument(PrintingSystemBase printingSystem)
        {
            printingSystem.Document.ForceLoad();
        }

        public static void LoadVirtualDocument(PrintingSystemBase ps, Stream stream)
        {
            ps.LoadVirtualDocument(stream);
        }

        public static void LoadVirtualDocument(PrintingSystemBase ps, string filePath)
        {
            ps.LoadVirtualDocument(filePath);
        }

        public static void LoadVirtualDocument(PrintingSystemBase ps, Stream stream, int pageIndex)
        {
            ps.LoadVirtualDocument(stream);
            ps.Document.LoadPage(pageIndex);
        }

        public static void LoadVirtualDocument(PrintingSystemBase ps, string filePath, int pageIndex)
        {
            ps.LoadVirtualDocument(filePath);
            ps.Document.LoadPage(pageIndex);
        }

        public static void SaveIndependentPages(PrintingSystemBase ps, Stream stream)
        {
            ps.SaveIndependentPages(stream);
        }
    }
}

