namespace ActiproSoftware.WinUICore.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface ICanvas
    {
        IDisposable CreateTextBatch();
        ITextLayout CreateTextLayout(string text, float maxWidth, string fontFamilyName, float fontSize, Color foreground);
        ITextLayout CreateTextLayout(ITextProvider textProvider, float maxWidth, string fontFamilyName, float fontSize, Color foreground, IEnumerable<ITextSpacer> spacers);
        ITextSpacer CreateTextSpacer(int characterIndex, object key, Size size, float baseline);
        SolidBrush GetSolidColorBrush(Color color);
        Pen GetSquiggleLinePen(Color color);
    }
}

