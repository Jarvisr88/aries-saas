namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("61c2402f-0ed0-5a18-ab69-59f4aa99a368"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITypedEventHandler_IToastNotification_Dismissed
    {
        [PreserveSig]
        int Invoke(IToastNotification sender, IToastDismissedEventArgs args);
    }
}

