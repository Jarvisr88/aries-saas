namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class ItemContextMenuBase : BaseLayoutElementMenu
    {
        public ItemContextMenuBase(DockLayoutManager container) : base(container)
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Item = null;
            base.OnClosed(e);
        }

        public virtual void Show(BaseLayoutItem item, UIElement placementTarget = null)
        {
            if (item != null)
            {
                this.Item = item;
                placementTarget ??= item;
                this.ShowPopup(placementTarget);
            }
        }

        public BaseLayoutItem Item { get; private set; }
    }
}

