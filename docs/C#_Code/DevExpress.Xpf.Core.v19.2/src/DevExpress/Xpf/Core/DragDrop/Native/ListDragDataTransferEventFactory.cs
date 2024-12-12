namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;

    internal class ListDragDataTransferEventFactory : IDragEventFactory
    {
        private readonly ListBoxEdit listBox;

        public ListDragDataTransferEventFactory(ListBoxEdit listBox)
        {
            this.listBox = listBox;
        }

        public CompleteRecordDragDropEventArgs CreateCustomCompleteDragDrop() => 
            new CompleteRecordDragDropEventArgs(ListBoxDragDropBehavior.CompleteRecordDragDropEvent, this.listBox);

        public DragRecordOverEventArgs CreateCustomDragOver() => 
            new DragRecordOverEventArgs(ListBoxDragDropBehavior.DragRecordOverEvent, this.listBox);

        public DropRecordEventArgs CreateCustomDrop() => 
            new DropRecordEventArgs(ListBoxDragDropBehavior.DropRecordEvent, this.listBox);

        public GiveRecordDragFeedbackEventArgs CreateCustomGiveFeedback() => 
            new GiveRecordDragFeedbackEventArgs(ListBoxDragDropBehavior.GiveRecordDragFeedbackEvent, this.listBox);

        public ContinueRecordDragEventArgs CreateCustomQueryContinueDrag() => 
            new ContinueRecordDragEventArgs(ListBoxDragDropBehavior.ContinueRecordDragEvent, this.listBox);

        public StartRecordDragEventArgs CreateCustomStartDrag() => 
            new StartRecordDragEventArgs(ListBoxDragDropBehavior.StartRecordDragEvent, this.listBox);
    }
}

