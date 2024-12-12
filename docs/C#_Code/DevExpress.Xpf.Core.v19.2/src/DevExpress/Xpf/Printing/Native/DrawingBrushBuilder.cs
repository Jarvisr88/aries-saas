namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class DrawingBrushBuilder : TileBrushBuilder<DrawingBrush>
    {
        public DrawingBrushBuilder(DrawingBrush brush) : base(brush)
        {
        }

        protected override VectorTileInfo CreateVectorTileInfo()
        {
            Rect drawingBounds = this.DrawingBounds;
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.PushTransform(new TranslateTransform(-drawingBounds.X, -drawingBounds.Y));
                context.DrawDrawing(base.Brush.Drawing);
                context.Pop();
            }
            return new VectorTileInfo(visual, Matrix.Identity);
        }

        protected override Size GetContentSize() => 
            this.DrawingBounds.Size;

        protected override Rect DrawingBounds =>
            base.Brush.Drawing.Bounds;
    }
}

