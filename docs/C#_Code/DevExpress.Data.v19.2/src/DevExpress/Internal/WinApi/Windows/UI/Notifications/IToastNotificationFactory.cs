namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using DevExpress.Internal.WinApi;
    using DevExpress.Internal.WinApi.Window.Data.Xml.Dom;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("04124B20-82C6-4229-B109-FD9ED4662B53"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IToastNotificationFactory : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        int CreateToastNotification(IXmlDocument content, out IToastNotification notification);
    }
}

