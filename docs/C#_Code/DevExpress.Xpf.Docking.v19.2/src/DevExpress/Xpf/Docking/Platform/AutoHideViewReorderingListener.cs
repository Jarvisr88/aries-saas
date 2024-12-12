namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class AutoHideViewReorderingListener : LayoutViewReorderingListener
    {
        public override bool CanDrag(Point point, ILayoutElement element) => 
            new DockLayoutElementDragInfo(base.View, point, element).AcceptHide();

        public override bool CanDrop(Point point, ILayoutElement element) => 
            new DockLayoutElementDragInfo(base.View, point, element).AcceptHide();
    }
}

