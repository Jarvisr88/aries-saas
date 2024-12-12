namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("35176862-CFD4-44F8-AD64-F500FD896C3B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IToastFailedEventArgs : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        int Error { get; }
    }
}

