namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BrickPagePairComparer : IComparer
    {
        public BrickPagePairComparer(IPageRepository pages)
        {
            this.<Pages>k__BackingField = pages;
        }

        protected virtual int Compare(BrickPagePair xPair, BrickPagePair yPair)
        {
            int num = xPair.PageIndex.CompareTo(yPair.PageIndex);
            RectangleF empty = RectangleF.Empty;
            RectangleF brickBounds = RectangleF.Empty;
            if (num == 0)
            {
                empty = xPair.GetBrickBounds(this.Pages);
                brickBounds = yPair.GetBrickBounds(this.Pages);
                num = Convert.ToInt32(empty.Y).CompareTo(Convert.ToInt32(brickBounds.Y));
            }
            if (num == 0)
            {
                num = Convert.ToInt32(empty.X).CompareTo(Convert.ToInt32(brickBounds.X));
            }
            return num;
        }

        public int Compare(object x, object y)
        {
            BrickPagePair xPair = x as BrickPagePair;
            return this.Compare(xPair, y as BrickPagePair);
        }

        protected IPageRepository Pages { get; }
    }
}

