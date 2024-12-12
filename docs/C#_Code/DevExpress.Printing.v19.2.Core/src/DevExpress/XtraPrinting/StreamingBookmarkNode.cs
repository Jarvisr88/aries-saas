namespace DevExpress.XtraPrinting
{
    using System;

    public class StreamingBookmarkNode : BookmarkNode
    {
        public StreamingBookmarkNode(string text, BrickPagePair bpPair) : base(text, bpPair)
        {
        }

        internal override bool IsValid(Document document) => 
            true;
    }
}

