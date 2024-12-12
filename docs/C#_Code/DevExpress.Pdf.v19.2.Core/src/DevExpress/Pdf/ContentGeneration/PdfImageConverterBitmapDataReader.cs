namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PdfImageConverterBitmapDataReader : PdfImageConverterImageDataReader
    {
        private readonly BitmapData bitmapData;
        private readonly Bitmap bitmap;
        private readonly int length;
        private int position;

        [SecuritySafeCritical]
        internal PdfImageConverterBitmapDataReader(Bitmap bitmap, int componentsCount)
        {
            this.bitmap = bitmap;
            int width = bitmap.Width;
            int height = bitmap.Height;
            this.bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            this.length = height * this.bitmapData.Stride;
        }

        [SecuritySafeCritical]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.bitmap.UnlockBits(this.bitmapData);
            }
        }

        [SecuritySafeCritical]
        public override int ReadNextRow(byte[] buffer, int count)
        {
            int num = this.length - this.position;
            int length = (num > count) ? count : num;
            Marshal.Copy(IntPtr.Add(this.bitmapData.Scan0, this.position), buffer, 0, length);
            this.position += this.bitmapData.Stride;
            return length;
        }
    }
}

