namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public static class DocumentMapTreeViewNodeBuilder
    {
        public static DocumentMapTreeViewNode Build(Document document);
        private static DocumentMapTreeViewNode CreateDocumentMapNode(BookmarkNode bookmarkNode);
    }
}

