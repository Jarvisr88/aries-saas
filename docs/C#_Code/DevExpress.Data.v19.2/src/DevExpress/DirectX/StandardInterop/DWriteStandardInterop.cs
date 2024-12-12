namespace DevExpress.DirectX.StandardInterop
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical, CLSCompliant(false)]
    public static class DWriteStandardInterop
    {
        public static DWriteFactory CreateDwriteFactory(bool shared = false)
        {
            IDWriteFactory factory;
            DWriteCreateFactory(shared ? DWRITE_FACTORY_TYPE.Shared : DWRITE_FACTORY_TYPE.Isolated, new Guid("b859ee5a-d838-4b5b-a2e8-1adc7d93db48"), out factory);
            return ((factory == null) ? null : new DWriteFactory(factory));
        }

        [DllImport("Dwrite.dll")]
        private static extern uint DWriteCreateFactory(DWRITE_FACTORY_TYPE type, [In, MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IDWriteFactory factory);
    }
}

