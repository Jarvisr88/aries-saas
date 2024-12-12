namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Collections.Generic;

    public interface IDocument
    {
        IEnumerable<IPage> Pages { get; }

        bool IsLoaded { get; }
    }
}

