namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class ItemContextMenu : ItemContextMenuBase
    {
        public ItemContextMenu(DockLayoutManager container) : base(container)
        {
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget) => 
            new ItemContextMenuInfo(this, base.Item);

        public ItemContextMenuInfo ItemMenuInfo =>
            base.MenuInfo as ItemContextMenuInfo;
    }
}

