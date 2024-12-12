namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    [ComImport, Guid("2c1d867d-c290-41c8-ae7e-34a98702e9a5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1PrintControl
    {
        void AddPage([In] ID2D1CommandList commandList, D2D_SIZE_F pageSize, [In, Out] IStream pagePrintTicketStream, out long tag1, out long tag2);
        void Close();
    }
}

