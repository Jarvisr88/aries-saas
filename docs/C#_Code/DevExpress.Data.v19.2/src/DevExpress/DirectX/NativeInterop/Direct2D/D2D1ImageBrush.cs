namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1ImageBrush : D2D1Brush
    {
        public D2D1ImageBrush(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_EXTEND_MODE GetExtendModeX() => 
            (D2D1_EXTEND_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 14);

        public D2D1_EXTEND_MODE GetExtendModeY() => 
            (D2D1_EXTEND_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 15);

        public D2D1Image GetImage()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 13);
            return new D2D1Image(ptr);
        }

        public D2D1_INTERPOLATION_MODE GetInterpolationMode() => 
            (D2D1_INTERPOLATION_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x10);

        public void GetSourceRectangle()
        {
            throw new NotImplementedException();
        }

        public void SetExtendModeX(D2D1_EXTEND_MODE extendModeX)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) extendModeX, 9);
        }

        public void SetExtendModeY(D2D1_EXTEND_MODE extendModeY)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) extendModeY, 10);
        }

        public void SetImage(D2D1Image image)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, image.NativeObject, 8);
        }

        public void SetInterpolationMode(D2D1_INTERPOLATION_MODE interpolationMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) interpolationMode, 11);
        }

        public void SetSourceRectangle(D2D_RECT_F sourceRectangle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref sourceRectangle, 12);
        }
    }
}

