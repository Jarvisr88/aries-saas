namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using System;

    public class AsyncNotifyBusyChangedResultReceiver : IAsyncResultReceiver, IAsyncCommandVisitor
    {
        private IAsyncResultReceiverBusyChangedListener busyChangedListener;

        public AsyncNotifyBusyChangedResultReceiver(IAsyncResultReceiverBusyChangedListener busyChangedListener)
        {
            this.busyChangedListener = busyChangedListener;
        }

        public void BusyChanged(bool busy)
        {
            this.busyChangedListener.ProcessBusyChanged(busy);
        }

        public void Canceled(Command command)
        {
        }

        public void Notification(NotificationExceptionThrown exception)
        {
        }

        public void Notification(NotificationInconsistencyDetected notification)
        {
        }

        public void PropertyDescriptorsRenewed()
        {
        }

        public void Refreshing(CommandRefresh refreshCommand)
        {
        }

        public void Visit(CommandApply command)
        {
        }

        public void Visit(CommandFindIncremental command)
        {
        }

        public void Visit(CommandGetAllFilteredAndSortedRows command)
        {
        }

        public void Visit(CommandGetGroupInfo command)
        {
        }

        public void Visit(CommandGetRow command)
        {
        }

        public void Visit(CommandGetRowIndexByKey command)
        {
        }

        public void Visit(CommandGetTotals command)
        {
        }

        public void Visit(CommandGetUniqueColumnValues command)
        {
        }

        public void Visit(CommandLocateByValue command)
        {
        }

        public void Visit(CommandPrefetchRows command)
        {
        }

        public void Visit(CommandRefresh command)
        {
        }
    }
}

