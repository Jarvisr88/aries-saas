namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AttachmentOpeningEventArgs : RoutedEventArgs
    {
        public AttachmentOpeningEventArgs(PdfFileAttachment attachment) : base(PdfViewerControl.AttachmentOpeningEvent)
        {
            this.Attachment = attachment;
        }

        public PdfFileAttachment Attachment { get; private set; }

        public bool Cancel { get; set; }
    }
}

