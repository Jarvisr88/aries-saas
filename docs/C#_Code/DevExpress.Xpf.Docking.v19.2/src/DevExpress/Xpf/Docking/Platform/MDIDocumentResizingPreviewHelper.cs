namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    internal class MDIDocumentResizingPreviewHelper : BaseResizingPreviewHelper
    {
        public MDIDocumentResizingPreviewHelper(LayoutView view) : base(view)
        {
        }

        protected override Rect GetAdornerBounds(Point change) => 
            DevExpress.Xpf.Layout.Core.ResizeHelper.CalcResizing(base.InitialBounds, base.StartPoint, change, base.MinSize, base.MaxSize);

        protected override Rect GetInitialAdornerBounds() => 
            base.GetItemBounds(base.Element);

        protected override UIElement GetUIElement() => 
            new FloatingResizePointer();

        protected override FloatingMode Mode =>
            FloatingMode.Adorner;
    }
}

