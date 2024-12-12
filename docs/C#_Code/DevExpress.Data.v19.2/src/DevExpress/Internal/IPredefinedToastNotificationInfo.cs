namespace DevExpress.Internal
{
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using System;

    public interface IPredefinedToastNotificationInfo
    {
        DevExpress.Internal.WinApi.Windows.UI.Notifications.ToastTemplateType ToastTemplateType { get; }

        string[] Lines { get; }

        string ImagePath { get; }

        NotificationDuration Duration { get; }

        PredefinedSound Sound { get; }
    }
}

