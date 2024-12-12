namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DragDataTransferService
    {
        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            this.Listener.Do<IDragDataTransferListener>(x => x.OnGiveFeedback(sender, new IndependentGiveFeedbackEventArgs(e)));
        }

        private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            this.Listener.Do<IDragDataTransferListener>(x => x.OnQueryContinueDrag(sender, new IndependentQueryContinueDragEventArgs(e)));
        }

        public virtual void Transfer()
        {
            DragDropEffects? allowedEffects = this.AllowedEffects;
            DragDropEffects effects = (allowedEffects != null) ? allowedEffects.GetValueOrDefault() : (DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll);
            ContentControl dragSource = new ContentControl();
            dragSource.QueryContinueDrag += new QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            dragSource.GiveFeedback += new GiveFeedbackEventHandler(this.OnGiveFeedback);
            DragDropEffects effects2 = DragDrop.DoDragDrop(dragSource, this.DataObject, effects);
            dragSource.QueryContinueDrag -= new QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            dragSource.GiveFeedback -= new GiveFeedbackEventHandler(this.OnGiveFeedback);
            this.DataObject = null;
            allowedEffects = null;
            this.AllowedEffects = allowedEffects;
            if (this.CallBack != null)
            {
                this.CallBack(effects2);
            }
        }

        public IDataObject DataObject { get; set; }

        public Action<DragDropEffects> CallBack { get; set; }

        public IDragDataTransferListener Listener { get; set; }

        public DragDropEffects? AllowedEffects { get; set; }
    }
}

