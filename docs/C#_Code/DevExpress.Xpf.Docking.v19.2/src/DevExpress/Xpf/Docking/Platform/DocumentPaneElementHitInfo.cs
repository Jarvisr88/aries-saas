namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class DocumentPaneElementHitInfo : DockLayoutElementHitInfo
    {
        public DocumentPaneElementHitInfo(Point point, DevExpress.Xpf.Docking.Platform.DocumentPaneElement element) : base(point, element)
        {
        }

        private DevExpress.Xpf.Docking.Platform.DocumentPaneElement DocumentPaneElement =>
            (DevExpress.Xpf.Docking.Platform.DocumentPaneElement) base.Element;

        public override bool InDragBounds =>
            false;

        public override bool InReorderingBounds =>
            this.DocumentPaneElement.HasItems && base.InPageHeaders;

        public override bool InMenuBounds =>
            base.InMenuBounds || (this.DocumentPaneElement.IsTabContainer && (!this.DocumentPaneElement.HasItems && base.InBounds));
    }
}

