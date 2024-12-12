namespace DevExpress.DirectX.NativeInterop.WIC.CCW
{
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("00000120-a8f2-4877-ba0a-fd2b6645fb94")]
    public interface IWICBitmapSourceCCW : IUnknownCCW
    {
        [MethodOffset(0)]
        int GetSize(out int width, out int height);
        [MethodOffset(1)]
        int GetPixelFormat(out Guid pixelFormat);
        [MethodOffset(2)]
        int GetResolution(out double dpiX, out double dpiY);
        [MethodOffset(3)]
        int CopyPalette(IntPtr palette);
        [MethodOffset(4)]
        int CopyPixels(ref WICRect prc, int stride, int bufferSize, IntPtr buffer);
    }
}

