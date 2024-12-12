namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1BitmapBrush : D2D1Brush
    {
        public D2D1BitmapBrush(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1Bitmap GetBitmap()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 15);
            return new D2D1Bitmap(ptr);
        }

        public D2D1_EXTEND_MODE GetExtendModeX() => 
            (D2D1_EXTEND_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 12);

        public D2D1_EXTEND_MODE GetExtendModeY() => 
            (D2D1_EXTEND_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 13);

        public D2D1_BITMAP_INTERPOLATION_MODE GetInterpolationMode() => 
            (D2D1_BITMAP_INTERPOLATION_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 14);

        public void SetBitmap(D2D1Bitmap bitmap)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, bitmap.ToNativeObject(), 11);
        }

        public void SetExtendModeX(D2D1_EXTEND_MODE extendModeX)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) extendModeX, 8);
        }

        public void SetExtendModeY(D2D1_EXTEND_MODE extendModeY)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) extendModeY, 9);
        }

        public void SetInterpolationMode(D2D1_BITMAP_INTERPOLATION_MODE interpolationMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) interpolationMode, 10);
        }
    }
}

