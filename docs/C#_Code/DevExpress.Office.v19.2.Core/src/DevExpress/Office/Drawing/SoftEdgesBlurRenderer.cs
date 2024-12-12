namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public class SoftEdgesBlurRenderer : AlphaBlurEffectRenderer
    {
        private byte[] valuesBitmap;

        public static Bitmap ApplyBlur(Bitmap bitmap, Bitmap opacityMask, int blurRadius) => 
            new SoftEdgesBlurRenderer().DrawCore(bitmap, opacityMask, blurRadius);

        [SecuritySafeCritical]
        private Bitmap DrawCore(Bitmap bitmap, Bitmap opacityMask, int blurRadius)
        {
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bitmapdata.Stride * bitmapdata.Height;
            this.valuesBitmap = new byte[length];
            Marshal.Copy(bitmapdata.Scan0, this.valuesBitmap, 0, length);
            base.ApplyBlur(opacityMask, opacityMask.Width, opacityMask.Height, blurRadius);
            opacityMask.Dispose();
            Marshal.Copy(this.valuesBitmap, 0, bitmapdata.Scan0, length);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        protected override void SetPixelVertical(byte[] target, int position, int alphaSum, double coeff)
        {
            byte num = this.valuesBitmap[position + 3];
            if (num > 0)
            {
                this.valuesBitmap[position + 3] = (byte) ((num * base.GetColorValue(alphaSum, coeff)) / 0xff);
            }
        }
    }
}

