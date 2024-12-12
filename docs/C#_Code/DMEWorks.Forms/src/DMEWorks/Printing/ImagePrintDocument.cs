namespace DMEWorks.Printing
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;

    public class ImagePrintDocument : PrintDocument
    {
        private readonly Image[] _images;
        private int _pageIndex;

        private ImagePrintDocument(Image image)
        {
            base.DefaultPageSettings.Landscape = image.Height < image.Width;
            if (!image.RawFormat.Equals(ImageFormat.Tiff))
            {
                this._images = new Image[] { new Bitmap(image) };
            }
            else
            {
                int frameCount = image.GetFrameCount(FrameDimension.Page);
                if (frameCount == 1)
                {
                    this._images = new Image[] { new Bitmap(image) };
                }
                else
                {
                    this._images = new Image[frameCount];
                    for (int i = 0; i < frameCount; i++)
                    {
                        image.SelectActiveFrame(FrameDimension.Page, i);
                        this._images[i] = new Bitmap(image);
                    }
                }
            }
            base.PrinterSettings.MinimumPage = 1;
            base.PrinterSettings.FromPage = 1;
            base.PrinterSettings.ToPage = this._images.Length;
            base.PrinterSettings.MaximumPage = this._images.Length;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = 0; i < this._images.Length; i++)
                {
                    DisposeImage(ref this._images[i]);
                }
            }
            base.Dispose(disposing);
        }

        private static void DisposeImage(ref Image image)
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }

        public static ImagePrintDocument FromArray(byte[] array)
        {
            ImagePrintDocument document;
            using (MemoryStream stream = new MemoryStream(array, false))
            {
                using (Image image = Image.FromStream(stream))
                {
                    document = new ImagePrintDocument(image);
                }
            }
            return document;
        }

        public static ImagePrintDocument FromFile(string filename)
        {
            using (Image image = Image.FromFile(filename))
            {
                return new ImagePrintDocument(image);
            }
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            this._pageIndex = base.PrinterSettings.FromPage;
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            if (this._pageIndex <= this._images.Length)
            {
                Image image = this._images[this._pageIndex - 1];
                if (image != null)
                {
                    e.Graphics.DrawImage(image, e.MarginBounds);
                }
                this._pageIndex++;
            }
            e.HasMorePages = this._pageIndex <= Math.Min(base.PrinterSettings.ToPage, this._images.Length);
        }
    }
}

