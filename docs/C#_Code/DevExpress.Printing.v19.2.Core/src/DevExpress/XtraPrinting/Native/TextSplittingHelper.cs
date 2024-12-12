namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public static class TextSplittingHelper
    {
        private static int GetEndLineIndex(int lineCount, float firstLineOffset, Font font, RectangleF brickBounds, RectangleF visibleBounds)
        {
            FontMetrics metrics = new FontMetrics(font, GraphicsUnit.Document);
            float num = brickBounds.Top + firstLineOffset;
            float num2 = num + metrics.CalculateHeight(1);
            if (FloatsComparer.Default.FirstGreaterSecond((double) num2, (double) brickBounds.Bottom) || FloatsComparer.Default.FirstGreaterSecond((double) num2, (double) visibleBounds.Bottom))
            {
                return 0;
            }
            float num3 = num + metrics.CalculateHeight(lineCount);
            if (!FloatsComparer.Default.FirstGreaterSecond((double) num3, (double) visibleBounds.Bottom))
            {
                return (!FloatsComparer.Default.FirstGreaterSecond((double) num3, (double) visibleBounds.Top) ? -1 : (lineCount - 1));
            }
            for (int i = lineCount - 1; i >= 0; i--)
            {
                float num5 = metrics.CalculateHeight(i + 1);
                if (FloatsComparer.Default.FirstLessOrEqualSecond((double) (num + num5), (double) visibleBounds.Bottom))
                {
                    return i;
                }
            }
            return -1;
        }

        private static float GetFirstLineOffset(RectangleF brickRectangle, string text, Font font, StringFormat stringFormat)
        {
            float num2;
            if (stringFormat.LineAlignment == StringAlignment.Near)
            {
                return 0f;
            }
            using (Graphics graphics = GraphicsHelper.CreateGraphicsFromHiResImage())
            {
                graphics.PageUnit = GraphicsUnit.Document;
                LineSplitter splitter = new LineSplitter(text, font, stringFormat);
                int firstValidSymbol = splitter.GetFirstValidSymbol();
                if (firstValidSymbol == -1)
                {
                    num2 = 0f;
                }
                else
                {
                    RectangleF ef = splitter.GetCharacterRect(graphics, brickRectangle, firstValidSymbol);
                    num2 = !ef.IsEmpty ? (ef.Top - brickRectangle.Top) : 0f;
                }
            }
            return num2;
        }

        private static int GetStartLineIndex(int lineCount, float firstLineOffset, Font font, RectangleF brickBounds, RectangleF visibleBounds)
        {
            float num = brickBounds.Top + firstLineOffset;
            if (!FloatsComparer.Default.FirstLessSecond((double) num, (double) visibleBounds.Top))
            {
                return (!FloatsComparer.Default.FirstLessSecond((double) num, (double) visibleBounds.Bottom) ? -1 : 0);
            }
            FontMetrics metrics = new FontMetrics(font, GraphicsUnit.Document);
            for (int i = 0; i < lineCount; i++)
            {
                float num3 = metrics.CalculateHeight(i + 1);
                if (FloatsComparer.Default.FirstGreaterSecond((double) (num + num3), (double) visibleBounds.Top))
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetVisibleText(TextBrick textBrick, RectangleF docRect, RectangleF docClipRect, IPrintingSystemContext context)
        {
            string text = textBrick.Text;
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            BrickStyle style = textBrick.Style;
            float firstLineOffset = GetFirstLineOffset(docRect, text, style.Font, style.StringFormat.Value);
            string[] strArray = TextFormatter.CreateInstance(GraphicsUnit.Document, context.Measurer).FormatMultilineText(text, style.Font, docRect.Width, float.MaxValue, style.StringFormat.Value);
            int startIndex = GetStartLineIndex(strArray.Length, firstLineOffset, style.Font, docRect, docClipRect);
            if (startIndex < 0)
            {
                return string.Empty;
            }
            int num3 = GetEndLineIndex(strArray.Length, firstLineOffset, style.Font, docRect, docClipRect);
            return ((num3 >= 0) ? ((strArray.Length != 0) ? string.Join(Environment.NewLine, strArray, startIndex, (num3 - startIndex) + 1) : string.Empty) : string.Empty);
        }
    }
}

