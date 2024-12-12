namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class BitmapCacheBrushBuilder : BrushBuilder<BitmapCacheBrush>
    {
        public BitmapCacheBrushBuilder(BitmapCacheBrush brush) : base(brush)
        {
        }

        private bool AllowLayoutContent() => 
            base.Brush.AutoLayoutContent || (VisualTreeHelper.GetParent(base.Brush.Target) != null);

        public override DXBrush CreateDxBrush(Rect brushBounds)
        {
            Bitmap image = this.CreateImage(brushBounds);
            if (image == null)
            {
                return null;
            }
            DXTextureBrush brush1 = new DXTextureBrush(image, DXWrapMode.Clamp);
            brush1.Transform = this.GetTransform(new System.Windows.Size((double) image.Width, (double) image.Height), brushBounds);
            return brush1;
        }

        protected Bitmap CreateImage(Rect brushBounds)
        {
            Rect bounds = this.GetBounds();
            if (!bounds.IsValid() || !this.AllowLayoutContent())
            {
                return null;
            }
            RenderTargetBitmap source = new RenderTargetBitmap((int) bounds.Width, (int) bounds.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                System.Windows.Point location = new System.Windows.Point();
                context.DrawRectangle(base.Brush, null, new Rect(location, bounds.Size));
            }
            source.Render(visual);
            return BitmapSourceBuilder.CreateBitmap(source);
        }

        private Rect GetBounds() => 
            PdfExportHelper.GetVisualBounds(base.Brush.Target);

        private DXTransformationMatrix GetTransform(System.Windows.Size imageSize, Rect brushBounds)
        {
            PointF[] plgpts = new PointF[] { brushBounds.TopLeft.ToPointF(), brushBounds.TopRight.ToPointF(), brushBounds.BottomLeft.ToPointF() };
            return new System.Drawing.Drawing2D.Matrix(new RectangleF(new PointF(0f, 0f), imageSize.ToSizeF()), plgpts).ToDXMatrix();
        }
    }
}

