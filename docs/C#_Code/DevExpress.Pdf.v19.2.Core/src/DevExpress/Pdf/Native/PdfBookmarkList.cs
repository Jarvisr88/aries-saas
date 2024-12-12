namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PdfBookmarkList : IList<PdfBookmark>, ICollection<PdfBookmark>, IEnumerable<PdfBookmark>, IEnumerable, IPdfBookmarkParent
    {
        private readonly List<PdfBookmark> bookmarks;
        private readonly IPdfBookmarkParent parent;

        public PdfBookmarkList(IPdfBookmarkParent parent)
        {
            this.bookmarks = new List<PdfBookmark>();
            this.parent = parent;
        }

        public PdfBookmarkList(IPdfBookmarkParent parent, PdfOutlineItem item) : this(parent)
        {
            if (item != null)
            {
                for (PdfOutline outline = item.First; outline != null; outline = outline.Next)
                {
                    this.bookmarks.Add(new PdfBookmark(parent, outline));
                }
            }
        }

        public PdfBookmarkList(IPdfBookmarkParent parent, IEnumerable<PdfBookmark> items) : this(parent)
        {
            if (items != null)
            {
                foreach (PdfBookmark bookmark in items)
                {
                    this.bookmarks.Add(this.PrepareAndValidateItem(bookmark));
                }
            }
        }

        public static PdfOutlines CreateOutlines(IList<PdfBookmark> bookmarks) => 
            ((bookmarks == null) || (bookmarks.Count == 0)) ? null : new PdfOutlines(bookmarks);

        public void Invalidate()
        {
            this.parent.Invalidate();
        }

        private PdfBookmark PrepareAndValidateItem(PdfBookmark item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectBookmarkListValue));
            }
            PdfDestination destination = item.Destination;
            PdfDocumentCatalog documentCatalog = this.DocumentCatalog;
            if ((destination != null) && (documentCatalog != null))
            {
                PdfPage page = destination.Page;
                if ((page != null) && !ReferenceEquals(page.DocumentCatalog, documentCatalog))
                {
                    throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDestinationPage));
                }
            }
            item.Parent = this.parent;
            this.Invalidate();
            return item;
        }

        void ICollection<PdfBookmark>.Add(PdfBookmark item)
        {
            this.bookmarks.Add(this.PrepareAndValidateItem(item));
        }

        void ICollection<PdfBookmark>.Clear()
        {
            if (this.bookmarks.Count > 0)
            {
                this.bookmarks.Clear();
                this.Invalidate();
            }
        }

        bool ICollection<PdfBookmark>.Contains(PdfBookmark item) => 
            this.bookmarks.Contains(item);

        void ICollection<PdfBookmark>.CopyTo(PdfBookmark[] array, int arrayIndex)
        {
            this.bookmarks.CopyTo(array, arrayIndex);
        }

        bool ICollection<PdfBookmark>.Remove(PdfBookmark item)
        {
            bool flag = this.bookmarks.Remove(item);
            if (flag)
            {
                this.Invalidate();
            }
            return flag;
        }

        IEnumerator<PdfBookmark> IEnumerable<PdfBookmark>.GetEnumerator() => 
            this.bookmarks.GetEnumerator();

        int IList<PdfBookmark>.IndexOf(PdfBookmark item) => 
            this.bookmarks.IndexOf(item);

        void IList<PdfBookmark>.Insert(int index, PdfBookmark item)
        {
            this.bookmarks.Insert(index, this.PrepareAndValidateItem(item));
        }

        void IList<PdfBookmark>.RemoveAt(int index)
        {
            this.bookmarks.RemoveAt(index);
            this.Invalidate();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.bookmarks.GetEnumerator();

        public PdfDocumentCatalog DocumentCatalog =>
            this.parent.DocumentCatalog;

        bool ICollection<PdfBookmark>.IsReadOnly =>
            false;

        int ICollection<PdfBookmark>.Count =>
            this.bookmarks.Count;

        PdfBookmark IList<PdfBookmark>.this[int index]
        {
            get => 
                this.bookmarks[index];
            set => 
                this.bookmarks[index] = this.PrepareAndValidateItem(value);
        }
    }
}

