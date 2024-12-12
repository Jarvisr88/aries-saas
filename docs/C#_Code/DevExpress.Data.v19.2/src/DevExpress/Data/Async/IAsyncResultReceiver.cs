namespace DevExpress.Data.Async
{
    using System;

    public interface IAsyncResultReceiver : IAsyncCommandVisitor
    {
        void BusyChanged(bool busy);
        void Notification(NotificationExceptionThrown exception);
        void Notification(NotificationInconsistencyDetected notification);
        void PropertyDescriptorsRenewed();
        void Refreshing(CommandRefresh refreshCommand);
    }
}

