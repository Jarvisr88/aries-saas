namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("5CC5B382-E6DD-4991-ABEF-06D8D2E7BD0C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlNodeSerializer : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        int GetXml([MarshalAs(UnmanagedType.HString)] out string xml);
    }
}

