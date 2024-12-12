namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal;
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("3F89D935-D9CB-4538-A0F0-FFE7659938F8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IToastDismissedEventArgs : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        ToastDismissalReason Reason { get; }
    }
}

