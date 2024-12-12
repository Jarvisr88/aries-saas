namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IDragDataTransferListener
    {
        void OnGiveFeedback(object sender, IGiveFeedbackEventArgs args);
        void OnQueryContinueDrag(object sender, IQueryContinueDragEventArgs args);
    }
}

