namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("53737037-6d14-410b-9bfe-0b182bb70961"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteTextLayout
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
        void SetMaxWidth(float maxWidth);
        void SetMaxHeight(float maxHeight);
        void SetFontCollection(IDWriteFontCollection fontCollection, DWRITE_TEXT_RANGE textRange);
        void SetFontFamilyName(string fontFamilyName, DWRITE_TEXT_RANGE textRange);
        void SetFontWeight(DWRITE_FONT_WEIGHT fontWeight, DWRITE_TEXT_RANGE textRange);
        void SetFontStyle(DWRITE_FONT_STYLE fontStyle, DWRITE_TEXT_RANGE textRange);
        void SetFontStretch(DWRITE_FONT_STRETCH fontStretch, DWRITE_TEXT_RANGE textRange);
        void SetFontSize(float fontSize, DWRITE_TEXT_RANGE textRange);
        void SetUnderline(bool hasUnderline, DWRITE_TEXT_RANGE textRange);
        void SetStrikethrough(bool hasStrikethrough, DWRITE_TEXT_RANGE textRange);
        void SetDrawingEffect(IntPtr drawingEffect, DWRITE_TEXT_RANGE textRange);
        void SetInlineObject(IDWriteInlineObject inlineObject, DWRITE_TEXT_RANGE textRange);
        void SetTypography();
        void SetLocaleName(string localeName, DWRITE_TEXT_RANGE textRange);
        [PreserveSig]
        float GetMaxWidth();
        [PreserveSig]
        float GetMaxHeight();
        void GetFontCollection(int currentPosition, out IDWriteFontCollection fontCollection, out DWRITE_TEXT_RANGE textRange);
        void GetFontFamilyNameLength(int currentPosition, out int nameLength, out DWRITE_TEXT_RANGE textRange);
        void GetFontFamilyName(int currentPosition, out string fontFamilyName, int nameSize, out DWRITE_TEXT_RANGE textRange);
        void GetFontWeight(int currentPosition, out DWRITE_FONT_WEIGHT fontWeight, out DWRITE_TEXT_RANGE textRange);
        void GetFontStyle(int currentPosition, out DWRITE_FONT_STYLE fontStyle, out DWRITE_TEXT_RANGE textRange);
        void GetFontStretch(int currentPosition, out DWRITE_FONT_STRETCH fontStretch, out DWRITE_TEXT_RANGE textRange);
        void GetFontSize(int currentPosition, out float fontSize, out DWRITE_TEXT_RANGE textRange);
        void GetUnderline(int currentPosition, out bool hasUnderline, out DWRITE_TEXT_RANGE textRange);
        void GetStrikethrough(int currentPosition, out bool hasStrikethrough, out DWRITE_TEXT_RANGE textRange);
        void GetDrawingEffect(int currentPosition, out IntPtr drawingEffect, out DWRITE_TEXT_RANGE textRange);
        void GetInlineObject(int currentPosition, out IDWriteInlineObject inlineObject, out DWRITE_TEXT_RANGE textRange);
        void GetTypography();
        void GetLocaleNameLength(int currentPosition, out int nameLength, out DWRITE_TEXT_RANGE textRange);
        void GetLocaleName(int currentPosition, out string localeName, int nameSize, out DWRITE_TEXT_RANGE textRange);
        void Draw(IntPtr clientDrawingContext, IDWriteTextRenderer renderer, float originX, float originY);
        void GetLineMetrics();
        void GetMetrics();
        void GetOverhangMetrics();
        [PreserveSig]
        int GetClusterMetrics([In, Out, MarshalAs(UnmanagedType.LPArray)] DWRITE_CLUSTER_METRICS[] metrics, int metricsCount, out int actualClusterCount);
        void DetermineMinWidth(out float minWidth);
        void HitTestPoint();
        void HitTestTextPosition();
        void HitTestTextRange();
    }
}

