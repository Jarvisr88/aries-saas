namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    internal sealed class EmptyDragDataTransferListener : IDragDataTransferListener
    {
        public void OnGiveFeedback(object sender, IGiveFeedbackEventArgs args)
        {
        }

        public void OnQueryContinueDrag(object sender, IQueryContinueDragEventArgs args)
        {
        }
    }
}

