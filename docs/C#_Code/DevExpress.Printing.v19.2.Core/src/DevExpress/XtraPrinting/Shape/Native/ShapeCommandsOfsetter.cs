namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShapeCommandsOfsetter : ShapeCommandsTransformer
    {
        private PointF offset;

        public ShapeCommandsOfsetter(PointF offset) : base(PointF.Empty)
        {
            this.offset = offset;
        }

        protected override void TransformPoint(ShapePointsCommand command, SizeF vector, int i)
        {
            PointF tf = command.Points[i];
            command.Points[i] = new PointF(tf.X + this.offset.X, tf.Y + this.offset.Y);
        }
    }
}

