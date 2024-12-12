namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DockLayoutElementHitInfo : LayoutElementHitInfo
    {
        public DockLayoutElementHitInfo(Point point, BaseLayoutElement element) : base(point, element)
        {
        }

        public bool InCloseButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.CloseButton);

        public bool InPinButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.PinButton);

        public bool InExpandButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.ExpandButton);

        public bool InMaximizeButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.MaximizeButton);

        public bool InMinimizeButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.MinimizeButton);

        public bool InRestoreButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.RestoreButton);

        public bool InDropDownButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.DropDownButton);

        public bool InScrollPrevButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.ScrollPrevButton);

        public bool InScrollNextButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.ScrollNextButton);

        public bool InPageHeaders =>
            Equals(this.HitResult, HitTestType.PageHeaders);

        public bool InHideButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.HideButton);

        public bool InCollapseButton =>
            base.InControlBox && Equals(this.HitResult, HitTestType.CollapseButton);

        protected bool IsCustomization
        {
            get
            {
                DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(((IDockLayoutElement) base.Element).View);
                return ((dockLayoutManager != null) ? dockLayoutManager.IsCustomization : false);
            }
        }
    }
}

