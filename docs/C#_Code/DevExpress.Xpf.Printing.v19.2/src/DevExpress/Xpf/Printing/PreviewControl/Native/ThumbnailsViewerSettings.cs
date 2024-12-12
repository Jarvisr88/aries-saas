namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ThumbnailsViewerSettings
    {
        private DocumentPreviewControl owner;
        private ThumbnailsDocumentViewModel thumbnailsDocument;

        public event EventHandler Invalidate;

        public event EventHandler InvalidateTextureCache;

        protected virtual ThumbnailsDocumentViewModel CreateThumbnailsDocument() => 
            new ThumbnailsDocumentViewModel(this.Document);

        private ThumbnailsDocumentViewModel GetActualThumnailsDocument()
        {
            ThumbnailsDocumentViewModel model;
            if (this.thumbnailsDocument == null)
            {
                this.thumbnailsDocument = model = this.CreateThumbnailsDocument();
                return model;
            }
            if (ReferenceEquals(this.thumbnailsDocument.OwnerDocument, this.Document))
            {
                return this.thumbnailsDocument;
            }
            this.thumbnailsDocument.AllowSynchronization(false);
            this.thumbnailsDocument = model = this.CreateThumbnailsDocument();
            return model;
        }

        protected internal virtual void Initialize(DocumentPreviewControl owner)
        {
            this.owner = owner;
        }

        protected internal void RaiseInvalidate()
        {
            this.Invalidate.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected internal void RaiseInvalidateTextureCache()
        {
            this.InvalidateTextureCache.Do<EventHandler>(x => x(this, EventArgs.Empty));
        }

        protected internal virtual void Release()
        {
            this.owner = null;
        }

        internal void UpdateThumbnailsDocument()
        {
            this.GetActualThumnailsDocument();
        }

        protected DocumentPreviewControl Owner =>
            this.owner;

        protected internal IDocumentViewModel Document
        {
            get
            {
                Func<DocumentPreviewControl, IDocumentViewModel> evaluator = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<DocumentPreviewControl, IDocumentViewModel> local1 = <>c.<>9__4_0;
                    evaluator = <>c.<>9__4_0 = x => x.Document;
                }
                return this.Owner.With<DocumentPreviewControl, IDocumentViewModel>(evaluator);
            }
        }

        public ThumbnailsDocumentViewModel ThumbnailsDocument =>
            this.GetActualThumnailsDocument();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsViewerSettings.<>c <>9 = new ThumbnailsViewerSettings.<>c();
            public static Func<DocumentPreviewControl, IDocumentViewModel> <>9__4_0;

            internal IDocumentViewModel <get_Document>b__4_0(DocumentPreviewControl x) => 
                x.Document;
        }
    }
}

