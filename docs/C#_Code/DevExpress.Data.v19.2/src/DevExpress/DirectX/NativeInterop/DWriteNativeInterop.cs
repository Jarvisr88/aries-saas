namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop.DirectWrite;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    public static class DWriteNativeInterop
    {
        public static DWriteFactory CreateDwriteFactory()
        {
            IntPtr ptr;
            DWriteCreateFactory(DWRITE_FACTORY_TYPE.Isolated, new Guid("b859ee5a-d838-4b5b-a2e8-1adc7d93db48"), out ptr);
            return new DWriteFactory(ptr);
        }

        [DllImport("Dwrite.dll")]
        private static extern uint DWriteCreateFactory(DWRITE_FACTORY_TYPE type, [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IntPtr factory);
    }
}

