namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1GradientStopCollection : D2D1Resource
    {
        public D2D1GradientStopCollection(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_GAMMA GetColorInterpolationGamma() => 
            (D2D1_GAMMA) ComObject.InvokeHelper.CalliInt(base.NativeObject, 6);

        public D2D1_EXTEND_MODE GetExtendMode() => 
            (D2D1_EXTEND_MODE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 7);

        public void GetGradientStopCount()
        {
            throw new NotImplementedException();
        }

        public void GetGradientStops()
        {
            throw new NotImplementedException();
        }
    }
}

