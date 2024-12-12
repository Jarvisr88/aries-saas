namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd906a8-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Brush
    {
        void GetFactory();
        void SetOpacity();
        [PreserveSig]
        void SetTransform(ref D2D_MATRIX_3X2_F transform);
        void GetOpacity();
        void GetTransform();
    }
}

