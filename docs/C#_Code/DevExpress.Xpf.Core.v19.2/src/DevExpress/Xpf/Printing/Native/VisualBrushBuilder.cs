namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class VisualBrushBuilder : TileBrushBuilder<VisualBrush>
    {
        public VisualBrushBuilder(VisualBrush brush) : base(brush)
        {
        }

        protected override VectorTileInfo CreateVectorTileInfo()
        {
            Rect drawingBounds = this.DrawingBounds;
            return new VectorTileInfo(base.Brush.Visual, new Matrix(1.0, 0.0, 0.0, 1.0, -drawingBounds.X, -drawingBounds.Y));
        }

        protected override Size GetContentSize() => 
            PdfExportHelper.GetVisualBounds(base.Brush.Visual).Size;

        protected override Rect DrawingBounds =>
            PdfExportHelper.GetVisualBounds(base.Brush.Visual);

        protected override bool CanSetBrush =>
            base.CanSetBrush && (base.Brush.AutoLayoutContent || (VisualTreeHelper.GetParent(base.Brush.Visual) != null));
    }
}

