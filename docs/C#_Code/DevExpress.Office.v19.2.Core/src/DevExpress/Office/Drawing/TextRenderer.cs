namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using System;
    using System.Drawing;

    public static class TextRenderer
    {
        public static TextRectanglePathInfo CalculateTextLayoutInfo(TextRendererContext textRendererContext)
        {
            TextLayoutCalculator textLayoutCalculator = GetTextLayoutCalculator(textRendererContext);
            textLayoutCalculator.CalculateTextLayout();
            if (textLayoutCalculator.ParagraphLayoutInfos.Count == 0)
            {
                textLayoutCalculator.Dispose();
                return null;
            }
            Size resultBitmapSize = textLayoutCalculator.GetResultBitmapSize();
            Rectangle textBounds = textLayoutCalculator.GetTextBounds();
            if ((resultBitmapSize.Width != 0) && ((resultBitmapSize.Height != 0) && ((textBounds.Width != 0) && (textBounds.Height != 0))))
            {
                return new TextRectanglePathInfo(textLayoutCalculator, Rectangle.Round(textRendererContext.TextRectangle));
            }
            textLayoutCalculator.Dispose();
            return null;
        }

        private static TextLayoutCalculator GetTextLayoutCalculator(TextRendererContext textRendererContext)
        {
            switch (textRendererContext.BodyProperties.VerticalText)
            {
                case DrawingTextVerticalTextType.EastAsianVertical:
                    return new HorizontalTextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.Horizontal:
                    return new HorizontalTextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.MongolianVertical:
                    return new HorizontalTextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.Vertical:
                    return new Vertical90TextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.Vertical270:
                    return new Vertical270TextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.WordArtVertical:
                    return new WordArtVerticalTextCalculator(textRendererContext);

                case DrawingTextVerticalTextType.VerticalWordArtRightToLeft:
                    return new WordArtVerticalRTLTextCalculator(textRendererContext);
            }
            throw new ArgumentOutOfRangeException("BodyProperties.VerticalText");
        }
    }
}

