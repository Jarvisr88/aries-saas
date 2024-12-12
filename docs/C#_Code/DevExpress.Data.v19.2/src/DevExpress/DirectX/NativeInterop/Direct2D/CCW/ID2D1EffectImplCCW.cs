namespace DevExpress.DirectX.NativeInterop.Direct2D.CCW
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop.CCW;
    using DevExpress.DirectX.NativeInterop.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [Guid("a248fd3f-3e6c-4e63-9f03-7f68ecc91db9")]
    public interface ID2D1EffectImplCCW : IUnknownCCW
    {
        [MethodOffset(0)]
        int Initialize(D2D1EffectContext effectContext, D2D1TransformGraph transformGraph);
        [MethodOffset(1)]
        int PrepareForRender(D2D1_CHANGE_TYPE changeType);
        [MethodOffset(2)]
        int SetGraph(D2D1TransformGraph transformGraph);
    }
}

