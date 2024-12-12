namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;

    public class WICColorTransform : WICBitmapSource
    {
        internal WICColorTransform(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Initialize(IComCallableWrapper<IWICBitmapSourceCCW> pIBitmapSource, WICColorContext pIContextSource, WICColorContext pIContextDest, Guid pixelFmtDest)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(pixelFmtDest.ToByteArray()))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, pIBitmapSource.NativeObject, pIContextSource.ToNativeObject(), pIContextDest.ToNativeObject(), marshaler.Pointer, 8));
            }
        }
    }
}

