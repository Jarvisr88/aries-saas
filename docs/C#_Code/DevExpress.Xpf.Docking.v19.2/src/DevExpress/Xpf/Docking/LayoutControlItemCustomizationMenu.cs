namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutControlItemCustomizationMenu : BaseLayoutElementMenu
    {
        public LayoutControlItemCustomizationMenu(DockLayoutManager container) : base(container)
        {
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget) => 
            new DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenuInfo(this, this.Items);

        protected override void OnClosed(EventArgs e)
        {
            this.Items = null;
            base.OnClosed(e);
        }

        public virtual void Show(UIElement source, BaseLayoutItem[] Items)
        {
            this.Items = Items;
            this.ShowPopup(source);
        }

        public BaseLayoutItem[] Items { get; private set; }

        public DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenuInfo LayoutControlItemCustomizationMenuInfo =>
            base.MenuInfo as DevExpress.Xpf.Docking.LayoutControlItemCustomizationMenuInfo;
    }
}

