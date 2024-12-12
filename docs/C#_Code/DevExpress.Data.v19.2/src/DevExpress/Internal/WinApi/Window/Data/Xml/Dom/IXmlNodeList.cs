namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("8C60AD77-83A4-4EC1-9C54-7BA429E13DA6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlNodeList : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        int Length { get; }
        int Item(uint index, out IXmlNode node);
    }
}

