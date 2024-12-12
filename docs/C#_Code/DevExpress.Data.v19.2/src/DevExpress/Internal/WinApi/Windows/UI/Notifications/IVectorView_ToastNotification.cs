namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("ba0aff1f-6a8a-5a7e-a9f7-505b6266a436"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IVectorView_ToastNotification
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        HResult GetAt(uint index, out IToastNotification value);
        uint Size { get; }
        HResult IndexOf(IToastNotification value, out uint index, out bool found);
        HResult GetMany();
    }
}

