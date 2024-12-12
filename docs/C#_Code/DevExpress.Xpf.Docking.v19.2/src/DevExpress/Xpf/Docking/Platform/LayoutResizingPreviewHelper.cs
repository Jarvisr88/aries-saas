namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    internal class LayoutResizingPreviewHelper : BaseResizingPreviewHelper
    {
        private LayoutGroup BackgroundGroup;

        public LayoutResizingPreviewHelper(LayoutView view, LayoutGroup backgroundGroup) : base(view)
        {
            this.BackgroundGroup = backgroundGroup;
        }

        protected override Rect GetAdornerBounds(Point change)
        {
            Rect initialBounds = base.InitialBounds;
            RectHelper.Offset(ref initialBounds, change.X, change.Y);
            return initialBounds;
        }

        protected override Rect GetInitialAdornerBounds() => 
            base.GetItemBounds(base.Element);

        protected override UIElement GetUIElement() => 
            new ShadowResizePointer();

        protected override void OnInitResizing()
        {
            base.OnInitResizing();
            Rect itemBounds = base.GetItemBounds(base.Owner.GetViewElement(this.BackgroundGroup));
            base.View.ResizingWindowHelper.UpdateBackground(itemBounds);
        }

        protected override DevExpress.Xpf.Core.FloatingMode Mode =>
            DevExpress.Xpf.Core.FloatingMode.Adorner;
    }
}

