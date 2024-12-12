namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("eaf3a2da-ecf4-4d24-b644-b34f6842024b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWritePixelSnapping
    {
        [PreserveSig]
        int IsPixelSnappingDisabled(IntPtr clientDrawingContext, out bool isDisabled);
        [PreserveSig]
        int GetCurrentTransform(IntPtr clientDrawingContext, out DWRITE_MATRIX transform);
        [PreserveSig]
        int GetPixelsPerDip(IntPtr clientDrawingContext, out float pixelsPerDip);
    }
}

