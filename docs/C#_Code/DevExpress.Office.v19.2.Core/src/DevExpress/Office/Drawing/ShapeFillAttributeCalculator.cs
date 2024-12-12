namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public static class ShapeFillAttributeCalculator
    {
        public static float CalculateBlipAlphaValue(DrawingBlip blip)
        {
            float num;
            using (IEnumerator<IDrawingEffect> enumerator = blip.Effects.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IDrawingEffect current = enumerator.Current;
                        AlphaModulateFixedEffect effect2 = current as AlphaModulateFixedEffect;
                        if (effect2 == null)
                        {
                            continue;
                        }
                        num = (float) DrawingValueConverter.FromPercentage(effect2.Amount);
                    }
                    else
                    {
                        return 1f;
                    }
                    break;
                }
            }
            return num;
        }

        public static RectangleF CalculatePictureClipBounds(RectangleOffset sourceRectangle, RectangleF sourceBounds)
        {
            if (sourceRectangle.Equals(RectangleOffset.Empty))
            {
                return sourceBounds;
            }
            float num = (float) DrawingValueConverter.FromPercentage(sourceRectangle.LeftOffset);
            float num2 = (float) DrawingValueConverter.FromPercentage(sourceRectangle.TopOffset);
            float num3 = (float) DrawingValueConverter.FromPercentage(sourceRectangle.RightOffset);
            float num4 = (float) DrawingValueConverter.FromPercentage(sourceRectangle.BottomOffset);
            float num5 = (1f - num) - num3;
            float num6 = (1f - num2) - num4;
            float width = (num5 != 0f) ? (sourceBounds.Width / num5) : sourceBounds.Width;
            float height = (num5 != 0f) ? (sourceBounds.Height / num6) : sourceBounds.Height;
            return new RectangleF(sourceBounds.X - (width * num), sourceBounds.Y - (height * num2), width, height);
        }

        public static RectangleF CalculatePictureSourceBounds(RectangleOffset sourceRectangle, int imageWidth, int imageHeight)
        {
            float x = imageWidth * ((float) DrawingValueConverter.FromPercentage(sourceRectangle.LeftOffset));
            float y = imageHeight * ((float) DrawingValueConverter.FromPercentage(sourceRectangle.TopOffset));
            float num3 = imageWidth * ((float) DrawingValueConverter.FromPercentage(sourceRectangle.RightOffset));
            float num4 = imageHeight * ((float) DrawingValueConverter.FromPercentage(sourceRectangle.BottomOffset));
            return new RectangleF(x, y, (imageWidth - x) - num3, (imageHeight - y) - num4);
        }
    }
}

