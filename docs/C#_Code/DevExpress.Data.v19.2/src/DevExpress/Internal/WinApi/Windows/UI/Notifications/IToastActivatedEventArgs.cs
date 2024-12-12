namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("e3bf92f3-c197-436f-8265-0625824f8dac"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IToastActivatedEventArgs : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        HResult GetArguments([MarshalAs(UnmanagedType.HString)] out string args);
    }
}

