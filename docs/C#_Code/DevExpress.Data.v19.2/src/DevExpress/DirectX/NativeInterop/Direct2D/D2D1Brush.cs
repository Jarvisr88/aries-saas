namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    public class D2D1Brush : D2D1Resource
    {
        public D2D1Brush(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public float GetOpacity() => 
            (float) ComObject.InvokeHelper.CalliInt(base.NativeObject, 6);

        public void GetTransform(out D2D_MATRIX_3X2_F transform)
        {
            transform = D2D_MATRIX_3X2_F.Identity;
            ComObject.InvokeHelper.Calli(base.NativeObject, ref transform, 7);
        }

        public void SetOpacity(float opacity)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, opacity, 4);
        }

        public void SetTransform(D2D_MATRIX_3X2_F transform)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref transform, 5);
        }
    }
}

