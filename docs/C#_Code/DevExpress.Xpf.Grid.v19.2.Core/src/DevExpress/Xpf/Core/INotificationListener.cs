namespace DevExpress.Xpf.Core
{
    using System.Collections.Generic;
    using System.Windows;

    public interface INotificationListener : IWeakEventListener
    {
        IEnumerable<NotificationType> SupportedNotifications { get; }
    }
}

