namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;

    [DebuggerDisplay(@"\{{GetType().FullName,nq}, PageIndex = {PageIndex}, Indices = {Indices}}")]
    public class BrickPagePair : BrickPageInfo
    {
        private static BrickPagePair empty = new BrickPagePair(BrickPagePairHelper.EmptyIndices, -1, -1L);

        public BrickPagePair()
        {
        }

        protected BrickPagePair(int[] brickIndices, int pageIndex, long pageID) : this(brickIndices, pageIndex, pageID, RectangleF.Empty)
        {
        }

        protected BrickPagePair(int[] brickIndices, int pageIndex, long pageID, RectangleF brickBounds) : base(brickIndices, pageIndex, pageID, brickBounds)
        {
        }

        private static bool ArrayAreEqual(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static BrickPagePair Create(DevExpress.XtraPrinting.Brick brick, DevExpress.XtraPrinting.Page page) => 
            Create(page.GetIndicesByBrick(brick), page);

        public static BrickPagePair Create(int[] indices, DevExpress.XtraPrinting.Page page) => 
            Create(indices, page, RectangleF.Empty);

        public static BrickPagePair Create(int[] indices, DevExpress.XtraPrinting.Page page, RectangleF brickBounds) => 
            (page != null) ? new BrickPagePair(indices, page.Index, page.ID, brickBounds) : Empty;

        public static BrickPagePair Create(int[] indices, int pageIndex, long pageID, RectangleF brickBounds) => 
            (pageIndex != -1) ? new BrickPagePair(indices, pageIndex, pageID, brickBounds) : Empty;

        public override bool Equals(object obj)
        {
            BrickPagePair pair = obj as BrickPagePair;
            return ((pair != null) && (ArrayAreEqual(base.BrickIndices, pair.BrickIndices) && Equals(this.PageIndex, pair.PageIndex)));
        }

        public virtual RectangleF GetBrickBounds(IPageRepository pages) => 
            base.GetBoundsCore(pages);

        public override int GetHashCode()
        {
            int[] destinationArray = new int[base.BrickIndices.Length + 1];
            Array.Copy(base.BrickIndices, destinationArray, (int) (destinationArray.Length - 1));
            destinationArray[destinationArray.Length - 1] = this.PageIndex;
            return HashCodeHelper.CalculateInt32List(destinationArray);
        }

        public virtual DevExpress.XtraPrinting.Page GetPage(IPageRepository pages) => 
            base.GetPageCore(pages);

        public void UpdatePageIndex(IPageRepository pages)
        {
            base.GetPageCore(pages);
        }

        [Description("Gets an empty brick-page pair.")]
        public static BrickPagePair Empty =>
            empty;

        internal string Indices =>
            BrickPagePairHelper.IndicesFromArray(base.BrickIndices);

        [Obsolete("Use the Page.GetBrickByIndices method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.XtraPrinting.Brick Brick
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        [Obsolete("Use the PageIndex property instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.XtraPrinting.Page Page
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}

