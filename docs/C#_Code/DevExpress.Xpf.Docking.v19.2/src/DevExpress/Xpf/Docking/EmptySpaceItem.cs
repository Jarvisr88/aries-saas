namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;

    public class EmptySpaceItem : FixedItem
    {
        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.EmptySpaceItem;
    }
}

