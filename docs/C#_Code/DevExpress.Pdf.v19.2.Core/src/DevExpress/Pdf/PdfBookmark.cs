namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfBookmark : IPdfBookmarkParent
    {
        private readonly PdfAction action;
        private PdfBookmarkList children;
        private string title;
        private bool isItalic;
        private bool isBold;
        private bool isInitiallyClosed;
        private PdfDestinationObject destinationObject;
        private IPdfBookmarkParent parent;
        private PdfRGBColor textColor;

        public PdfBookmark()
        {
            this.textColor = new PdfRGBColor();
            this.children = new PdfBookmarkList(this);
        }

        internal PdfBookmark(PdfDestinationObject destinationObject) : this()
        {
            this.destinationObject = destinationObject;
        }

        internal PdfBookmark(IPdfBookmarkParent parent, PdfOutline outline)
        {
            this.textColor = new PdfRGBColor();
            this.parent = parent;
            this.title = outline.Title;
            this.destinationObject = outline.DestinationObject;
            this.isItalic = outline.IsItalic;
            this.isBold = outline.IsBold;
            this.isInitiallyClosed = outline.Closed;
            this.children = new PdfBookmarkList(this, outline);
            this.textColor = new PdfRGBColor(outline.Color);
            this.action = outline.Action;
        }

        internal PdfBookmark(PdfBookmark bookmark, PdfDestinationObject destinationObject, PdfAction action) : this(destinationObject)
        {
            this.title = bookmark.Title;
            this.isItalic = bookmark.IsItalic;
            this.isBold = bookmark.IsBold;
            this.isInitiallyClosed = bookmark.IsInitiallyClosed;
            this.textColor = bookmark.TextColor;
            this.action = action;
        }

        void IPdfBookmarkParent.Invalidate()
        {
            if (this.parent != null)
            {
                this.parent.Invalidate();
            }
        }

        private void Invalidate()
        {
            ((IPdfBookmarkParent) this).Invalidate();
        }

        internal IPdfBookmarkParent Parent
        {
            get => 
                this.parent;
            set => 
                this.parent = value;
        }

        internal PdfDestinationObject DestinationObject =>
            this.destinationObject;

        public string Title
        {
            get => 
                this.title;
            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    this.Invalidate();
                }
            }
        }

        public PdfDestination Destination
        {
            get => 
                this.destinationObject?.GetDestination(((IPdfBookmarkParent) this).DocumentCatalog, true);
            set
            {
                if (!ReferenceEquals(value, this.Destination))
                {
                    PdfDocumentCatalog documentCatalog = ((IPdfBookmarkParent) this).DocumentCatalog;
                    if ((documentCatalog != null) && ((value != null) && !ReferenceEquals(documentCatalog, value.Page.DocumentCatalog)))
                    {
                        throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDestinationPage));
                    }
                    this.destinationObject = new PdfDestinationObject(value);
                    this.Invalidate();
                }
            }
        }

        public PdfAction Action =>
            this.action;

        public bool IsItalic
        {
            get => 
                this.isItalic;
            set
            {
                if (value != this.isItalic)
                {
                    this.isItalic = value;
                    this.Invalidate();
                }
            }
        }

        public bool IsBold
        {
            get => 
                this.isBold;
            set
            {
                if (value != this.isBold)
                {
                    this.isBold = value;
                    this.Invalidate();
                }
            }
        }

        public bool IsInitiallyClosed
        {
            get => 
                this.isInitiallyClosed;
            set
            {
                if (value != this.isInitiallyClosed)
                {
                    this.isInitiallyClosed = value;
                    this.Invalidate();
                }
            }
        }

        public IList<PdfBookmark> Children
        {
            get => 
                this.children;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Children", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectBookmarkListValue));
                }
                this.children = new PdfBookmarkList(this, value);
                this.Invalidate();
            }
        }

        public PdfRGBColor TextColor
        {
            get => 
                this.textColor;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("TextColor");
                }
                this.textColor = value;
                this.Invalidate();
            }
        }

        PdfDocumentCatalog IPdfBookmarkParent.DocumentCatalog =>
            this.parent?.DocumentCatalog;
    }
}

