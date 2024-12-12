namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfPageList : IList<PdfPage>, ICollection<PdfPage>, IEnumerable<PdfPage>, IEnumerable
    {
        private readonly PdfDeferredPageList pages;
        private readonly PdfDocumentCatalog documentCatalog;
        private int nodeObjectNumber;

        public event EventHandler Changed;

        public PdfPageList(PdfDocumentCatalog documentCatalog)
        {
            this.nodeObjectNumber = -1;
            this.documentCatalog = documentCatalog;
            this.pages = new PdfDeferredPageList();
        }

        public PdfPageList(PdfPageTreeNode source, PdfDocumentCatalog documentCatalog)
        {
            this.nodeObjectNumber = -1;
            this.documentCatalog = documentCatalog;
            this.pages = new PdfDeferredPageList(source);
        }

        public PdfPage Add(PdfPage item)
        {
            PdfPage page = null;
            if (item != null)
            {
                page = this.ClonePage(item);
                this.pages.Add(page);
                this.RaiseChanged();
            }
            return page;
        }

        public PdfPage AddNewPage(PdfPage item)
        {
            if (item != null)
            {
                this.pages.Add(item);
                this.RaiseChanged();
            }
            return item;
        }

        public void AppendDocument(PdfDocumentCatalog catalog)
        {
            this.pages.AddRange(this.documentCatalog.Objects.ClonePages(catalog.Pages, true));
        }

        private bool CheckItem(PdfArticleThread articleThread, PdfPage page)
        {
            PdfBead firstBead = articleThread.FirstBead;
            if (firstBead == null)
            {
                return true;
            }
            if (ReferenceEquals(firstBead.Page, page))
            {
                PdfBead bead3 = null;
                PdfBead next = firstBead.Next;
                while (true)
                {
                    if (!ReferenceEquals(next, firstBead))
                    {
                        if (ReferenceEquals(next.Page, page))
                        {
                            next = next.Next;
                            continue;
                        }
                        bead3 = next;
                    }
                    if (bead3 == null)
                    {
                        return true;
                    }
                    firstBead = bead3;
                    articleThread.FirstBead = firstBead;
                    break;
                }
            }
            PdfBead bead2 = firstBead;
            for (PdfBead bead5 = bead2.Next; !ReferenceEquals(bead5, firstBead); bead5 = bead5.Next)
            {
                if (!ReferenceEquals(bead5.Page, page))
                {
                    bead2.Next = bead5;
                    bead5.Previous = bead2;
                    bead2 = bead5;
                }
            }
            firstBead.Previous = bead2;
            bead2.Next = firstBead;
            return false;
        }

        private bool CheckItem(PdfInteractiveFormField element, PdfPage page)
        {
            this.DoWithItems<PdfInteractiveFormField>(element.Kids, k => this.CheckItem(k, page));
            return (((element.Widget == null) || !ReferenceEquals(element.Widget.Page, page)) ? ((element.Kids != null) && (element.Kids.Count == 0)) : true);
        }

        private bool CheckItem(PdfLogicalStructureElementList elements, PdfPage page)
        {
            this.DoWithItems<PdfLogicalStructureElement>(elements, item => this.CheckItem(item, page));
            return (elements.Count == 0);
        }

        private bool CheckItem(PdfLogicalStructureItem item, PdfPage page)
        {
            if (ReferenceEquals(item.ContainingPage, page))
            {
                return true;
            }
            PdfLogicalStructureElement element = item as PdfLogicalStructureElement;
            if (element != null)
            {
                this.DoWithItems<PdfLogicalStructureItem>(element.Kids, kid => this.CheckItem(kid, page));
                for (PdfLogicalStructureElement element2 = element.Parent as PdfLogicalStructureElement; element2 != null; element2 = element2.Parent as PdfLogicalStructureElement)
                {
                    if (ReferenceEquals(element2.Page, page))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private PdfPage ClonePage(PdfPage page)
        {
            PdfPage[] pages = new PdfPage[] { page };
            return this.documentCatalog.Objects.ClonePages(pages, false)[0];
        }

        public bool Contains(PdfPage item) => 
            this.pages.Contains(item);

        private void DeleteFromOutlines(PdfOutlineItem item, PdfPage page)
        {
            if (item != null)
            {
                PdfOutline first = item.First;
                while (true)
                {
                    if (first == null)
                    {
                        item.UpdateCount();
                        break;
                    }
                    PdfDestination destination = first.Destination;
                    if (destination == null)
                    {
                        PdfGoToAction action = first.Action as PdfGoToAction;
                        if (action != null)
                        {
                            destination = action.Destination;
                        }
                    }
                    if ((destination != null) && ReferenceEquals(destination.Page, page))
                    {
                        PdfOutline prev = first.Prev;
                        PdfOutline next = first.Next;
                        if (prev != null)
                        {
                            prev.Next = next;
                        }
                        if (next != null)
                        {
                            next.Prev = prev;
                        }
                        if (ReferenceEquals(item.First, first))
                        {
                            item.First = next;
                        }
                        if (ReferenceEquals(item.Last, first))
                        {
                            item.Last = prev;
                        }
                    }
                    this.DeleteFromOutlines(first, page);
                    first = first.Next;
                }
            }
        }

        private bool DeletePage(PdfPage page)
        {
            foreach (PdfPage page2 in this.pages)
            {
                page2.EnsureAnnotations();
            }
            PdfNames names = this.documentCatalog.Names;
            if (names != null)
            {
                this.DoWithItems<string, PdfPage>(names.PageNames, p => ReferenceEquals(p, page));
                names.RemoveDestinations(p => (p != null) && ReferenceEquals(p.Page, page));
                this.DoWithItems<string, PdfSpiderSet>(names.WebCaptureContentSetsIds, delegate (PdfSpiderSet w) {
                    Func<PdfPage, bool> <>9__3;
                    Func<PdfPage, bool> action = <>9__3;
                    if (<>9__3 == null)
                    {
                        Func<PdfPage, bool> local1 = <>9__3;
                        action = <>9__3 = p => ReferenceEquals(p, page);
                    }
                    this.DoWithItems<PdfPage>((IList<PdfPage>) w.PageSet, action);
                    return false;
                });
                this.DoWithItems<string, PdfSpiderSet>(names.WebCaptureContentSetsUrls, delegate (PdfSpiderSet w) {
                    Func<PdfPage, bool> <>9__5;
                    Func<PdfPage, bool> action = <>9__5;
                    if (<>9__5 == null)
                    {
                        Func<PdfPage, bool> local1 = <>9__5;
                        action = <>9__5 = p => ReferenceEquals(p, page);
                    }
                    this.DoWithItems<PdfPage>((IList<PdfPage>) w.PageSet, action);
                    return false;
                });
            }
            this.DoWithItems<PdfArticleThread>(this.documentCatalog.Threads, item => this.CheckItem(item, page));
            PdfLogicalStructure logicalStructure = this.documentCatalog.LogicalStructure;
            if (logicalStructure != null)
            {
                logicalStructure.Resolve();
                this.DoWithItems<PdfLogicalStructureItem>(logicalStructure.Kids, item => this.CheckItem(item, page));
                this.DoWithItems<int, PdfLogicalStructureElementList>(logicalStructure.Parents, item => this.CheckItem(item, page));
                this.DoWithItems<string, PdfLogicalStructureItem>(logicalStructure.Elements, item => this.CheckItem(item, page));
            }
            this.DeleteFromOutlines(this.documentCatalog.Outlines, page);
            this.DoWithItems<PdfPage>(this.pages, delegate (PdfPage p) {
                Func<PdfAnnotation, bool> <>9__11;
                Func<PdfAnnotation, bool> action = <>9__11;
                if (<>9__11 == null)
                {
                    Func<PdfAnnotation, bool> local1 = <>9__11;
                    action = <>9__11 = delegate (PdfAnnotation a) {
                        PdfLinkAnnotation annotation = a as PdfLinkAnnotation;
                        if (annotation != null)
                        {
                            PdfGoToAction action = annotation.Action as PdfGoToAction;
                            PdfDestination destination = annotation.Destination;
                            if (((action != null) && ((action.Destination != null) && ReferenceEquals(action.Destination.Page, page))) || ((destination != null) && ReferenceEquals(destination.Page, page)))
                            {
                                return true;
                            }
                        }
                        return ReferenceEquals(a.Page, page);
                    };
                }
                this.DoWithItems<PdfAnnotation>(p.Annotations, action);
                return false;
            });
            PdfInteractiveForm acroForm = this.documentCatalog.AcroForm;
            if (acroForm != null)
            {
                this.DoWithItems<PdfInteractiveFormField>(acroForm.Fields, f => this.CheckItem(f, page) || ((f.Widget != null) && ReferenceEquals(f.Widget.Page, page)));
                this.DoWithItems<PdfInteractiveFormField>(acroForm.CalculationOrder, co => (co != null) && ((co.Widget != null) && ReferenceEquals(co.Widget.Page, page)));
            }
            if ((this.documentCatalog.OpenDestination != null) && ReferenceEquals(this.documentCatalog.OpenDestination.Page, page))
            {
                this.documentCatalog.OpenDestination = null;
            }
            PdfGoToAction openAction = this.documentCatalog.OpenAction as PdfGoToAction;
            if ((openAction != null) && ((openAction.Destination != null) && ReferenceEquals(openAction.Destination.Page, page)))
            {
                this.documentCatalog.OpenAction = null;
            }
            if (page.Parent != null)
            {
                page.Parent.RemovePage(page);
            }
            bool flag = this.pages.Remove(page);
            if (flag)
            {
                this.RaiseChanged();
            }
            return flag;
        }

        public void DeletePage(int pageNumber)
        {
            int count = this.pages.Count;
            if ((pageNumber < 1) || (pageNumber > count))
            {
                throw new ArgumentOutOfRangeException("pageNumber", string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectPageNumber), count));
            }
            this.DeletePage(this.pages[pageNumber - 1]);
        }

        private void DoWithItems<Tkey, Tvalue>(IDictionary<Tkey, Tvalue> dictionary, Func<Tvalue, bool> action)
        {
            if (dictionary != null)
            {
                foreach (Tkey local in new List<Tkey>(dictionary.Keys))
                {
                    if (action(dictionary[local]))
                    {
                        dictionary.Remove(local);
                    }
                }
            }
        }

        private void DoWithItems<T>(IList<T> list, Func<T, bool> action)
        {
            if (list != null)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (action(list[i]))
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        public PdfPage FindPage(int objectNumber)
        {
            PdfPage page2;
            using (IEnumerator<PdfPage> enumerator = this.pages.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPage current = enumerator.Current;
                        if (current.ObjectNumber != objectNumber)
                        {
                            continue;
                        }
                        page2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return page2;
        }

        private int GetNodeNumber(PdfObjectCollection objects)
        {
            if (this.nodeObjectNumber == -1)
            {
                int num = objects.LastObjectNumber + 1;
                objects.LastObjectNumber = num;
                this.nodeObjectNumber = num;
            }
            return this.nodeObjectNumber;
        }

        public PdfPageTreeNode GetPageNode(PdfObjectCollection objects, bool withPages)
        {
            IEnumerable<PdfPage> pages = withPages ? ((IEnumerable<PdfPage>) this.pages) : ((IEnumerable<PdfPage>) new PdfPage[0]);
            PdfPageTreeNode node1 = new PdfPageTreeNode(this.documentCatalog, null, null, 0, pages);
            node1.ObjectNumber = this.GetNodeNumber(objects);
            return node1;
        }

        public PdfPage Insert(int index, PdfPage item)
        {
            PdfPage page = null;
            if (item != null)
            {
                page = this.ClonePage(item);
                this.pages.Insert(index, page);
                this.RaiseChanged();
            }
            return page;
        }

        public PdfPage InsertNewPage(int index, PdfPage item)
        {
            if (item != null)
            {
                this.pages.Insert(index, item);
                this.RaiseChanged();
            }
            return item;
        }

        private void RaiseChanged()
        {
            EventHandler changed = this.Changed;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

        void ICollection<PdfPage>.Add(PdfPage item)
        {
            this.Add(item);
        }

        void ICollection<PdfPage>.Clear()
        {
            while (this.pages.Count > 0)
            {
                this.DeletePage(this.pages.Count);
            }
        }

        void ICollection<PdfPage>.CopyTo(PdfPage[] array, int arrayIndex)
        {
            this.pages.CopyTo(array, arrayIndex);
        }

        bool ICollection<PdfPage>.Remove(PdfPage item) => 
            this.DeletePage(item);

        IEnumerator<PdfPage> IEnumerable<PdfPage>.GetEnumerator() => 
            this.pages.GetEnumerator();

        int IList<PdfPage>.IndexOf(PdfPage item) => 
            this.pages.IndexOf(item);

        void IList<PdfPage>.Insert(int index, PdfPage item)
        {
            this.Insert(index, item);
        }

        void IList<PdfPage>.RemoveAt(int index)
        {
            this.DeletePage((int) (index + 1));
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.pages.GetEnumerator();

        public PdfPage this[int index]
        {
            get => 
                this.pages[index];
            set
            {
                if (value != null)
                {
                    PdfPage page = null;
                    if (index < this.Count)
                    {
                        page = this.pages[index];
                    }
                    this.pages[index] = this.ClonePage(value);
                    this.RaiseChanged();
                    if (page != null)
                    {
                        this.DeletePage(page);
                    }
                }
            }
        }

        public int Count =>
            this.pages.Count;

        bool ICollection<PdfPage>.IsReadOnly =>
            false;

        private class PdfDeferredPageList : PdfDeferredList<PdfPage>
        {
            public PdfDeferredPageList()
            {
            }

            public PdfDeferredPageList(PdfPageTreeNode source) : base(((IEnumerable<PdfPage>) source).GetEnumerator(), source.Count)
            {
            }

            protected override PdfPage ParseObject(object value) => 
                (PdfPage) value;
        }
    }
}

