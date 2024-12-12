namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using DevExpress.Internal.WinApi.Window.Data.Xml.Dom;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("997E2675-059E-4E60-8B06-1760917C8B80"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IToastNotification : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        IXmlDocument Content { get; }
        DateTime ExpirationTime { get; set; }
        [MethodImpl(MethodImplOptions.InternalCall)]
        extern int AddDismissed([In, MarshalAs(UnmanagedType.Interface)] ITypedEventHandler_IToastNotification_Dismissed handler, out EventRegistrationToken token);
        int RemoveDismissed([In] EventRegistrationToken token);
        int AddActivated([In, MarshalAs(UnmanagedType.Interface)] ITypedEventHandler_IToastNotification_Activated handler, out EventRegistrationToken token);
        int RemoveActivated([In] EventRegistrationToken token);
        int AddFailed([In, MarshalAs(UnmanagedType.Interface)] ITypedEventHandler_IToastNotification_Failed handler, out EventRegistrationToken token);
        int RemoveFailed([In] EventRegistrationToken token);
    }
}

