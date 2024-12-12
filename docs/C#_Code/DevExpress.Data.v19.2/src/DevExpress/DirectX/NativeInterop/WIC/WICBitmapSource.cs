namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class WICBitmapSource : ComObject
    {
        internal WICBitmapSource(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void CopyPixels(WICRect rect, int stride, byte[] buffer)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(buffer))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref rect, stride, buffer.Length, marshaler.Pointer, 7));
            }
        }

        public Guid GetPixelFormat()
        {
            byte[] buffer = new byte[0x10];
            using (ArrayMarshaler marshaler = new ArrayMarshaler(buffer))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, 4));
                return new Guid(buffer);
            }
        }
    }
}

