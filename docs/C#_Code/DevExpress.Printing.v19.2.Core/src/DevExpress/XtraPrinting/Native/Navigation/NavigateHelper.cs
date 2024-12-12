namespace DevExpress.XtraPrinting.Native.Navigation
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class NavigateHelper
    {
        protected virtual bool CanIterateBrick(NestedBrickIterator iterator);
        protected virtual void PreprocessBrick(NestedBrickIterator iterator, IPageItem pageItem);
        public static BrickPagePairCollection SelectBrickPagePairs(Document documnent, BrickSelector selector, IComparer sortComparer);
        public BrickPagePairCollection SelectBrickPagePairs(ICollection<Page> pages, BrickSelector selector, IComparer sortComparer);
    }
}

