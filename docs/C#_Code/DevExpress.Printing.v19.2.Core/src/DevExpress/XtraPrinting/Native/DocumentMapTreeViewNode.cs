namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DocumentMapTreeViewNode : IBookmarkNode
    {
        public DocumentMapTreeViewNode();
        public DocumentMapTreeViewNode(int pageIndex, string text, string associatedElementTag);

        public int PageIndex { get; set; }

        public string Text { get; set; }

        public string AssociatedElementTag { get; set; }

        public IList<DocumentMapTreeViewNode> Nodes { get; set; }

        string IBookmarkNode.Text { get; }

        int IBookmarkNode.PageIndex { get; }

        string IBookmarkNode.Indices { get; }

        IEnumerable<IBookmarkNode> IBookmarkNode.Nodes { get; }
    }
}

