namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("9c906818-31d7-4fd3-a151-7c5e225db55a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteTextFormat
    {
        void SetTextAlignment(DWRITE_TEXT_ALIGNMENT textAlignment);
        void SetParagraphAlignment(DWRITE_PARAGRAPH_ALIGNMENT paragraphAlignment);
        void SetWordWrapping(DWRITE_WORD_WRAPPING wordWrapping);
        void SetReadingDirection(DWRITE_READING_DIRECTION readingDirection);
        void SetFlowDirection(DWRITE_FLOW_DIRECTION flowDirection);
        void SetIncrementalTabStop(float incrementalTabStop);
        IDWriteInlineObject SetTrimming([In] ref DWRITE_TRIMMING trimmingOptions);
        void SetLineSpacing(DWRITE_LINE_SPACING_METHOD lineSpacingMethod, float lineSpacing, float baseline);
        [PreserveSig]
        DWRITE_TEXT_ALIGNMENT GetTextAlignment();
        [PreserveSig]
        DWRITE_PARAGRAPH_ALIGNMENT GetParagraphAlignment();
        [PreserveSig]
        DWRITE_WORD_WRAPPING GetWordWrapping();
        [PreserveSig]
        DWRITE_READING_DIRECTION GetReadingDirection();
        [PreserveSig]
        DWRITE_FLOW_DIRECTION GetFlowDirection();
        [PreserveSig]
        float GetIncrementalTabStop();
        void GetTrimming(out DWRITE_TRIMMING trimmingOptions, out IDWriteInlineObject trimmingSign);
        void GetLineSpacing(out DWRITE_LINE_SPACING_METHOD lineSpacingMethod, out float lineSpacing, out float baseline);
        void GetFontCollection(out IDWriteFontCollection fontCollection);
        [PreserveSig]
        int GetFontFamilyNameLength();
        void GetFontFamilyName(out string fontFamilyName, int nameSize);
        [PreserveSig]
        DWRITE_FONT_WEIGHT GetFontWeight();
        [PreserveSig]
        DWRITE_FONT_STYLE GetFontStyle();
        [PreserveSig]
        DWRITE_FONT_STRETCH GetFontStretch();
        [PreserveSig]
        float GetFontSize();
        [PreserveSig]
        int GetLocaleNameLength();
        void GetLocaleName(out string localeName, int nameSize);
    }
}

