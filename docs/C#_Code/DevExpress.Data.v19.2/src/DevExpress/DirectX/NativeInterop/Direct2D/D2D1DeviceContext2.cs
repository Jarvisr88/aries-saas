namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [Guid("394ea6a3-0c34-4321-950b-6ca20f0be6c7")]
    public class D2D1DeviceContext2 : D2D1DeviceContext
    {
        internal D2D1DeviceContext2(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1GradientMesh CreateGradientMesh(D2D1_GRADIENT_MESH_PATCH[] patches)
        {
            IntPtr ptr;
            using (ArrayMarshaler marshaler = new ArrayMarshaler(patches))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, patches.Length, out ptr, 0x61));
            }
            return new D2D1GradientMesh(ptr);
        }

        public void DrawGradientMesh(D2D1GradientMesh mesh)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, mesh.NativeObject, 0x67);
        }
    }
}

