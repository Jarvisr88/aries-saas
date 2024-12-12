namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public abstract class BlurEffectRendererBase
    {
        protected const int blueChannel = 0;
        protected const int greenChannel = 1;
        protected const int redChannel = 2;
        protected const int alphaChannel = 3;
        protected const int channelsCount = 4;

        protected BlurEffectRendererBase()
        {
        }

        protected Bitmap ApplyBlur(Bitmap bitmap, int width, int height, int blurRadius) => 
            this.ApplyBlur(bitmap, width, height, blurRadius, 1.0);

        protected Bitmap ApplyBlur(Bitmap bitmap, int width, int height, int blurRadius, double blurRadiusCoeff) => 
            this.ApplyGaussianBlurCore(bitmap, width, height, blurRadius, blurRadiusCoeff);

        [SecuritySafeCritical]
        private Bitmap ApplyGaussianBlurCore(Bitmap bitmap, int width, int height, int blurRadius, double blurRadiusCoeff)
        {
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int size = (4 * width) * height;
            byte[] content = this.GetContent(data, size);
            int stride = data.Stride;
            double radiusFrac = blurRadiusCoeff / ((double) ((2 * blurRadius) + 1));
            this.BoxBlur(content, this.GetContent(data, size), width, height, stride, blurRadius, radiusFrac);
            Marshal.Copy(content, 0, data.Scan0, content.Length);
            bitmap.UnlockBits(data);
            return bitmap;
        }

        protected virtual void BoxBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac)
        {
            this.BoxHorizontalBlur(source, target, width, height, stride, radius, radiusFrac);
            this.BoxVerticalBlur(target, source, width, height, stride, radius, radiusFrac);
        }

        protected abstract void BoxHorizontalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac);
        protected abstract void BoxVerticalBlur(byte[] source, byte[] target, int width, int height, int stride, int radius, double radiusFrac);
        protected byte GetColorValue(int colorValue, double coeff)
        {
            double num = Math.Round((double) (colorValue * coeff));
            return ((num <= 255.0) ? ((num >= 0.0) ? ((byte) num) : 0) : 0xff);
        }

        [SecuritySafeCritical]
        protected byte[] GetContent(BitmapData data, int size)
        {
            byte[] destination = new byte[size];
            Marshal.Copy(data.Scan0, destination, 0, destination.Length);
            return destination;
        }

        protected void SetPixelCore(byte[] target, int position, byte red, byte green, byte blue, byte alpha)
        {
            target[position + 2] = red;
            target[position + 1] = green;
            target[position] = blue;
            target[position + 3] = alpha;
        }
    }
}

