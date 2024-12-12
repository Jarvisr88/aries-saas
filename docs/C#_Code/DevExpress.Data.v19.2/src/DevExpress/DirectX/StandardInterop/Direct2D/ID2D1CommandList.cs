namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("b4f34a19-2383-4d76-94f6-ec343657c3dc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1CommandList
    {
        void GetFactory();
        void Stream();
        void Close();
    }
}

