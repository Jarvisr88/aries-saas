namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ImageBrushBuilder : TileBrushBuilder<ImageBrush>
    {
        public ImageBrushBuilder(ImageBrush brush) : base(brush)
        {
        }

        protected override VectorTileInfo CreateVectorTileInfo()
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawImage(base.Brush.ImageSource, new Rect(new Point(0.0, 0.0), this.GetContentSize()));
            }
            return new VectorTileInfo(visual, Matrix.Identity);
        }

        protected override Size GetContentSize() => 
            new Size(base.Brush.ImageSource.Width, base.Brush.ImageSource.Height);

        protected override Rect DrawingBounds =>
            new Rect(0.0, 0.0, base.Brush.ImageSource.Width, base.Brush.ImageSource.Height);
    }
}

