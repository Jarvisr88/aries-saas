namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.Drawing;

    public class ShapeRectangle : FilletShapeBase
    {
        public ShapeRectangle()
        {
        }

        private ShapeRectangle(ShapeRectangle source) : base(source)
        {
        }

        protected override ShapeBase CloneShape() => 
            new ShapeRectangle(this);

        protected internal override PointF[] CreatePoints(RectangleF bounds, int angle) => 
            ShapeHelper.CreateRectanglePoints(bounds);

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Rectangle;
    }
}

