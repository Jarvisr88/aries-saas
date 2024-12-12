namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("00000000-0000-0000-C000-000000000046")]
    public interface IUnknownCCW
    {
        [MethodOffset(0)]
        int QueryInterface(IntPtr riid, IntPtr ppOutput);
        [MethodOffset(1)]
        int AddRef();
        [MethodOffset(2)]
        int Release();
    }
}

