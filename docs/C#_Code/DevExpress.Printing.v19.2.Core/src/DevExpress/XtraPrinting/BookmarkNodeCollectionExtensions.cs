namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class BookmarkNodeCollectionExtensions
    {
        private static int GetNodeIndex(IBookmarkNodeCollection collection, long[] prevPages)
        {
            long pageID = -1L;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Pair.PageID != pageID)
                {
                    pageID = collection[i].Pair.PageID;
                    if (Array.IndexOf<long>(prevPages, pageID) < 0)
                    {
                        return i;
                    }
                }
            }
            return collection.Count;
        }

        public static IList<BookmarkNode> GetPageNodes(this IBookmarkNodeCollection collection, Page page)
        {
            List<BookmarkNode> list = new List<BookmarkNode>();
            foreach (BookmarkNode node in collection)
            {
                if (node.Pair.PageID == page.ID)
                {
                    list.Add(node);
                }
            }
            return list;
        }

        public static void InsertNodes(this IBookmarkNodeCollection collection, long[] prevPages, IList<BookmarkNode> nodes)
        {
            int num = (prevPages.Length != 0) ? GetNodeIndex(collection, prevPages) : 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                collection.Insert(num + i, nodes[i]);
            }
        }
    }
}

