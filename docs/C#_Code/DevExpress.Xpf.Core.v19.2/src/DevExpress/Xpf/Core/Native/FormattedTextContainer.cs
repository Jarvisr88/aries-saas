namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class FormattedTextContainer : ITextContainer
    {
        private static readonly Func<FormattedText, IEnumerator> getFormattedTextEnumerator;
        private Size desiredSize;
        private FormattedText formattedText;

        static FormattedTextContainer();
        public Size Arrange(Size finalSize, RenderTextBlockContext tbContext);
        private Point CalcAnchorPointForDrawText(RenderTextBlockContext context, TextTrimming textTrimming);
        private FormattedText CreateFormattedText(RenderTextBlockContext context, string textToFormat);
        private TextAlignment GetTextAlignment(RenderTextBlockContext context);
        private string GetTextToFormat(RenderTextBlockContext context);
        private TextTrimming GetTextTrimming(RenderTextBlockContext context);
        private TextWrapping GetTextWrapping(RenderTextBlockContext context);
        public bool Initialize(RenderTextBlockContext context);
        private bool IsRtl(RenderTextBlockContext tbContext);
        public Size Measure(Size availableSize, RenderTextBlockContext tbContext);
        public bool NeedInvalidate(RenderTextBlockContext tbContext);
        public void Render(DrawingContext dc, RenderTextBlockContext context);

        public object Trimming { get; }

        public bool HasCollapsedLines { get; }
    }
}

