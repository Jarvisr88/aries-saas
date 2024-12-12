namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows;
    using System.Windows.Media;

    public abstract class TileBrushBuilder<TBrush> : TileBrushBuilder where TBrush: TileBrush
    {
        private readonly TBrush brush;

        protected TileBrushBuilder(TBrush brush)
        {
            this.brush = brush;
        }

        protected abstract VectorTileInfo CreateVectorTileInfo();
        protected abstract System.Windows.Size GetContentSize();
        private PointF[] GetDestinationRectPoints(Rect tileBounds, Rect viewboxBounds)
        {
            System.Windows.Size imageSize = this.GetImageSize(tileBounds.Size, viewboxBounds.Size);
            double x = tileBounds.X + this.GetOffsetX(this.Brush.AlignmentX, imageSize, tileBounds.Size);
            double y = tileBounds.Y + this.GetOffsetY(this.Brush.AlignmentY, imageSize, tileBounds.Size);
            return new PointF[] { new System.Windows.Point(x, y).ToPointF(), new System.Windows.Point(x + imageSize.Width, y).ToPointF(), new System.Windows.Point(x, y + imageSize.Height).ToPointF() };
        }

        private System.Windows.Size GetImageSize(System.Windows.Size tileSize, System.Windows.Size contentSize)
        {
            if (this.Brush.Stretch == Stretch.None)
            {
                return contentSize;
            }
            if (this.Brush.Stretch == Stretch.Fill)
            {
                return tileSize;
            }
            double num = tileSize.Width / contentSize.Width;
            double num2 = tileSize.Height / contentSize.Height;
            double num3 = 0.0;
            num3 = (this.Brush.Stretch != Stretch.Uniform) ? Math.Max(num, num2) : Math.Min(num, num2);
            return new System.Windows.Size(contentSize.Width * num3, contentSize.Height * num3);
        }

        private double GetOffsetX(AlignmentX alignment, System.Windows.Size contentSize, System.Windows.Size tileSize) => 
            (alignment != AlignmentX.Left) ? ((alignment != AlignmentX.Right) ? ((tileSize.Width - contentSize.Width) / 2.0) : (tileSize.Width - contentSize.Width)) : 0.0;

        private double GetOffsetY(AlignmentY alignment, System.Windows.Size contentSize, System.Windows.Size tileSize) => 
            (alignment != AlignmentY.Top) ? ((alignment != AlignmentY.Bottom) ? ((tileSize.Height - contentSize.Height) / 2.0) : (tileSize.Height - contentSize.Height)) : 0.0;

        private Rect GetTileBounds(Rect brushBounds) => 
            (this.Brush.ViewportUnits != BrushMappingMode.Absolute) ? new Rect(brushBounds.Width * this.Brush.Viewport.X, brushBounds.Height * this.Brush.Viewport.Y, brushBounds.Width * this.Brush.Viewport.Width, brushBounds.Height * this.Brush.Viewport.Height) : this.Brush.Viewport;

        protected DXTransformationMatrix GetTransform(Rect tileBounds, Rect viewboxBounds) => 
            new System.Drawing.Drawing2D.Matrix(viewboxBounds.ToRectangleF(), this.GetDestinationRectPoints(tileBounds, viewboxBounds)).ToDXMatrix();

        private Rect GetViewboxBounds(double contentWidth, double contentHeight) => 
            (this.Brush.ViewboxUnits != BrushMappingMode.Absolute) ? new Rect(contentWidth * this.Brush.Viewbox.X, contentHeight * this.Brush.Viewbox.Y, contentWidth * this.Brush.Viewbox.Width, contentHeight * this.Brush.Viewbox.Height) : this.Brush.Viewbox;

        public override void SetDXBrush(Rect brushBounds, PdfGraphicsCommandConstructor parentConstructor)
        {
            if (this.CanSetBrush && brushBounds.IsValid())
            {
                Rect tileBounds = this.GetTileBounds(brushBounds);
                System.Windows.Size contentSize = this.GetContentSize();
                VectorTileInfo vectorTileInfo = this.CreateVectorTileInfo();
                System.Windows.Media.Matrix transform = System.Windows.Media.Matrix.Multiply(this.GetTransform(tileBounds, this.GetViewboxBounds(contentSize.Width, contentSize.Height)).ToWpfMatrix(), vectorTileInfo.Transform);
                PdfVectorBrushContainer container = new PdfVectorBrushContainer(tileBounds.Width, tileBounds.Height, this.Brush.TileMode.ToWrapMode(), this.BrushTransform, constructor => constructor.DoWithState(() => PdfVisualExporter.DrawVisual(constructor, vectorTileInfo.Visual, true), new MatrixTransform(transform), null));
                parentConstructor.SetBrush(container);
            }
        }

        protected TBrush Brush =>
            this.brush;

        protected virtual bool CanSetBrush =>
            this.DrawingBounds.IsValid();

        protected abstract Rect DrawingBounds { get; }

        private DXTransformationMatrix BrushTransform
        {
            get
            {
                System.Windows.Media.Matrix matrix1 = this.brush.Transform.Value;
                return this.brush.Transform.Value.ToDXMatrix();
            }
        }
    }
}

