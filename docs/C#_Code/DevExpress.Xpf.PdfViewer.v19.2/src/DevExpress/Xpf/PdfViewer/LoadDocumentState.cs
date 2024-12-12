namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class LoadDocumentState
    {
        public SynchronizationContext Current { get; set; }

        public object InnerState { get; set; }
    }
}

