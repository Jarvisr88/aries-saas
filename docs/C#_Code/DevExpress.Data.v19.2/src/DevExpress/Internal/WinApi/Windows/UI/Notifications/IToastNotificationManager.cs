namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using DevExpress.Internal.WinApi.Window.Data.Xml.Dom;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("50AC103F-D235-4598-BBEF-98FE4D1A3AD4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IToastNotificationManager : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        void f6();
        int CreateToastNotifierWithId([In, MarshalAs(UnmanagedType.HString)] string applicationId, out IToastNotifier notifier);
        IXmlDocument GetTemplateContent(ToastTemplateType type);
    }
}

