namespace DevExpress.XtraReports.Native.Navigation
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public abstract class DocumentMapBuilderBase
    {
        private readonly PageList pages;
        private readonly IBookmarkNodeCollection bookmarkNodes;
        private readonly BookmarkNode rootBookmark;
        private readonly bool bookmarkDuplicateSuppress;
        private readonly Dictionary<object, List<BookmarkNode>> childNodes = new Dictionary<object, List<BookmarkNode>>();
        private readonly Dictionary<object, BookmarkNode> nodes = new Dictionary<object, BookmarkNode>();

        public DocumentMapBuilderBase(PageList pages, IBookmarkNodeCollection bookmarkNodes, BookmarkNode rootBookmark, bool bookmarkDuplicateSuppress)
        {
            this.pages = pages;
            this.bookmarkNodes = bookmarkNodes;
            this.rootBookmark = rootBookmark;
            this.bookmarkDuplicateSuppress = bookmarkDuplicateSuppress;
        }

        protected void BuildBookmarkNodes(BrickPagePairCollection bpPairs)
        {
            bpPairs.Sort(new BookmarkBrickPagePairComparer(this.pages, new Func<BrickPagePair, VisualBrick>(this.GetVisualBrick)));
            for (int i = 0; i < bpPairs.Count; i++)
            {
                BrickPagePair brickPagePair = bpPairs[i];
                VisualBrick visualBrick = this.GetVisualBrick(brickPagePair);
                if (visualBrick != null)
                {
                    BookmarkNode item = this.CreateBookmarkNode(visualBrick.BookmarkInfo.Bookmark, brickPagePair);
                    this.nodes[this.GetVisualBrickKey(visualBrick)] = item;
                    if (visualBrick.BookmarkInfo.ParentBrick == null)
                    {
                        this.bookmarkNodes.Add(item);
                    }
                    else
                    {
                        List<BookmarkNode> list;
                        if (!this.childNodes.TryGetValue(this.GetVisualBrickKey(visualBrick.BookmarkInfo.ParentBrick), out list))
                        {
                            list = new List<BookmarkNode>();
                            this.childNodes[this.GetVisualBrickKey(visualBrick.BookmarkInfo.ParentBrick)] = list;
                        }
                        list.Add(item);
                    }
                }
            }
        }

        protected void ClearNodes()
        {
            this.childNodes.Clear();
            this.nodes.Clear();
        }

        protected abstract BookmarkNode CreateBookmarkNode(string bookmark, BrickPagePair brickPagePair);
        public void FinalizeBuild()
        {
            foreach (KeyValuePair<object, List<BookmarkNode>> pair in this.childNodes)
            {
                BookmarkNode node;
                if (this.nodes.TryGetValue(pair.Key, out node))
                {
                    foreach (BookmarkNode node2 in pair.Value)
                    {
                        node.Nodes.Add(node2);
                    }
                }
            }
            if (this.bookmarkDuplicateSuppress)
            {
                this.SuppressDuplicate(this.rootBookmark);
            }
        }

        protected abstract VisualBrick GetVisualBrick(BrickPagePair brickPagePair);
        protected virtual object GetVisualBrickKey(VisualBrick visualBrick) => 
            visualBrick;

        private int IsNodeIncluded(List<BookmarkNode> bookmarkCollection, BookmarkNode bookmarkNode)
        {
            int index;
            using (List<BookmarkNode>.Enumerator enumerator = bookmarkCollection.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BookmarkNode current = enumerator.Current;
                        if (current.Text != bookmarkNode.Text)
                        {
                            continue;
                        }
                        index = bookmarkCollection.IndexOf(current);
                    }
                    else
                    {
                        return -1;
                    }
                    break;
                }
            }
            return index;
        }

        private void SuppressDuplicate(BookmarkNode rootBookmark)
        {
            List<BookmarkNode> bookmarkCollection = new List<BookmarkNode>();
            foreach (BookmarkNode node in rootBookmark.Nodes)
            {
                int num = this.IsNodeIncluded(bookmarkCollection, node);
                if (num == -1)
                {
                    bookmarkCollection.Add(node);
                    continue;
                }
                foreach (BookmarkNode node2 in node.Nodes)
                {
                    bookmarkCollection[num].Nodes.Add(node2);
                }
            }
            for (int i = rootBookmark.Nodes.Count - 1; i >= 0; i--)
            {
                rootBookmark.Nodes.RemoveAt(i);
            }
            foreach (BookmarkNode node3 in bookmarkCollection)
            {
                this.SuppressDuplicate(node3);
                rootBookmark.Nodes.Add(node3);
            }
        }
    }
}

