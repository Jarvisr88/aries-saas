namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal static class PreviewImageHelper
    {
        private static double CalculatePreviewScale(double width, double height, Page page)
        {
            System.Windows.Size previewPageSize = GetPreviewPageSize(page);
            return Math.Min((double) (height / previewPageSize.Height), (double) (width / previewPageSize.Width));
        }

        private static ImageSource ConvertToImageSource(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            stream.Seek(0L, SeekOrigin.Begin);
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        internal static ImageSource GetPreview(Page page, System.Windows.Size size, double scaleX, Action afterDrawPagesAction = null)
        {
            double num = CalculatePreviewScale(size.Width, size.Height, page);
            System.Windows.Size previewPageSize = GetPreviewPageSize(page);
            Bitmap image = new Bitmap((int) ((previewPageSize.Width * num) * scaleX), (int) ((previewPageSize.Height * num) * scaleX));
            Graphics gr = Graphics.FromImage(image);
            gr.ScaleTransform((float) num, (float) num);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.PixelOffsetMode = PixelOffsetMode.Half;
            ((IPage) page).Draw(gr, PointF.Empty);
            if (afterDrawPagesAction != null)
            {
                afterDrawPagesAction();
            }
            return ConvertToImageSource(image);
        }

        private static System.Windows.Size GetPreviewPageSize(Page page)
        {
            float num2 = page.PageSize.Height.DocToDip();
            return new System.Windows.Size((double) page.PageSize.Width.DocToDip(), (double) num2);
        }
    }
}

