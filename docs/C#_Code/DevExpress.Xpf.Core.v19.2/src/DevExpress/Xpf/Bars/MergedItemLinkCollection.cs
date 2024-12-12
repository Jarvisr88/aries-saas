namespace DevExpress.Xpf.Bars
{
    using System;

    public class MergedItemLinkCollection : BarItemLinkCollection
    {
        public MergedItemLinkCollection(ILinksHolder holder);

        protected override bool EnableLinkCollectionLogic { get; }
    }
}

