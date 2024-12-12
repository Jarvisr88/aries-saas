namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1LinearGradientBrush : D2D1Brush
    {
        internal D2D1LinearGradientBrush(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D_POINT_2F GetEndPoint()
        {
            throw new NotImplementedException();
        }

        public D2D1GradientStopCollection GetGradientStopCollection()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 12);
            return new D2D1GradientStopCollection(ptr);
        }

        public D2D_POINT_2F GetStartPoint()
        {
            throw new NotImplementedException();
        }

        public void SetEndPoint(D2D_POINT_2F endPoint)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, endPoint, 9);
        }

        public void SetStartPoint(D2D_POINT_2F startPoint)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, startPoint, 8);
        }
    }
}

