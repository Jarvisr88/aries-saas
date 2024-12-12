namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1PathGeometry : D2D1Geometry
    {
        public D2D1PathGeometry(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void GetFigureCount()
        {
            throw new NotImplementedException();
        }

        public void GetSegmentCount()
        {
            throw new NotImplementedException();
        }

        public D2D1GeometrySink Open()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 0x11));
            return new D2D1GeometrySink(ptr);
        }

        public void Stream(D2D1GeometrySink geometrySink)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, geometrySink.ToNativeObject(), 0x12));
        }
    }
}

