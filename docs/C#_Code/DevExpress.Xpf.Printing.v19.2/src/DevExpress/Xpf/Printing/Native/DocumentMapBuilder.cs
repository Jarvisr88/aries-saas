namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public class DocumentMapBuilder
    {
        private Dictionary<BookmarkInfo, BookmarkNode> bookmarkNodesHT;

        public void Build(Document document)
        {
            document.Bookmark = document.Name;
            this.bookmarkNodesHT = new Dictionary<BookmarkInfo, BookmarkNode>();
            foreach (Page page in document.Pages)
            {
                Page[] bricks = new Page[] { page };
                NestedBrickIterator iterator = new NestedBrickIterator(bricks);
                while (iterator.MoveNext())
                {
                    VisualBrick currentBrick = iterator.CurrentBrick as VisualBrick;
                    if (currentBrick != null)
                    {
                        BookmarkInfo bookmarkInfo = currentBrick.BookmarkInfo;
                        if ((bookmarkInfo != null) && bookmarkInfo.HasBookmark)
                        {
                            BookmarkNode item = this.GetBookmarkNode(bookmarkInfo, currentBrick, page);
                            if (bookmarkInfo.ParentInfo != null)
                            {
                                this.GetBookmarkNode(bookmarkInfo.ParentInfo, null, null).Nodes.Add(item);
                                continue;
                            }
                            document.BookmarkNodes.Add(item);
                        }
                    }
                }
            }
            this.bookmarkNodesHT.Clear();
        }

        private BookmarkNode GetBookmarkNode(BookmarkInfo bookmarkInfo, VisualBrick brick, Page page)
        {
            BookmarkNode node;
            if (!this.bookmarkNodesHT.TryGetValue(bookmarkInfo, out node))
            {
                node = new BookmarkNode(bookmarkInfo.Bookmark, brick, page);
                this.bookmarkNodesHT[bookmarkInfo] = node;
            }
            return node;
        }
    }
}

