namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface ITextContainer
    {
        Size Arrange(Size finalSize, RenderTextBlockContext tbContext);
        bool Initialize(RenderTextBlockContext context);
        Size Measure(Size availableSize, RenderTextBlockContext tbContext);
        bool NeedInvalidate(RenderTextBlockContext tbContext);
        void Render(DrawingContext dc, RenderTextBlockContext context);

        bool HasCollapsedLines { get; }
    }
}

