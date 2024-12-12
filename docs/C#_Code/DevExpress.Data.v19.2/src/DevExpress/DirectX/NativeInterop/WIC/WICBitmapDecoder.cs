namespace DevExpress.DirectX.NativeInterop.WIC
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class WICBitmapDecoder : ComObject
    {
        internal WICBitmapDecoder(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public WICBitmapDecodeFrame GetFrame(int index)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, index, out ptr, 13));
            return new WICBitmapDecodeFrame(ptr);
        }

        public int GetFrameCount()
        {
            int num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out num, 12));
            return num;
        }

        public void Initialize(NativeStream data, WICDecodeOptions decodeOptions)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, data.NativeObject, (int) decodeOptions, 4));
        }
    }
}

