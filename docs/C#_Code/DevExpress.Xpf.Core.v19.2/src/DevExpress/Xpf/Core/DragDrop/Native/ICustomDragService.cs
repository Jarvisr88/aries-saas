namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public interface ICustomDragService
    {
        CustomCompleteDragResult CustomCompleteDragDrop(DragDropEffects effects, object[] dragObjects, bool canceled);
        DropPosition CustomDragOver(DragOverEventSource eventSource);
        CustomDropResult CustomDrop(IDragEventArgs args, DropInfo dropInfo, DropPosition position);
        CustomStartDragResult CustomStartDrag(DragInfo dragInfo, Lazy<IDataObject> dataObject);

        bool InsideControl { get; set; }

        IDataObject ActiveDataObject { get; set; }
    }
}

