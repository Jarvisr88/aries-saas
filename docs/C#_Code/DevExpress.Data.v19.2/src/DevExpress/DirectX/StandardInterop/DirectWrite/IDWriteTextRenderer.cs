namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("ef8a8135-5cc6-45fe-8825-c5a0724eb819"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteTextRenderer
    {
        [PreserveSig]
        int IsPixelSnappingDisabled(IntPtr clientDrawingContext, out bool isDisabled);
        [PreserveSig]
        int GetCurrentTransform(IntPtr clientDrawingContext, out DWRITE_MATRIX transform);
        [PreserveSig]
        int GetPixelsPerDip(IntPtr clientDrawingContext, out float pixelsPerDip);
        [PreserveSig]
        int DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, DWRITE_MEASURING_MODE measuringMode, ref DWRITE_GLYPH_RUN_COMMON glyphRun, ref DWRITE_GLYPH_RUN_DESCRIPTION glyphRunDescription, IntPtr clientDrawingEffect);
        [PreserveSig]
        int DrawUnderline();
        [PreserveSig]
        int DrawStrikethrough();
        [PreserveSig]
        int DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, bool isSideways, bool isRightToLeft, IntPtr clientDrawingEffect);
    }
}

