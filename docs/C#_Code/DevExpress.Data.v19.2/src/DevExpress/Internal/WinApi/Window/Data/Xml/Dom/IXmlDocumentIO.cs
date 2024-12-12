namespace DevExpress.Internal.WinApi.Window.Data.Xml.Dom
{
    using DevExpress.Internal.WinApi;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, CLSCompliant(false), Guid("6cd0e74e-ee65-4489-9ebf-ca43e87ba637"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlDocumentIO : IInspectable
    {
        void IInspectableStub1();
        void IInspectableStub2();
        void IInspectableStub3();
        void LoadXml([In, MarshalAs(UnmanagedType.HString)] string xml);
    }
}

