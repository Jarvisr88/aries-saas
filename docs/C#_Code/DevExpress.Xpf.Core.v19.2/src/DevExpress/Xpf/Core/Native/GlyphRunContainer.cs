namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class GlyphRunContainer : ITextContainer
    {
        private GlyphRun glyphRun;
        private LayoutDependentGlyphRunProperties glyphRunProperties;
        private const double EmMultiplier = 100.0;
        private Rect alignmentBox;
        private Size desiredSize;
        private double lastMeasureWidth;
        private double lastArrangeWidth;
        private GlyphTypeFacePropertiesCache glyphTypeFacePropertiesCache;

        public Size Arrange(Size finalSize, RenderTextBlockContext tbContext);
        private Point CalcAnchorPointForDrawText(RenderTextBlockContext context);
        private bool CheckIsInvalidIndex(ushort glyphIndex);
        private static bool CheckText(string text);
        private double GetAdvanceWidth(ushort glyphIndex, bool sideways);
        private ushort GetGlyphFromCharacter(char character);
        private TextAlignment GetTextAlignment(RenderTextBlockContext context);
        private TextTrimming GetTextTrimming(RenderTextBlockContext context);
        public bool Initialize(RenderTextBlockContext context);
        private bool IsRtl(RenderTextBlockContext tbContext);
        public Size Measure(Size availableSize, RenderTextBlockContext context);
        public bool NeedInvalidate(RenderTextBlockContext context);
        private LayoutDependentGlyphRunProperties ParseGlyphRunProperties(RenderTextBlockContext context, string text);
        private int ParseGlyphsProperty(GlyphTypeface fontFace, string unicodeString, bool sideways, out List<ParsedGlyphData> parsedGlyphs);
        public void Render(DrawingContext dc, RenderTextBlockContext context);

        public bool HasCollapsedLines { get; }
    }
}

