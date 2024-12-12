namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;

    public class BlurEffectRenderer : BlurEffectRendererBase
    {
        public static Bitmap ApplyBlur(Bitmap bitmap, int blurRadius)
        {
            bitmap = new BlurEffectRenderer().ApplyBlur(bitmap, bitmap.Width, bitmap.Height, blurRadius);
            return bitmap;
        }

        public static Bitmap ApplyReflectionBlur(Bitmap bitmap, int blurRadius) => 
            new BlurEffectRenderer().EnlargeAndBlurBitmap(bitmap, blurRadius);

        protected override void BoxBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            int size = source.Length / 4;
            this.Premultiply(source, size);
            base.BoxBlur(source, target, width, height, stride, radius, radiusFrac);
            this.Unpremultiply(source, size);
        }

        protected override unsafe void BoxHorizontalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            Parallel.For(0, height, delegate (int row) {
                int[] sum = new int[4];
                int[] numArray2 = new int[4];
                int[] numArray3 = new int[4];
                int position = row * stride;
                int num2 = position;
                int num3 = position + (radius * 4);
                for (int i = 0; i < 4; i++)
                {
                    numArray2[i] = source[position + i];
                    numArray3[i] = source[(position + ((width - 1) * 4)) + i];
                    sum[i] = (radius + 1) * numArray2[i];
                }
                int num5 = 0;
                while (num5 < radius)
                {
                    int num6 = position + (num5 * 4);
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            num5++;
                            break;
                        }
                        int* numPtr1 = &(sum[index]);
                        numPtr1[0] += source[num6 + index];
                        index++;
                    }
                }
                int num8 = 0;
                while (num8 <= radius)
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num8++;
                            num3 += 4;
                            position += 4;
                            break;
                        }
                        int* numPtr2 = &(sum[index]);
                        numPtr2[0] += source[num3 + index] - numArray2[index];
                        index++;
                    }
                }
                int num10 = radius + 1;
                while (num10 < (width - radius))
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num10++;
                            num2 += 4;
                            num3 += 4;
                            position += 4;
                            break;
                        }
                        int* numPtr3 = &(sum[index]);
                        numPtr3[0] += source[num3 + index] - source[num2 + index];
                        index++;
                    }
                }
                int num12 = width - radius;
                while (num12 < width)
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num12++;
                            num2 += 4;
                            position += 4;
                            break;
                        }
                        int* numPtr4 = &(sum[index]);
                        numPtr4[0] += numArray3[index] - source[num2 + index];
                        index++;
                    }
                }
            });
        }

        protected override unsafe void BoxVerticalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            Parallel.For(0, width, delegate (int column) {
                int[] sum = new int[4];
                int[] numArray2 = new int[4];
                int[] numArray3 = new int[4];
                int position = column * 4;
                int num2 = position;
                int num3 = position + (radius * stride);
                for (int i = 0; i < 4; i++)
                {
                    numArray2[i] = source[position + i];
                    numArray3[i] = source[(position + ((height - 1) * stride)) + i];
                    sum[i] = (radius + 1) * numArray2[i];
                }
                int num5 = 0;
                while (num5 < radius)
                {
                    int num6 = position + (num5 * stride);
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            num5++;
                            break;
                        }
                        int* numPtr1 = &(sum[index]);
                        numPtr1[0] += source[num6 + index];
                        index++;
                    }
                }
                int num8 = 0;
                while (num8 <= radius)
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num8++;
                            num3 += stride;
                            position += stride;
                            break;
                        }
                        int* numPtr2 = &(sum[index]);
                        numPtr2[0] += source[num3 + index] - numArray2[index];
                        index++;
                    }
                }
                int num10 = radius + 1;
                while (num10 < (height - radius))
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num10++;
                            num2 += stride;
                            num3 += stride;
                            position += stride;
                            break;
                        }
                        int* numPtr3 = &(sum[index]);
                        numPtr3[0] += source[num3 + index] - source[num2 + index];
                        index++;
                    }
                }
                int num12 = height - radius;
                while (num12 < height)
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= 4)
                        {
                            this.SetPixel(target, position, sum, radiusFrac);
                            num12++;
                            num2 += stride;
                            position += stride;
                            break;
                        }
                        int* numPtr4 = &(sum[index]);
                        numPtr4[0] += numArray3[index] - source[num2 + index];
                        index++;
                    }
                }
            });
        }

        private Bitmap EnlargeAndBlurBitmap(Bitmap bitmap, int blurRadius)
        {
            int x = blurRadius;
            int width = bitmap.Width;
            int height = bitmap.Height;
            int num4 = width + (2 * x);
            int num5 = height + (2 * x);
            Bitmap image = new Bitmap(num4, num5, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(bitmap, new Rectangle(x, x, width, height));
            }
            bitmap.Dispose();
            return base.ApplyBlur(image, num4, num5, blurRadius);
        }

        private void Premultiply(byte[] bytes, int size)
        {
            Parallel.For(0, size, delegate (int current) {
                int position = current * 4;
                byte num2 = bytes[position + 3];
                if (num2 > 0)
                {
                    this.SetColors(bytes, position, ((double) num2) / 255.0);
                }
            });
        }

        private void SetColors(byte[] bytes, int position, double coeff)
        {
            for (int i = 0; i < 3; i++)
            {
                bytes[position + i] = base.GetColorValue(bytes[position + i], coeff);
            }
        }

        private void SetPixel(byte[] target, int position, int[] sum, double coeff)
        {
            int colorValue = sum[3];
            if (colorValue > 0)
            {
                byte red = base.GetColorValue(sum[2], coeff);
                base.SetPixelCore(target, position, red, base.GetColorValue(sum[1], coeff), base.GetColorValue(sum[0], coeff), base.GetColorValue(colorValue, coeff));
            }
        }

        private void Unpremultiply(byte[] bytes, int size)
        {
            Parallel.For(0, size, delegate (int current) {
                int position = current * 4;
                byte num2 = bytes[position + 3];
                if (num2 > 0)
                {
                    this.SetColors(bytes, position, 255.0 / ((double) num2));
                }
            });
        }
    }
}

