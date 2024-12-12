namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1BitmapBrush1 : D2D1BitmapBrush
    {
        public D2D1BitmapBrush1(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_INTERPOLATION_MODE GetInterpolationMode1() => 
            (D2D1_INTERPOLATION_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x11);

        public void SetInterpolationMode1(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) interpolationMode, 0x10);
        }
    }
}

