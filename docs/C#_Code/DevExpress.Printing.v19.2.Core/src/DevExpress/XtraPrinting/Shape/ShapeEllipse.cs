namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.Drawing;

    public class ShapeEllipse : ClosedShapeBase
    {
        protected override RectangleF AdjustClientRectangle(RectangleF clientBounds, float lineWidth)
        {
            clientBounds = base.AdjustClientRectangle(clientBounds, lineWidth);
            return new RectangleF(clientBounds.X, clientBounds.Y, Math.Max((float) 0f, (float) (clientBounds.Width - 1f)), Math.Max((float) 0f, (float) (clientBounds.Height - 1f)));
        }

        protected override ShapeBase CloneShape() => 
            new ShapeEllipse();

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle) => 
            ShapeHelper.CreateRectanglePoints(bounds);

        protected override ILinesAdjuster GetLinesAdjuster() => 
            UniformLinesAdjuster.Instance;

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Ellipse;
    }
}

