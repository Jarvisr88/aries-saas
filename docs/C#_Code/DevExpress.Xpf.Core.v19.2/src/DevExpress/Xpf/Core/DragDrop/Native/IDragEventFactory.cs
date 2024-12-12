namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;

    public interface IDragEventFactory
    {
        CompleteRecordDragDropEventArgs CreateCustomCompleteDragDrop();
        DragRecordOverEventArgs CreateCustomDragOver();
        DropRecordEventArgs CreateCustomDrop();
        GiveRecordDragFeedbackEventArgs CreateCustomGiveFeedback();
        ContinueRecordDragEventArgs CreateCustomQueryContinueDrag();
        StartRecordDragEventArgs CreateCustomStartDrag();
    }
}

