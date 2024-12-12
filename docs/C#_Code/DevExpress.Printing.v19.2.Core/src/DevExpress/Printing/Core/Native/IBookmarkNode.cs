namespace DevExpress.Printing.Core.Native
{
    using System;
    using System.Collections.Generic;

    public interface IBookmarkNode
    {
        string Text { get; }

        int PageIndex { get; }

        string Indices { get; }

        IEnumerable<IBookmarkNode> Nodes { get; }
    }
}

