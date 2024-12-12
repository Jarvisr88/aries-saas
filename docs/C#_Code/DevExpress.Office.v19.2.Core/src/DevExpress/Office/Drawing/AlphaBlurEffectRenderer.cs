namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class AlphaBlurEffectRenderer : BlurEffectRendererBase
    {
        protected override void BoxHorizontalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            Parallel.For(0, height, delegate (int row) {
                int position = row * stride;
                int num2 = position;
                int num3 = position + (radius * 4);
                int num4 = source[position + 3];
                int num5 = source[(position + ((width - 1) * 4)) + 3];
                int alphaSum = (radius + 1) * num4;
                for (int i = 0; i < radius; i++)
                {
                    alphaSum += source[(position + (i * 4)) + 3];
                }
                int num8 = 0;
                while (num8 <= radius)
                {
                    alphaSum += source[num3 + 3] - num4;
                    this.SetPixelHorizontal(target, position, alphaSum, radiusFrac);
                    num8++;
                    num3 += 4;
                    position += 4;
                }
                int num9 = radius + 1;
                while (num9 < (width - radius))
                {
                    alphaSum += source[num3 + 3] - source[num2 + 3];
                    this.SetPixelHorizontal(target, position, alphaSum, radiusFrac);
                    num9++;
                    num2 += 4;
                    num3 += 4;
                    position += 4;
                }
                int num10 = width - radius;
                while (num10 < width)
                {
                    alphaSum += num5 - source[num2 + 3];
                    this.SetPixelHorizontal(target, position, alphaSum, radiusFrac);
                    num10++;
                    num2 += 4;
                    position += 4;
                }
            });
        }

        protected override void BoxVerticalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            Parallel.For(0, width, delegate (int column) {
                int position = column * 4;
                int num2 = position;
                int num3 = position + (radius * stride);
                int num4 = source[position + 3];
                int num5 = source[(position + ((height - 1) * stride)) + 3];
                int alphaSum = (radius + 1) * num4;
                for (int i = 0; i < radius; i++)
                {
                    alphaSum += source[(position + (i * stride)) + 3];
                }
                int num8 = 0;
                while (num8 <= radius)
                {
                    alphaSum += source[num3 + 3] - num4;
                    this.SetPixelVertical(target, position, alphaSum, radiusFrac);
                    num8++;
                    num3 += stride;
                    position += stride;
                }
                int num9 = radius + 1;
                while (num9 < (height - radius))
                {
                    alphaSum += source[num3 + 3] - source[num2 + 3];
                    this.SetPixelVertical(target, position, alphaSum, radiusFrac);
                    num9++;
                    num2 += stride;
                    num3 += stride;
                    position += stride;
                }
                int num10 = height - radius;
                while (num10 < height)
                {
                    alphaSum += num5 - source[num2 + 3];
                    this.SetPixelVertical(target, position, alphaSum, radiusFrac);
                    num10++;
                    num2 += stride;
                    position += stride;
                }
            });
        }

        private Bitmap DrawBlur(Bitmap bitmap, int blurRadius, System.Drawing.Color color, double blurRadiusCoeff)
        {
            this.Color = color;
            bitmap = base.ApplyBlur(bitmap, bitmap.Width, bitmap.Height, blurRadius, blurRadiusCoeff);
            return bitmap;
        }

        private Bitmap DrawInnerShadowBlur(Bitmap bitmap, Size sourceSize, int blurRadius, System.Drawing.Color color)
        {
            this.Color = color;
            int width = bitmap.Width;
            bitmap = base.ApplyBlur(bitmap, width, bitmap.Height, blurRadius);
            Bitmap image = new Bitmap(sourceSize.Width, sourceSize.Height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(bitmap, new Point(-blurRadius, -blurRadius));
            }
            bitmap.Dispose();
            return image;
        }

        private Bitmap EnlargeAndDrawBlur(Bitmap bitmap, int blurRadius, System.Drawing.Color color, double blurRadiusCoeff)
        {
            int x = blurRadius;
            Bitmap image = new Bitmap(bitmap.Width + (2 * x), bitmap.Height + (2 * x), PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(bitmap, new Point(x, x));
            }
            bitmap.Dispose();
            return this.DrawBlur(image, blurRadius, color, blurRadiusCoeff);
        }

        public static Bitmap GlowBlur(Bitmap bitmap, int blurRadiusInPixels, System.Drawing.Color color) => 
            new AlphaBlurEffectRenderer().DrawBlur(bitmap, (int) (blurRadiusInPixels * 0.8), color, 1.4);

        public static Bitmap InnerShadowBlur(Bitmap bitmap, Size sourceSizeInPixels, int blurRadiusInPixels, System.Drawing.Color color) => 
            new AlphaBlurEffectRenderer().DrawInnerShadowBlur(bitmap, sourceSizeInPixels, blurRadiusInPixels, color);

        protected virtual void SetPixelHorizontal(byte[] target, int position, int alphaSum, double coeff)
        {
            if (alphaSum > 0)
            {
                target[position + 3] = base.GetColorValue(alphaSum, coeff);
            }
        }

        protected virtual void SetPixelVertical(byte[] target, int position, int alphaSum, double coeff)
        {
            if (alphaSum > 0)
            {
                byte alpha = Math.Min(base.GetColorValue(alphaSum, coeff), this.Color.A);
                base.SetPixelCore(target, position, this.Color.R, this.Color.G, this.Color.B, alpha);
            }
        }

        public static Bitmap ShadowBlur(Bitmap bitmap, int blurRadiusInPixels, System.Drawing.Color color) => 
            new AlphaBlurEffectRenderer().EnlargeAndDrawBlur(bitmap, blurRadiusInPixels, color, 1.0);

        protected System.Drawing.Color Color { get; set; }
    }
}

