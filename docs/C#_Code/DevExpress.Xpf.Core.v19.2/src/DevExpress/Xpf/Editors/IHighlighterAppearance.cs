namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public interface IHighlighterAppearance
    {
        Inline CreateInlineForHighlighting(FrameworkElement editor, string text);

        FontFamily HighlightedFontFamily { get; set; }

        double? HighlightedFontSize { get; set; }

        FontStyle? HighlightedFontStyle { get; set; }

        FontWeight? HighlightedFontWeight { get; set; }

        Brush HighlightedTextBackground { get; set; }

        Brush HighlightedTextForeground { get; set; }
    }
}

