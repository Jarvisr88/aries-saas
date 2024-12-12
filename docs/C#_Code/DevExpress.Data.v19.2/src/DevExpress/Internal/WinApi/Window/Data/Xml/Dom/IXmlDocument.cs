namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("F7F3A506-1E87-42D6-BCFB-B8C809FA5494"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlDocument : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        void get_Doctype();
        void get_Implementation();
        void get_DocumentElement();
        int CreateElement([In, MarshalAs(UnmanagedType.HString)] string name, out IXmlElement element);
        void CreateDocumentFragment();
        int CreateTextNode([In, MarshalAs(UnmanagedType.HString)] string data, out IXmlText newTextNode);
        void CreateComment();
        void CreateProcessingInstruction();
        void CreateAttribute();
        void CreateEntityReference();
        int GetElementsByTagName([In, MarshalAs(UnmanagedType.HString)] string tagName, out IXmlNodeList elements);
    }
}

