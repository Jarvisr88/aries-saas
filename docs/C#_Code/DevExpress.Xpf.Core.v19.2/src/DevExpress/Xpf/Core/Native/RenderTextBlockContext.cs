namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderTextBlockContext : FrameworkRenderElementContext
    {
        private string text;
        private string highlightedText;
        private DevExpress.Xpf.Editors.HighlightedTextCriteria criteria;
        private System.Windows.TextTrimming? textTrimming;
        private System.Windows.TextWrapping? textWrapping;
        private System.Windows.TextAlignment? textAlignment;
        private TextDecorationCollection textDecorations;
        private RenderTextBlockMode renderMode;
        private bool forceUseRealTextBlock;
        private readonly bool allowGlyphRunRendering;

        public RenderTextBlockContext(RenderTextBlock factory);
        public override void AddChild(FrameworkRenderElementContext child);
        protected override void FontSettingsChanged(RenderFontSettings oldValue, RenderFontSettings newValue);
        protected override void ForegroundChanged(object oldValue, object newValue);
        protected override FrameworkRenderElementContext GetRenderChild(int index);
        private void InvalidateText();
        protected override void OnFlowDirectionChanged(FlowDirection oldValue, FlowDirection newValue);
        public override void RemoveChild(FrameworkRenderElementContext child);
        protected override void ResetRenderCachesInternal();
        protected override void ResetTemplatesAndVisualsInternal();
        private bool ShouldAdditionalUpdateOnHighlightedTextChanged(string ht, bool hasDecorations);
        public override bool ShouldUseMirrorTransform();

        protected override int RenderChildrenCount { get; }

        public System.Windows.Media.Typeface Typeface { get; internal set; }

        public System.Windows.Media.GlyphTypeface GlyphTypeface { get; internal set; }

        public bool UseRealTextBlock { get; }

        public bool IsRealTextBlockInitialized { get; }

        public bool AllowGlyphRunRendering { get; }

        public string Text { get; set; }

        public bool ForceUseRealTextBlock { get; set; }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria { get; set; }

        public string HighlightedText { get; set; }

        public System.Windows.TextTrimming? TextTrimming { get; set; }

        public System.Windows.TextWrapping? TextWrapping { get; set; }

        public System.Windows.TextAlignment? TextAlignment { get; set; }

        public TextDecorationCollection TextDecorations { get; set; }

        public RenderTextBlockMode RenderMode { get; set; }

        public TextDecorationCollection CachedTextDecorations { get; internal set; }

        public GuidelineSet CachedGuidelineSet { get; internal set; }

        public ITextContainer GlyphRunContainer { get; internal set; }

        public ITextContainer FormattedTextContainer { get; internal set; }

        public RenderRealTextBlockContext Child { get; private set; }

        public double FontSize { get; }

        public System.Windows.FontStyle FontStyle { get; }

        public System.Windows.FontWeight FontWeight { get; }

        public System.Windows.FontStretch FontStretch { get; }

        public System.Windows.Media.FontFamily FontFamily { get; }
    }
}

