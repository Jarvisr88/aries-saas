namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class AutoHideFloatingHelper : FloatingHelper
    {
        public AutoHideFloatingHelper(LayoutView view) : base(view)
        {
        }

        protected override Rect GetItemContainerScreenRect(ILayoutElement element) => 
            this.GetScreenRect(element, base.GetItemScreenRect(element));

        protected override Rect GetItemScreenRect(ILayoutElement element) => 
            this.GetScreenRect(element, base.GetItemScreenRect(element));

        private Rect GetScreenRect(ILayoutElement element, Rect itemScreenRect)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            Point location = itemScreenRect.Location();
            if (base.View.Adapter.DragService.DragItem is AutoHidePaneHeaderItemElement)
            {
                location = new Point(base.View.Adapter.DragService.DragOrigin.X - 15.0, base.View.Adapter.DragService.DragOrigin.Y - 15.0);
            }
            return new Rect(location, item.GetAutoHideRenderSize());
        }
    }
}

