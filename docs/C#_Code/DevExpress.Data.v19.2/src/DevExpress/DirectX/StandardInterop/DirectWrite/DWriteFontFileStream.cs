namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DWriteFontFileStream : ComObject<IDWriteFontFileStream>
    {
        internal DWriteFontFileStream(IDWriteFontFileStream nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        public byte[] ReadAllData()
        {
            long num;
            IntPtr ptr;
            IntPtr ptr2;
            byte[] buffer2;
            InteropHelpers.CheckHResult(base.WrappedObject.GetFileSize(out num));
            InteropHelpers.CheckHResult(base.WrappedObject.ReadFileFragment(out ptr, 0L, num, out ptr2));
            try
            {
                byte[] destination = new byte[num];
                Marshal.Copy(ptr, destination, 0, (int) num);
                buffer2 = destination;
            }
            finally
            {
                base.WrappedObject.ReleaseFileFragment(ptr2);
            }
            return buffer2;
        }
    }
}

