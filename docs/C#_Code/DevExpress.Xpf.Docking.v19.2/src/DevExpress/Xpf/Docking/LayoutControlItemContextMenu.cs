namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class LayoutControlItemContextMenu : ItemContextMenuBase
    {
        public LayoutControlItemContextMenu(DockLayoutManager container) : base(container)
        {
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget) => 
            new DevExpress.Xpf.Docking.LayoutControlItemContextMenuInfo(this, base.Item);

        public DevExpress.Xpf.Docking.LayoutControlItemContextMenuInfo LayoutControlItemContextMenuInfo =>
            base.MenuInfo as DevExpress.Xpf.Docking.LayoutControlItemContextMenuInfo;
    }
}

