namespace DevExpress.XtraPrinting
{
    using System;

    public class RootBookmarkNode : BookmarkNode
    {
        public RootBookmarkNode()
        {
        }

        public RootBookmarkNode(string text) : base(text, null, null)
        {
        }

        protected internal override int GetPageRangeIndex(int[] indices) => 
            (indices.Length != 0) ? 0 : -1;
    }
}

