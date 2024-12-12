namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows.Controls;

    public abstract class DocumentPreviewBase : Control
    {
        protected DocumentPreviewBase()
        {
        }

        public abstract IDocumentPreviewModel Model { get; set; }

        public abstract DevExpress.Xpf.Printing.DocumentViewer DocumentViewer { get; protected set; }
    }
}

