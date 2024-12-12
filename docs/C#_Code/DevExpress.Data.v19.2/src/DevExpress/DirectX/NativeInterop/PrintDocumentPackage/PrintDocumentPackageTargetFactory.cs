namespace DevExpress.DirectX.NativeInterop.PrintDocumentPackage
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Security;

    public class PrintDocumentPackageTargetFactory : ComObject
    {
        private static readonly Guid clsid = new Guid("348ef17d-6c81-4982-92b4-ee188a43867a");
        private static readonly Guid interfaceId = new Guid("d2959bf7-b31b-4a3d-9600-712eb1335ba4");

        private PrintDocumentPackageTargetFactory(IntPtr nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        public static PrintDocumentPackageTargetFactory Create() => 
            new PrintDocumentPackageTargetFactory(Ole32Interop.CoCreateInstance(clsid, interfaceId));

        public PrintDocumentPackageTarget CreateDocumentPackageTargetForPrintJob(string printerName, string jobName, NativeStream jobOutputStream, NativeStream jobPrintTicketStream)
        {
            IntPtr ptr;
            using (StringMarshaler marshaler = new StringMarshaler(printerName))
            {
                using (StringMarshaler marshaler2 = new StringMarshaler(jobName))
                {
                    InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, marshaler2.Pointer, jobOutputStream.ToNativeObject(), jobPrintTicketStream.ToNativeObject(), out ptr, 3));
                }
            }
            return new PrintDocumentPackageTarget(ptr);
        }
    }
}

