namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;

    public class BarItemMenuHeaderCollectionNode<TElement> : BarItemLinkInfoCollectionNode<TElement> where TElement: class, IBarItemLinkInfo
    {
        public BarItemMenuHeaderCollectionNode(BarItemLinkMenuHeader root, BarItemLinkInfoCollectionNode<TElement> parent, BaseBarItemLinkInfoFactory<TElement> factory, bool allowRecycling);
        protected override bool GetShouldCreateLinkInfo();
        protected override void OnItemChanged(object sender, ValueChangedEventArgs<BarItem> e);
    }
}

