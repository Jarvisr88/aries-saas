namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("ab54de2d-97d9-5528-b6ad-105afe156530"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITypedEventHandler_IToastNotification_Activated
    {
        [PreserveSig]
        int Invoke(IToastNotification sender, IInspectable args);
    }
}

