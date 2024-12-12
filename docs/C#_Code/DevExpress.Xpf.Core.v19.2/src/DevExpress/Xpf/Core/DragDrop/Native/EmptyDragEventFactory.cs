namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;

    internal class EmptyDragEventFactory : IDragEventFactory
    {
        public CompleteRecordDragDropEventArgs CreateCustomCompleteDragDrop() => 
            null;

        public DragRecordOverEventArgs CreateCustomDragOver() => 
            null;

        public DropRecordEventArgs CreateCustomDrop() => 
            null;

        public GiveRecordDragFeedbackEventArgs CreateCustomGiveFeedback() => 
            null;

        public ContinueRecordDragEventArgs CreateCustomQueryContinueDrag() => 
            null;

        public StartRecordDragEventArgs CreateCustomStartDrag() => 
            null;
    }
}

