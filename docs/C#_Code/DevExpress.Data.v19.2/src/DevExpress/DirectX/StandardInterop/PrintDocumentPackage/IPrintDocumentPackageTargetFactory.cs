namespace DevExpress.DirectX.StandardInterop.PrintDocumentPackage
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("d2959bf7-b31b-4a3d-9600-712eb1335ba4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPrintDocumentPackageTargetFactory
    {
        IPrintDocumentPackageTarget CreateDocumentPackageTargetForPrintJob([In] string printerName, [In] string jobName, [In] IntPtr jobOutputStream, [In] IntPtr jobPrintTicketStream);
    }
}

