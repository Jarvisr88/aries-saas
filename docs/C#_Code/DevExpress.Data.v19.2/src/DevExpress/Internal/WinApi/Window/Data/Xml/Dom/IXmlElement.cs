namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("2DFB8A1F-6B10-4EF8-9F83-EFCCE8FAEC37"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlElement : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        string TagName { get; }
        void GetAttribute();
        int SetAttribute([In, MarshalAs(UnmanagedType.HString)] string attributeName, [In, MarshalAs(UnmanagedType.HString)] string attributeValue);
    }
}

