namespace DevExpress.Xpf.Bars
{
    using System;

    public class GalleryBarItemLinkControllStrategy : BarItemLinkControlStrategy
    {
        public GalleryBarItemLinkControllStrategy(IBarItemLinkControl instance);
        public override INavigationOwner GetBoundOwner();
        public override bool SetFocus();
    }
}

