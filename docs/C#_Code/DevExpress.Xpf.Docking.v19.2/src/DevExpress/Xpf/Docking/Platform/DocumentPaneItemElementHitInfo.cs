namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class DocumentPaneItemElementHitInfo : DockLayoutElementHitInfo
    {
        public DocumentPaneItemElementHitInfo(Point point, DocumentPaneItemElement element) : base(point, element)
        {
        }

        protected override bool HitTestBounds(object element, Point pt, Rect bounds)
        {
            Thickness dragOffset;
            DocumentPaneItemElement element2 = element as DocumentPaneItemElement;
            if (element2 != null)
            {
                dragOffset = element2.DragOffset;
            }
            else
            {
                dragOffset = new Thickness();
            }
            Thickness padding = dragOffset;
            RectHelper.Inflate(ref bounds, padding);
            return base.HitTestBounds(element, pt, bounds);
        }

        public override bool InDragBounds =>
            false;

        public override bool InReorderingBounds =>
            base.InBounds;
    }
}

