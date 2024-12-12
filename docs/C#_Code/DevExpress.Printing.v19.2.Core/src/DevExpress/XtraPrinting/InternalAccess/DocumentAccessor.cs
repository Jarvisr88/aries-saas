namespace DevExpress.XtraPrinting.InternalAccess
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public static class DocumentAccessor
    {
        public static void LoadPage(Document document, int pageIndex)
        {
            document.LoadPage(pageIndex);
        }

        public static void SetInfoString(PSDocument doc, string value)
        {
            doc.InfoString = value;
        }
    }
}

