namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.Drawing;

    public class ShapeLine : ShapeBase
    {
        protected override ShapeBase CloneShape() => 
            new ShapeLine();

        protected internal override ShapeCommandCollection CreateCommands(RectangleF bounds, int angle)
        {
            ShapeLineCommandCollection commands = new ShapeLineCommandCollection();
            PointF tf = RectHelper.CenterOf(bounds);
            commands.AddLine(new PointF(tf.X, bounds.Top), new PointF(tf.X, bounds.Bottom));
            return commands;
        }

        internal override DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId =>
            DevExpress.XtraPrinting.Shape.Native.ShapeId.Line;

        protected internal override bool SupportsFillColor =>
            false;
    }
}

