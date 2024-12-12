namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IDragDropInfoFactory
    {
        DragInfo CreateDragInfo(object sender);
        DropInfo CreateDropInfo(object sender, DragInfo activeDragInfo);
    }
}

