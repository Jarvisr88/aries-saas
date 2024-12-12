namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;

    internal class GridDragDataTransferEventFactory : IDragEventFactory
    {
        private readonly DataViewBase view;

        public GridDragDataTransferEventFactory(DataViewBase view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
        }

        public CompleteRecordDragDropEventArgs CreateCustomCompleteDragDrop() => 
            new CompleteRecordDragDropEventArgs(DataViewBase.CompleteRecordDragDropEvent, this.view);

        public DragRecordOverEventArgs CreateCustomDragOver() => 
            new DragRecordOverEventArgs(DataViewBase.DragRecordOverEvent, this.view);

        public DropRecordEventArgs CreateCustomDrop() => 
            new DropRecordEventArgs(DataViewBase.DropRecordEvent, this.view);

        public GiveRecordDragFeedbackEventArgs CreateCustomGiveFeedback() => 
            new GiveRecordDragFeedbackEventArgs(DataViewBase.GiveRecordDragFeedbackEvent, this.view);

        public ContinueRecordDragEventArgs CreateCustomQueryContinueDrag() => 
            new ContinueRecordDragEventArgs(DataViewBase.ContinueRecordDragEvent, this.view);

        public StartRecordDragEventArgs CreateCustomStartDrag() => 
            new StartRecordDragEventArgs(DataViewBase.StartRecordDragEvent, this.view);
    }
}

