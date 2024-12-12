namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class PageList : IList<Page>, ICollection<Page>, IEnumerable<Page>, IEnumerable, IPageRepository
    {
        private IList<Page> list;
        private readonly DevExpress.XtraPrinting.Document document;
        private int raiseDocumentChangedLockCount;

        public PageList(DevExpress.XtraPrinting.Document document) : this(document, new IndexedDictionary<Page>())
        {
        }

        public PageList(DevExpress.XtraPrinting.Document document, IList<Page> list)
        {
            this.document = document;
            this.list = list;
        }

        public virtual void Add(Page page)
        {
            if (!this.document.IsCreated)
            {
                this.ValidatePage(page);
                this.InnerList.Add(page);
            }
            else
            {
                long[] pageIds = this.GetPageIds(this.Count);
                IList<BookmarkNode> en = this.InsertPageBookmarks(pageIds, page);
                IList<EditingField> list2 = this.InsertPageEditingFields(pageIds, page);
                this.ValidatePage(page);
                this.InnerList.Add(page);
                en.ForEach<BookmarkNode>(node => node.Pair.UpdatePageIndex(this));
                list2.ForEach<EditingField>(field => field.UpdatePageIndex(this));
            }
            if (page.Owner == null)
            {
                page.SetOwner(this, this.InnerList.Count - 1);
            }
            this.OnInsertComplete(this.InnerList.Count - 1, page);
        }

        internal void AddPageInternal(Page page)
        {
            try
            {
                this.SuppressRaiseDocumentChanged();
                this.Add(page);
            }
            finally
            {
                this.AllowRaiseDocumentChanged();
            }
        }

        public void AddRange(IEnumerable pages)
        {
            this.SuppressRaiseDocumentChanged();
            foreach (Page page in pages)
            {
                this.Add(page);
            }
            this.AllowRaiseDocumentChanged();
            this.RaiseDocumentChanged();
        }

        private void AllowRaiseDocumentChanged()
        {
            this.raiseDocumentChangedLockCount--;
        }

        public void Clear()
        {
            this.OnClear(this.Count);
            this.InnerList.Clear();
        }

        public bool Contains(Page item) => 
            this.InnerList.Contains(item);

        public void CopyTo(Page[] array, int arrayIndex)
        {
            this.InnerList.CopyTo(array, arrayIndex);
        }

        bool IPageRepository.TryGetPageByID(long id, out Page page, out int index)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                if (id == this.list[i].ID)
                {
                    index = i;
                    page = this.list[i];
                    return true;
                }
            }
            index = -1;
            page = null;
            return false;
        }

        public IEnumerator GetEnumerator() => 
            this.InnerList.GetEnumerator();

        protected virtual long[] GetPageIds(int length)
        {
            long[] numArray = new long[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this[i].ID;
            }
            return numArray;
        }

        public virtual int GetPageIndexByID(long id)
        {
            Page page;
            return (!this.TryGetPageByID(id, out page) ? -1 : page.Index);
        }

        public IEnumerable<Page> GetPagesByIndexes(IEnumerable<int> indexes) => 
            from i in indexes select this.InnerList[i];

        public int IndexOf(Page page) => 
            this.InnerList.IndexOf(page);

        public virtual void Insert(int index, Page page)
        {
            if (this.document.IsCreated)
            {
                long[] pageIds = this.GetPageIds(index);
                this.InsertPageBookmarks(pageIds, page);
                this.InsertPageEditingFields(pageIds, page);
            }
            this.ValidatePage(page);
            this.InnerList.Insert(index, page);
            this.OnInsertComplete(index, page);
        }

        private IList<BookmarkNode> InsertPageBookmarks(long[] prevPages, Page page)
        {
            if (page.Document == null)
            {
                return new BookmarkNode[0];
            }
            IList<BookmarkNode> pageNodes = page.Document.BookmarkNodes.GetPageNodes(page);
            if (pageNodes.Count > 0)
            {
                this.document.BookmarkNodes.InsertNodes(prevPages, pageNodes);
            }
            return pageNodes;
        }

        private IList<EditingField> InsertPageEditingFields(long[] prevPages, Page page)
        {
            if ((page.Document == null) || (page.Document.PrintingSystem == null))
            {
                return new EditingField[0];
            }
            IList<EditingField> pageEditingFields = page.Document.PrintingSystem.EditingFields.GetPageEditingFields(page);
            if (pageEditingFields.Count > 0)
            {
                this.document.PrintingSystem.EditingFields.InsertFields(prevPages, pageEditingFields);
            }
            return pageEditingFields;
        }

        protected virtual void InvalidateIndices(int fromIndex)
        {
            for (int i = fromIndex; i < this.Count; i++)
            {
                this[i].InvalidateIndex(i);
            }
        }

        protected virtual void OnClear(int count)
        {
            if ((this.document != null) && (count > 0))
            {
                this.document.OnClearPages();
            }
        }

        protected virtual void OnInsertComplete(int index, object value)
        {
            this.InvalidateIndices(index);
            this.document.PrintingSystem.OnPageInsertComplete(new PageInsertCompleteEventArgs(index));
            this.RaiseDocumentChanged();
        }

        protected virtual void OnRemove(int index, object value)
        {
            this[index].SetOwner(null, -1);
        }

        protected virtual void OnRemoveComplete(int index, object value)
        {
            this.InvalidateIndices(index);
            this.RaiseDocumentChanged();
        }

        protected void RaiseDocumentChanged()
        {
            if (this.raiseDocumentChangedLockCount == 0)
            {
                this.document.IsModified = true;
                this.document.CanChangePageSettings = false;
                this.document.OnContentChanged();
            }
        }

        public bool Remove(Page page)
        {
            int index = this.InnerList.IndexOf(page);
            if (index < 0)
            {
                throw new ArgumentException("page");
            }
            this.RemoveCore(page, index);
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            this.RemoveCore(this[index], index);
        }

        private void RemoveCore(Page page, int index)
        {
            this.OnRemove(index, page);
            this.InnerList.RemoveAt(index);
            this.OnRemoveComplete(index, page);
        }

        private void SuppressRaiseDocumentChanged()
        {
            this.raiseDocumentChangedLockCount++;
        }

        IEnumerator<Page> IEnumerable<Page>.GetEnumerator() => 
            this.InnerList.GetEnumerator();

        public Page[] ToArray()
        {
            Page[] array = new Page[this.InnerList.Count];
            this.InnerList.CopyTo(array, 0);
            return array;
        }

        public bool TryGetPageByID(long id, out Page page)
        {
            int index = 0;
            for (int i = 0; i < this.list.Count; i++)
            {
                Page page2 = this.list[i];
                if (id == page2.ID)
                {
                    page = page2;
                    page.SetOwner(this, index);
                    return true;
                }
                index++;
            }
            page = null;
            return false;
        }

        public bool TryGetPageByIndex(int index, out Page page)
        {
            if ((index >= 0) && (index < this.Count))
            {
                page = this[index];
                return true;
            }
            page = null;
            return false;
        }

        protected virtual void ValidatePage(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            if (this.InnerList.Contains(page))
            {
                this.SuppressRaiseDocumentChanged();
                this.Remove(page);
                this.AllowRaiseDocumentChanged();
            }
            page.SetOwner(this, -1);
        }

        protected internal IList<Page> InnerList =>
            this.list;

        internal DevExpress.XtraPrinting.Document Document =>
            this.document;

        [Description("Gets the first page within the PageList collection.")]
        public Page First =>
            (this.Count > 0) ? this[0] : null;

        [Description("Gets the last page in the PageList collection.")]
        public Page Last =>
            (this.Count > 0) ? this[this.Count - 1] : null;

        [Description("Gets or sets an item within the PageList collection at a specific index.")]
        public virtual Page this[int index]
        {
            get
            {
                Page page = this.InnerList[index];
                if (page.Owner == null)
                {
                    page.SetOwner(this, index);
                }
                return page;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public virtual int Count =>
            this.InnerList.Count;

        bool ICollection<Page>.IsReadOnly =>
            this.InnerList.IsReadOnly;
    }
}

