namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class BrickPageInfo
    {
        internal const int UndefinedPageIndex = -1;
        private RectangleF brickBounds;
        private long pageID;
        private int pageIndex;
        private int[] brickIndices;

        protected BrickPageInfo() : this(BrickPagePairHelper.EmptyIndices, -1, -1L, RectangleF.Empty)
        {
        }

        protected BrickPageInfo(int[] brickIndices, int pageIndex, long pageID, RectangleF brickBounds)
        {
            this.brickIndices = brickIndices;
            this.pageIndex = pageIndex;
            this.pageID = pageID;
            this.brickBounds = brickBounds;
        }

        protected RectangleF GetBoundsCore(IPageRepository pages)
        {
            if (this.brickBounds == RectangleF.Empty)
            {
                Page pageCore = this.GetPageCore(pages);
                if (pageCore != null)
                {
                    this.brickBounds = pageCore.GetBrickBounds(this.BrickIndices);
                }
            }
            return this.brickBounds;
        }

        protected Page GetPageCore(IPageRepository pages)
        {
            Page page;
            if ((this.PageIndex < 0) || (!pages.TryGetPageByIndex(this.PageIndex, out page) || (this.PageID != page.ID)))
            {
                int num;
                if ((this.PageID < 0L) || !pages.TryGetPageByID(this.PageID, out page, out num))
                {
                    return null;
                }
                this.PageIndex = num;
            }
            return page;
        }

        internal void UpdatePageInfo(int[] brickIndices, int pageIndex, long pageID, RectangleF brickBounds)
        {
            this.brickIndices = brickIndices;
            this.pageIndex = pageIndex;
            this.pageID = pageID;
            this.brickBounds = brickBounds;
        }

        [DXHelpExclude(true)]
        public int[] BrickIndices
        {
            get => 
                this.brickIndices;
            protected set => 
                this.brickIndices = value;
        }

        [Description("")]
        public virtual int PageIndex
        {
            get => 
                this.pageIndex;
            protected set => 
                this.pageIndex = value;
        }

        protected internal long PageID
        {
            get => 
                this.pageID;
            set => 
                this.pageID = value;
        }

        internal RectangleF BrickBounds =>
            this.brickBounds;
    }
}

