namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("B3A69EB0-AAB0-4B82-A6FA-B1453F7C021B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlNamedNodeMap : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        void get_Length();
        void Item();
        int GetNamedItem([In, MarshalAs(UnmanagedType.HString)] string name, out IXmlNode node);
    }
}

