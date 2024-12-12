namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ImageDrawingBuilder : DrawingBuilder<ImageDrawing>
    {
        public ImageDrawingBuilder(ImageDrawing drawing) : base(drawing)
        {
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            if (this.IsImageValid())
            {
                constructor.DoWithState(delegate {
                    ImagesSourceBuilder builder = ImagesSourceBuilder.Create(this.Drawing.ImageSource);
                    if (builder != null)
                    {
                        builder.GenerateData(constructor);
                    }
                }, this.GetImageTransform(), null);
            }
        }

        private double GetDpiScale(bool scaleY)
        {
            BitmapImage imageSource = base.Drawing.ImageSource as BitmapImage;
            return ((imageSource == null) ? 1.0 : (96.0 / (scaleY ? imageSource.DpiY : imageSource.DpiX)));
        }

        private Transform GetImageTransform()
        {
            double num2 = base.Drawing.Bounds.Height / base.Drawing.ImageSource.Height;
            return new MatrixTransform((base.Drawing.Bounds.Width / base.Drawing.ImageSource.Width) * this.GetDpiScale(false), 0.0, 0.0, num2 * this.GetDpiScale(true), base.Drawing.Bounds.Left, base.Drawing.Bounds.Top);
        }

        private bool IsImageValid() => 
            (base.Drawing.ImageSource != null) && ((base.Drawing.ImageSource.Width > 0.0) && (base.Drawing.ImageSource.Height > 0.0));
    }
}

