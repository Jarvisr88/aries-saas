namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("53E31837-6600-4A81-9395-75CFFE746F94"), ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), CLSCompliant(false)]
    public interface INotificationActivationCallback
    {
        void Activate([In, MarshalAs(UnmanagedType.LPWStr)] string appUserModelId, [In, MarshalAs(UnmanagedType.LPWStr)] string invokedArgs, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=3)] NOTIFICATION_USER_INPUT_DATA[] data, [In, MarshalAs(UnmanagedType.U4)] uint count);
    }
}

