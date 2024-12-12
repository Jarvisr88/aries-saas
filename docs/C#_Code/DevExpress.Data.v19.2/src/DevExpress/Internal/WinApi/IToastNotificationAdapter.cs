namespace DevExpress.Internal.WinApi
{
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;

    [CLSCompliant(false)]
    public interface IToastNotificationAdapter
    {
        IToastNotification Create(IPredefinedToastNotificationInfo info);
        void Hide(IToastNotification notification);
        void Show(IToastNotification notification);
    }
}

