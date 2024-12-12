namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    internal class BookmarkBrickPagePairComparer : BrickPagePairComparer
    {
        public BookmarkBrickPagePairComparer(IPageRepository pages, Func<BrickPagePair, VisualBrick> getBrick) : base(pages)
        {
            this.<GetBrick>k__BackingField = getBrick;
        }

        protected override int Compare(BrickPagePair xPair, BrickPagePair yPair)
        {
            Brick brick = this.GetBrick(xPair);
            Brick brick2 = this.GetBrick(yPair);
            if ((brick != null) && (brick2 != null))
            {
                int num = brick.SafeGetAttachedValue<int>(BrickAttachedProperties.ColumnIndex, -1);
                int num2 = brick2.SafeGetAttachedValue<int>(BrickAttachedProperties.ColumnIndex, -1);
                if ((num > -1) && ((num2 > -1) && (num != num2)))
                {
                    return num.CompareTo(num2);
                }
            }
            return base.Compare(xPair, yPair);
        }

        private Func<BrickPagePair, VisualBrick> GetBrick { get; }
    }
}

