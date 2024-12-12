namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class ReflectionRenderer
    {
        private static ColorBlend CreateColorBlend(byte startAlpha, byte endAlpha, double startPosition, double endPosition)
        {
            List<float> list = new List<float>();
            List<Color> list2 = new List<Color>();
            list.Add(0f);
            if (startPosition != 0.0)
            {
                list.Add((float) startPosition);
                list2.Add(Color.FromArgb(0xff, 0, 0, 0));
            }
            list2.Add(Color.FromArgb(startAlpha, 0, 0, 0));
            list2.Add(Color.FromArgb(endAlpha, 0, 0, 0));
            if (endPosition != 1.0)
            {
                list.Add((float) endPosition);
                list2.Add(Color.FromArgb(0, 0, 0, 0));
            }
            list.Add(1f);
            return new ColorBlend(list.Count) { 
                Colors = list2.ToArray(),
                Positions = list.ToArray()
            };
        }

        private static Bitmap CreateGradientLayer(int width, int height, byte startAlpha, byte endAlpha, double startPosition, double endPosition, float fadeDirection)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), fadeDirection))
            {
                brush.InterpolationColors = CreateColorBlend(startAlpha, endAlpha, startPosition, endPosition);
                Bitmap image = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.FillRectangle(brush, 0, 0, width, height);
                }
                return image;
            }
        }

        internal static void ProcessReflection(Bitmap reflection, ReflectionOpacityInfo opacityInfo)
        {
            byte startAlpha = Convert.ToByte((double) (DrawingValueConverter.FromPercentage(opacityInfo.StartOpacity) * 255.0));
            using (Bitmap bitmap = CreateGradientLayer(reflection.Width, reflection.Height, startAlpha, Convert.ToByte((double) (DrawingValueConverter.FromPercentage(opacityInfo.EndOpacity) * 255.0)), DrawingValueConverter.FromPercentage(opacityInfo.StartPosition), DrawingValueConverter.FromPercentage(opacityInfo.EndPosition), (float) DrawingValueConverter.FromPositiveFixedAngle(opacityInfo.FadeDirection)))
            {
                ProcessReflectionCore(reflection, bitmap);
            }
        }

        [SecuritySafeCritical]
        private static void ProcessReflectionCore(Bitmap reflection, Bitmap gradient)
        {
            BitmapData bitmapdata = reflection.LockBits(new Rectangle(0, 0, reflection.Width, reflection.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData data2 = gradient.LockBits(new Rectangle(0, 0, gradient.Width, gradient.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bitmapdata.Stride * reflection.Height;
            byte[] valuesReflection = new byte[length];
            byte[] valuesGradient = new byte[length];
            IntPtr source = bitmapdata.Scan0;
            IntPtr ptr2 = data2.Scan0;
            Marshal.Copy(source, valuesReflection, 0, length);
            Marshal.Copy(ptr2, valuesGradient, 0, length);
            GdipExtensions.IterateBitmapBytes(length, delegate (int i) {
                byte num = valuesGradient[i + 3];
                byte num2 = valuesReflection[i + 3];
                valuesReflection[i + 3] = Convert.ToByte((float) ((((float) num2) / 255f) * num));
            });
            Marshal.Copy(valuesReflection, 0, source, length);
            reflection.UnlockBits(bitmapdata);
            gradient.UnlockBits(data2);
        }
    }
}

