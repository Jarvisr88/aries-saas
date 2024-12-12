namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("95e3e803-c969-5e3a-9753-ea2ad22a9a33"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITypedEventHandler_IToastNotification_Failed
    {
        [PreserveSig]
        int Invoke(IToastNotification sender, IToastFailedEventArgs args);
    }
}

