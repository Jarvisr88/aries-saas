namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1SimplifiedGeometrySink : ComObject
    {
        public D2D1SimplifiedGeometrySink(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void AddBeziers(D2D1_BEZIER_SEGMENT[] beziers)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(beziers))
            {
                ComObject.InvokeHelper.Calli(base.NativeObject, marshaler.Pointer, beziers.Length, 7);
            }
        }

        public void AddLines(D2D_POINT_2F[] points)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(points))
            {
                ComObject.InvokeHelper.Calli(base.NativeObject, marshaler.Pointer, points.Length, 6);
            }
        }

        public void BeginFigure(D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, startPoint, (int) figureBegin, 5);
        }

        public void Close()
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, 9));
        }

        public void EndFigure(D2D1_FIGURE_END figureEnd)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) figureEnd, 8);
        }

        public void SetFillMode(D2D1_FILL_MODE fillMode)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, (int) fillMode, 3);
        }

        public void SetSegmentFlags()
        {
            throw new NotImplementedException();
        }
    }
}

