namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    public class D2D1TransformedGeometry : D2D1Geometry
    {
        public D2D1TransformedGeometry(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1Geometry GetSourceGeometry()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 0x11);
            return new D2D1Geometry(ptr);
        }

        public void GetTransform(out D2D_MATRIX_3X2_F transform)
        {
            throw new NotImplementedException();
        }
    }
}

