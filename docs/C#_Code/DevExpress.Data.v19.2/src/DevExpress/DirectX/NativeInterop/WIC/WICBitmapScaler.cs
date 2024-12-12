namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;

    public class WICBitmapScaler : WICBitmapSource
    {
        internal WICBitmapScaler(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Initialize(IComCallableWrapper<IWICBitmapSourceCCW> source, int width, int height, WICBitmapInterpolationMode mode)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, source.NativeObject, width, height, (int) mode, 8));
        }

        public void Initialize(WICBitmapSource source, int width, int height, WICBitmapInterpolationMode mode)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, source.NativeObject, width, height, (int) mode, 8));
        }
    }
}

