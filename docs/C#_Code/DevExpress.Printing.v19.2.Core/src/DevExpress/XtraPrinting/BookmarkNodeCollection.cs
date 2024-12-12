namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;

    public class BookmarkNodeCollection : Collection<BookmarkNode>, IBookmarkNodeCollection, IList, ICollection, IEnumerable
    {
        protected override void ClearItems()
        {
            base.ClearItems();
            foreach (BookmarkNode node in this)
            {
                node.Nodes.Clear();
            }
        }
    }
}

