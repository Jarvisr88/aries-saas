namespace DevExpress.DirectX.NativeInterop.Direct2D.CCW
{
    using DevExpress.DirectX.NativeInterop.CCW;
    using DevExpress.DirectX.NativeInterop.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [Guid("36bfdcb6-9739-435d-a30d-a653beff6a6f")]
    public interface ID2D1DrawTransformCCW : ID2D1TransformCCW, ID2D1TransformNodeCCW, IUnknownCCW
    {
        [MethodOffset(0)]
        int SetDrawInfo(D2D1DrawInfo drawInfo);
    }
}

