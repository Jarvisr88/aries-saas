namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal sealed class EmptyCustomDragService : ICustomDragService
    {
        public CustomCompleteDragResult CustomCompleteDragDrop(DragDropEffects effects, object[] dragObjects, bool canceled) => 
            new CustomCompleteDragResult(false, effects);

        public DropPosition CustomDragOver(DragOverEventSource dragOverEventSource) => 
            dragOverEventSource.Position;

        public CustomDropResult CustomDrop(IDragEventArgs args, DropInfo dropInfo, DropPosition position) => 
            null;

        public CustomStartDragResult CustomStartDrag(DragInfo dragInfo, Lazy<IDataObject> dataObject) => 
            new CustomStartDragResult(dataObject.Value);

        public bool InsideControl { get; set; }

        public IDataObject ActiveDataObject { get; set; }
    }
}

