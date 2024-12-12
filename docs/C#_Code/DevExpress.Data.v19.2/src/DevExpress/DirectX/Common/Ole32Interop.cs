namespace DevExpress.DirectX.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    internal static class Ole32Interop
    {
        private const int CLSCTX_INPROC_SERVER = 1;

        [SecuritySafeCritical]
        public static IntPtr CoCreateInstance(Guid clsid, Guid iid)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(clsid.ToByteArray()))
            {
                using (ArrayMarshaler marshaler2 = new ArrayMarshaler(iid.ToByteArray()))
                {
                    InteropHelpers.CheckHResult(CoCreateInstance(marshaler.Pointer, IntPtr.Zero, 1, marshaler2.Pointer, out ptr));
                }
            }
            return ptr;
        }

        [DllImport("Ole32.dll")]
        private static extern int CoCreateInstance(IntPtr rclsid, IntPtr pUnkOuter, int dwClsContext, IntPtr riid, out IntPtr ppv);
    }
}

