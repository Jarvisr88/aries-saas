namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1GeometrySink : D2D1SimplifiedGeometrySink
    {
        public D2D1GeometrySink(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void AddArc()
        {
            throw new NotImplementedException();
        }

        public void AddBezier(D2D1_BEZIER_SEGMENT bezier)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref bezier, 11);
        }

        public void AddLine(D2D_POINT_2F point)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, point, 10);
        }

        public void AddQuadraticBezier()
        {
            throw new NotImplementedException();
        }

        public void AddQuadraticBeziers()
        {
            throw new NotImplementedException();
        }
    }
}

