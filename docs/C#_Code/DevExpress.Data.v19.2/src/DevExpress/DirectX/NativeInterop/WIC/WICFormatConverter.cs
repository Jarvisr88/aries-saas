namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;

    public class WICFormatConverter : WICBitmapSource
    {
        internal WICFormatConverter(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Initialize(IComCallableWrapper<IWICBitmapSourceCCW> bitmapSource, Guid guid)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(guid.ToByteArray()))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, bitmapSource.NativeObject, marshaler.Pointer, 0, IntPtr.Zero, 0.0, 0, 8));
            }
        }

        public void Initialize(WICBitmapSource bitmapSource, Guid guid)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(guid.ToByteArray()))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, bitmapSource.NativeObject, marshaler.Pointer, 0, IntPtr.Zero, 0.0, 0, 8));
            }
        }
    }
}

