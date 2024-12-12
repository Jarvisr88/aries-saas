namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    internal sealed class EmptyDragDropInfoFactory : IDragDropInfoFactory
    {
        public DragInfo CreateDragInfo(object sender) => 
            null;

        public DropInfo CreateDropInfo(object sender, DragInfo activeDragInfo) => 
            null;
    }
}

