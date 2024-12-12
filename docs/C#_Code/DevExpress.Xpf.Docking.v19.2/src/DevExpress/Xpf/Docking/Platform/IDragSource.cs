namespace DevExpress.Xpf.Docking.Platform
{
    using System;

    public interface IDragSource
    {
        bool AcceptDockTarget(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo);
        bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo);
    }
}

