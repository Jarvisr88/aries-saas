namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1C741D59-2122-47D5-A856-83F3D4214875"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlNode : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        void get_NodeValue();
        void put_NodeValue();
        void get_NodeType();
        void get_NodeName();
        void get_ParentNode();
        void get_ChildNodes();
        void get_FirstChild();
        void get_LastChild();
        void get_PreviousSibling();
        void get_NextSibling();
        IXmlNamedNodeMap Attributes { get; }
        void HasChildNodes();
        void get_OwnerDocument();
        void InsertBefore();
        void ReplaceChild();
        void RemoveChild();
        int AppendChild(IXmlNode childNode, out IXmlNode appendedChild);
    }
}

