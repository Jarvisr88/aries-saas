namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PdfOutlineTreeListItemCollection : ObservableCollection<PdfOutlineTreeListItem>
    {
        private readonly IList<PdfPage> pages;

        public PdfOutlineTreeListItemCollection(PdfDocument document)
        {
            this.pages = document.Pages;
            try
            {
                PdfOutlines outlines = document.Outlines;
                if (outlines != null)
                {
                    this.Add(outlines.First, 0);
                }
            }
            catch
            {
            }
        }

        private int Add(PdfOutline outline, int parentId)
        {
            int num = parentId;
            while (outline != null)
            {
                base.Add(new PdfOutlineTreeListItem(outline, ++num, parentId));
                if (outline.First != null)
                {
                    num = this.Add(outline.First, num);
                }
                outline = outline.Next;
            }
            return num;
        }

        private bool CanPrintPages(PdfOutline outline, bool printSection)
        {
            if ((outline.ActualDestination != null) && (!printSection || (this.GetNextPageNumber(outline) != 0)))
            {
                return true;
            }
            outline = outline.First;
            while (outline != null)
            {
                if (this.CanPrintPages(outline, printSection))
                {
                    return true;
                }
                outline = outline.Next;
            }
            return false;
        }

        public bool CanPrintPages(IEnumerable<PdfOutlineTreeListItem> items, bool printSection)
        {
            bool flag;
            using (IEnumerator<PdfOutlineTreeListItem> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfOutlineTreeListItem current = enumerator.Current;
                        if (!this.CanPrintPages(current.Outline, printSection))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private int GetNextPageNumber(PdfOutline outline)
        {
            while (true)
            {
                PdfOutline next = outline.Next;
                if (next != null)
                {
                    return this.GetPageNumber(next);
                }
                outline = outline.Parent as PdfOutline;
                if (outline == null)
                {
                    return (this.pages.Count + 1);
                }
            }
        }

        private int GetPageNumber(PdfOutline outline)
        {
            PdfDestination actualDestination = outline.ActualDestination;
            return ((actualDestination != null) ? (actualDestination.CreateTarget(this.pages).PageIndex + 1) : 0);
        }

        public int[] GetPrintPageNumbers(IEnumerable<PdfOutlineTreeListItem> items, bool printSection)
        {
            int[] numArray;
            List<int> list = new List<int>();
            using (IEnumerator<PdfOutlineTreeListItem> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfOutlineTreeListItem current = enumerator.Current;
                        using (IEnumerator enumerator2 = ((IEnumerable) new PdfOutlineEnumerator(current.Outline)).GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                PdfOutline outline = (PdfOutline) enumerator2.Current;
                                int pageNumber = this.GetPageNumber(outline);
                                if (pageNumber != 0)
                                {
                                    if (!list.Contains(pageNumber))
                                    {
                                        list.Add(pageNumber);
                                    }
                                    if (printSection)
                                    {
                                        int nextPageNumber = this.GetNextPageNumber(outline);
                                        if (nextPageNumber != 0)
                                        {
                                            if (nextPageNumber > pageNumber)
                                            {
                                                for (int i = pageNumber + 1; i < nextPageNumber; i++)
                                                {
                                                    if (!list.Contains(i))
                                                    {
                                                        list.Add(i);
                                                    }
                                                }
                                            }
                                            else if (nextPageNumber < pageNumber)
                                            {
                                                for (int i = pageNumber - 1; i > nextPageNumber; i--)
                                                {
                                                    if (!list.Contains(i))
                                                    {
                                                        list.Add(i);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            return new int[0];
                                        }
                                    }
                                    continue;
                                }
                                else
                                {
                                    numArray = new int[0];
                                }
                                return numArray;
                            }
                        }
                        continue;
                    }
                    else
                    {
                        list.Sort();
                        return list.ToArray();
                    }
                    break;
                }
            }
            return numArray;
        }
    }
}

