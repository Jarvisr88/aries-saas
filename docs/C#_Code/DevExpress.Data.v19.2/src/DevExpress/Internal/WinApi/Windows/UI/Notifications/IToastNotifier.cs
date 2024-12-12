namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("75927B93-03F3-41EC-91D3-6E5BAC1B38E7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IToastNotifier : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        int Show(IToastNotification notification);
        void Hide(IToastNotification notification);
        NotificationSetting Setting { get; }
        int AddToSchedule(IToastNotification scheduledToast);
        int RemoveFromSchedule(IToastNotification scheduledToast);
        HResult GetScheduledToastNotifications(out IVectorView_ToastNotification toasts);
    }
}

