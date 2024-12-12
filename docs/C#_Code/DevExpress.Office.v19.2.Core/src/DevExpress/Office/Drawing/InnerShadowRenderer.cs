namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class InnerShadowRenderer
    {
        [SecuritySafeCritical]
        public static void ApplyShadow(Bitmap figure, Bitmap shadowOpacityMask, Color originalShadowColor)
        {
            Rectangle rect = new Rectangle(0, 0, figure.Width, figure.Height);
            BitmapData bitmapdata = figure.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData data2 = shadowOpacityMask.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bitmapdata.Stride * bitmapdata.Height;
            byte[] valuesFigure = new byte[length];
            byte[] valuesShadowOpacityMask = new byte[length];
            Marshal.Copy(bitmapdata.Scan0, valuesFigure, 0, length);
            Marshal.Copy(data2.Scan0, valuesShadowOpacityMask, 0, length);
            GdipExtensions.IterateBitmapBytes(length, delegate (int i) {
                Color fillColor = GetColor(valuesFigure, i);
                if (fillColor.A > 0)
                {
                    Color color2 = GetResultColor(fillColor, originalShadowColor, valuesShadowOpacityMask[i + 3]);
                    if (color2.A > 0)
                    {
                        valuesFigure[i] = color2.B;
                        valuesFigure[i + 1] = color2.G;
                        valuesFigure[i + 2] = color2.R;
                        valuesFigure[i + 3] = color2.A;
                    }
                }
            });
            Marshal.Copy(valuesFigure, 0, bitmapdata.Scan0, length);
            figure.UnlockBits(bitmapdata);
            shadowOpacityMask.UnlockBits(data2);
        }

        private static Color CalculateMixedFillColor(Color fillColor, Color shadowColor)
        {
            byte a = fillColor.A;
            byte alpha = Convert.ToByte((float) ((a - (((float) (a * a)) / 255f)) * (((float) shadowColor.A) / 255f)));
            Color foreground = MixColors(fillColor, Color.FromArgb(alpha, shadowColor));
            return ConvertRgbaToRgb(Color.White, foreground);
        }

        private static Color CalculateMixedShadowColor(Color fillColor, Color shadowColor)
        {
            byte alpha = Convert.ToByte((float) (((float) (fillColor.A * shadowColor.A)) / 255f));
            Color foreground = MixColors(fillColor, Color.FromArgb(alpha, shadowColor));
            return ConvertRgbaToRgb(Color.White, foreground);
        }

        private static Color ConvertRgbaToRgb(Color background, Color foreground)
        {
            float num = ((float) foreground.A) / 255f;
            return CreateColor(1f, Math.Round((double) ((background.R * (1f - num)) + (foreground.R * num))), Math.Round((double) ((background.G * (1f - num)) + (foreground.G * num))), Math.Round((double) ((background.B * (1f - num)) + (foreground.B * num))));
        }

        private static Color ConvertRgbToRgba(Color color, byte fillAlpha)
        {
            int num = 0xff - Math.Min(color.R, Math.Min(color.B, color.G));
            float a = ((float) Math.Max(fillAlpha, num)) / 255f;
            return CreateColor(a, Math.Round((double) (255f - (((float) (0xff - color.R)) / a))), Math.Round((double) (255f - (((float) (0xff - color.G)) / a))), Math.Round((double) (255f - (((float) (0xff - color.B)) / a))));
        }

        private static Color CreateColor(float a, double r, double g, double b) => 
            Color.FromArgb(Convert.ToByte((float) (a * 255f)), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));

        private static Color GetColor(byte[] array, int i) => 
            Color.FromArgb(array[i + 3], array[i + 2], array[i + 1], array[i]);

        private static Color GetResultColor(Color fillColor, Color shadowColor, byte opacity)
        {
            Color baseColor = CalculateMixedShadowColor(fillColor, shadowColor);
            Color color = ConvertRgbaToRgb(CalculateMixedFillColor(fillColor, shadowColor), Color.FromArgb(opacity, baseColor));
            byte a = fillColor.A;
            if ((a > 0) && (a < 0xff))
            {
                color = ConvertRgbToRgba(color, a);
            }
            return color;
        }

        private static Color MixColors(Color background, Color foreground)
        {
            float num = ((float) background.A) / 255f;
            float num2 = ((float) foreground.A) / 255f;
            float a = 1f - ((1f - num) * (1f - num2));
            return ((a != 0f) ? CreateColor(a, Math.Round((double) (((foreground.R * num2) + ((background.R * num) * (1f - num2))) / a)), Math.Round((double) (((foreground.G * num2) + ((background.G * num) * (1f - num2))) / a)), Math.Round((double) (((foreground.B * num2) + ((background.B * num) * (1f - num2))) / a))) : Color.Empty);
        }
    }
}

