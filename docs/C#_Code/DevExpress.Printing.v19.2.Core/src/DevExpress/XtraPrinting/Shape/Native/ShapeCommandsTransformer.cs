namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public abstract class ShapeCommandsTransformer : CompositeCommandsVisitor
    {
        protected PointF center;

        protected ShapeCommandsTransformer(PointF center)
        {
            this.center = center;
        }

        protected abstract void TransformPoint(ShapePointsCommand command, SizeF vector, int i);
        private void TransformPointsCommand(ShapePointsCommand command)
        {
            int length = command.Points.Length;
            for (int i = 0; i < length; i++)
            {
                PointF tf = command.Points[i];
                this.TransformPoint(command, new SizeF(tf.X - this.center.X, tf.Y - this.center.Y), i);
            }
        }

        public override void VisitShapeBezierCommand(ShapeBezierCommand command)
        {
            this.TransformPointsCommand(command);
        }

        public override void VisitShapeLineCommand(ShapeLineCommand command)
        {
            this.TransformPointsCommand(command);
        }
    }
}

