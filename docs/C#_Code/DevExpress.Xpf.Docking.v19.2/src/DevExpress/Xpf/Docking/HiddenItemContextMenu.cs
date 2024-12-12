namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class HiddenItemContextMenu : ItemContextMenuBase
    {
        public HiddenItemContextMenu(DockLayoutManager container) : base(container)
        {
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget) => 
            new DevExpress.Xpf.Docking.HiddenItemContextMenuInfo(this, base.Item);

        public DevExpress.Xpf.Docking.HiddenItemContextMenuInfo HiddenItemContextMenuInfo =>
            base.MenuInfo as DevExpress.Xpf.Docking.HiddenItemContextMenuInfo;
    }
}

