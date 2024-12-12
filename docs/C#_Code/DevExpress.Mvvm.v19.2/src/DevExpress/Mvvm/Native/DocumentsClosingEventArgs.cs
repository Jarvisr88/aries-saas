namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DocumentsClosingEventArgs : CancelEventArgs
    {
        public DocumentsClosingEventArgs(IEnumerable<IDocument> documents)
        {
            this.Documents = documents;
        }

        public IEnumerable<IDocument> Documents { get; private set; }
    }
}

