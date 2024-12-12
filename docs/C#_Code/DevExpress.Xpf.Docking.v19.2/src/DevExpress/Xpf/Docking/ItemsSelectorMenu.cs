namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ItemsSelectorMenu : BaseLayoutElementMenu
    {
        public ItemsSelectorMenu(DockLayoutManager container) : base(container)
        {
        }

        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget) => 
            new ItemsSelectorMenuInfo(this, this.Items);

        protected override void OnClosed(EventArgs e)
        {
            this.Items = null;
            base.OnClosed(e);
        }

        public virtual void Show(UIElement source, BaseLayoutItem[] items)
        {
            this.Items = items;
            this.ShowPopup(source);
        }

        public BaseLayoutItem[] Items { get; private set; }

        public ItemsSelectorMenuInfo ItemsMenuInfo =>
            base.MenuInfo as ItemsSelectorMenuInfo;
    }
}

