namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class GlowEffectHelper
    {
        private const float epsilon = 1.192093E-07f;

        public static Brush ConvertGradientBrushWithTransparentColors(Brush brush, Color glowColor)
        {
            LinearGradientBrush brush2 = brush as LinearGradientBrush;
            if (brush2 != null)
            {
                ColorBlend blend = ConvertTransparentColors(brush2.InterpolationColors, glowColor);
                if (blend != null)
                {
                    LinearGradientBrush brush4 = (LinearGradientBrush) brush.Clone();
                    brush4.InterpolationColors = blend;
                    return brush4;
                }
            }
            PathGradientBrush brush3 = brush as PathGradientBrush;
            if (brush3 != null)
            {
                ColorBlend blend2 = ConvertTransparentColors(brush3.InterpolationColors, glowColor);
                if (blend2 != null)
                {
                    PathGradientBrush brush5 = (PathGradientBrush) brush.Clone();
                    brush5.InterpolationColors = blend2;
                    return brush5;
                }
            }
            return null;
        }

        private static ColorBlend ConvertTransparentColors(ColorBlend colorBlend, Color glowColor)
        {
            List<Color> newColors = new List<Color>();
            List<float> newPositions = new List<float>();
            Color[] colors = colorBlend.Colors;
            float[] positions = colorBlend.Positions;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            for (int i = 0; i < colors.Length; i++)
            {
                Color color = colors[i];
                float item = positions[i];
                flag2 = color.A == 0;
                if (flag2)
                {
                    flag3 = true;
                }
                if (!flag2 & flag)
                {
                    newColors.Add(glowColor);
                    newPositions.Add(positions[i - 1]);
                }
                else if ((!flag & flag2) && (i != 0))
                {
                    newColors.Add(glowColor);
                    newPositions.Add(item);
                }
                newColors.Add(flag2 ? color : glowColor);
                newPositions.Add(item);
                flag = flag2;
            }
            return (flag3 ? CreateColorBlend(newColors, newPositions) : null);
        }

        private static ColorBlend CreateColorBlend(List<Color> newColors, List<float> newPositions) => 
            new ColorBlend(newColors.Count) { 
                Colors = newColors.ToArray(),
                Positions = newPositions.ToArray()
            };

        private static float[] CreateDashPattern(float[] originalPattern, float coeff1, float coeff2) => 
            CreateDashPattern(originalPattern, coeff1, coeff2, coeff2);

        private static float[] CreateDashPattern(float[] originalPattern, float coeff1, float coeff2, float coeff3)
        {
            int length = originalPattern.Length;
            float[] numArray = new float[] { originalPattern[0] * coeff1 };
            for (int i = 1; i < length; i += 2)
            {
                numArray[i] = originalPattern[i] * coeff2;
            }
            for (int j = 2; j < length; j += 2)
            {
                numArray[j] = originalPattern[j] * coeff3;
            }
            return numArray;
        }

        public static Pen CreatePen(Color color, float width, PenInfo penInfo)
        {
            Pen resultPen = new Pen(color, width);
            if (penInfo != null)
            {
                PrepareDashStyle(resultPen, penInfo.Pen, width);
            }
            resultPen.LineJoin = LineJoin.Round;
            resultPen.StartCap = LineCap.Round;
            resultPen.EndCap = LineCap.Round;
            return resultPen;
        }

        private static void PrepareDashStyle(Pen resultPen, Pen originalPen, float width)
        {
            if (originalPen != null)
            {
                DashStyle dashStyle = originalPen.DashStyle;
                if (dashStyle != DashStyle.Solid)
                {
                    DashCap dashCap = originalPen.DashCap;
                    resultPen.DashStyle = DashStyle.Custom;
                    resultPen.DashCap = dashCap;
                    float coeff = originalPen.Width / width;
                    if (dashCap == DashCap.Round)
                    {
                        PrepareDashStyleForRoundCaps(resultPen, coeff, dashStyle, originalPen.DashPattern);
                    }
                    else
                    {
                        PrepareDashStyleForSquareCaps(resultPen, coeff, dashStyle, originalPen.DashPattern);
                    }
                }
            }
        }

        private static void PrepareDashStyleForRoundCaps(Pen resultPen, float coeff, DashStyle originalDashStyle, float[] originalPattern)
        {
            float num2;
            float num3;
            float num = 1f - coeff;
            if (originalDashStyle == DashStyle.Dot)
            {
                num3 = Math.Max((float) (coeff - num), (float) 1.192093E-07f);
                num2 = num3;
            }
            else if (originalDashStyle == DashStyle.Dash)
            {
                num3 = Math.Max((float) (coeff - num), (float) 1.192093E-07f);
                num2 = coeff + (num / 3f);
            }
            else
            {
                num3 = Math.Max((float) (coeff - (num / 3f)), (float) 1.192093E-07f);
                num2 = coeff + ((1f - num3) / ((5.3f * originalPattern[0]) / 4f));
            }
            resultPen.DashOffset = num;
            resultPen.DashPattern = CreateDashPattern(originalPattern, num2, num3);
        }

        private static void PrepareDashStyleForSquareCaps(Pen resultPen, float coeff, DashStyle originalDashStyle, float[] originalPattern)
        {
            float num2;
            float num3;
            float num4;
            float num = (1f - coeff) / 2f;
            if (originalDashStyle == DashStyle.Dot)
            {
                num2 = coeff + ((3f * num) / 2f);
                num3 = Math.Max((float) (coeff - ((3f * num) / 2f)), (float) 1.192093E-07f);
                num4 = num3;
            }
            else if (originalDashStyle == DashStyle.Dash)
            {
                num2 = coeff + ((2f * num) / 3f);
                num3 = Math.Max((float) (coeff - (2f * num)), (float) 1.192093E-07f);
                num4 = num3;
            }
            else
            {
                num3 = Math.Max((float) (coeff - (num / 3f)), (float) 1.192093E-07f);
                num2 = coeff + ((1f - num3) / (2f * originalPattern[0]));
                num4 = coeff + num;
            }
            resultPen.DashOffset = num;
            resultPen.DashPattern = CreateDashPattern(originalPattern, num2, num3, num4);
        }
    }
}

