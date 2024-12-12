namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class MDIDocumentElementHitInfo : DockLayoutElementHitInfo
    {
        public MDIDocumentElementHitInfo(Point point, MDIDocumentElement element) : base(point, element)
        {
        }

        public override bool InReorderingBounds =>
            base.IsDragging ? base.InBounds : base.InHeader;
    }
}

