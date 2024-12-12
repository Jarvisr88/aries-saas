namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class DWriteFontFamily : DWriteFontList
    {
        public DWriteFontFamily(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DWriteLocalizedStrings GetFamilyNames()
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, 6));
            return new DWriteLocalizedStrings(ptr);
        }

        public DWriteFont GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) weight, (int) stretch, (int) style, out ptr, 7));
            return new DWriteFont(ptr);
        }

        public DWriteFontList GetMatchingFonts(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) weight, (int) stretch, (int) style, out ptr, 8));
            return new DWriteFontList(ptr);
        }
    }
}

