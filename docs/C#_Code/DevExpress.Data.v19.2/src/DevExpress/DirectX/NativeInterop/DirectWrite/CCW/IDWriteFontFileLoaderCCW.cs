namespace DevExpress.DirectX.NativeInterop.DirectWrite.CCW
{
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("727cad4e-d6af-4c9e-8a08-d695b11caa49")]
    public interface IDWriteFontFileLoaderCCW : IUnknownCCW
    {
        [MethodOffset(0)]
        int CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, out IntPtr fontFileStream);
    }
}

