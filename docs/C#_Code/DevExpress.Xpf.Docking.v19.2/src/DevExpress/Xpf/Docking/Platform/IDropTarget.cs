namespace DevExpress.Xpf.Docking.Platform
{
    using System;

    public interface IDropTarget
    {
        bool AcceptDockSource(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo);
        bool AcceptDragSource(DockLayoutElementDragInfo dragInfo);
        bool AcceptFill(DockLayoutElementDragInfo dragInfo);
        bool AcceptReordering(DockLayoutElementDragInfo info);
    }
}

