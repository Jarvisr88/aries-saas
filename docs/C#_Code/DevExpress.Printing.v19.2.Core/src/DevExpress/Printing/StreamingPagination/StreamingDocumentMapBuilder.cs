namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Native.Navigation;
    using System;

    public class StreamingDocumentMapBuilder : DocumentMapBuilderBase
    {
        private Page currentPage;

        public StreamingDocumentMapBuilder(IStreamingDocument document) : base(null, document.BookmarkNodes, document.RootBookmark, document.BookmarkDuplicateSuppress)
        {
        }

        public void Build(BrickPagePairCollection bpPairs, Page page)
        {
            this.currentPage = page;
            base.BuildBookmarkNodes(bpPairs);
        }

        protected override BookmarkNode CreateBookmarkNode(string bookmark, BrickPagePair brickPagePair) => 
            new StreamingBookmarkNode(bookmark, brickPagePair);

        protected override VisualBrick GetVisualBrick(BrickPagePair brickPagePair) => 
            (brickPagePair.PageIndex != this.currentPage.Index) ? null : (this.currentPage.GetBrickByIndices(brickPagePair.BrickIndices) as VisualBrick);
    }
}

