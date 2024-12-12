namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class CriticalPointsCalculator : CompositeCommandsVisitor
    {
        private float maxX = float.MinValue;
        private float maxY = float.MinValue;
        private float minX = float.MaxValue;
        private float minY = float.MaxValue;

        private void UpdateCriticalValues(ShapePointsCommand command)
        {
            foreach (PointF tf in command.Points)
            {
                this.maxX = Math.Max(this.maxX, tf.X);
                this.minX = Math.Min(this.minX, tf.X);
                this.maxY = Math.Max(this.maxY, tf.Y);
                this.minY = Math.Min(this.minY, tf.Y);
            }
        }

        public override void VisitShapeBezierCommand(ShapeBezierCommand command)
        {
            this.UpdateCriticalValues(command);
        }

        public override void VisitShapeLineCommand(ShapeLineCommand command)
        {
            this.UpdateCriticalValues(command);
        }

        public float MaxX =>
            this.maxX;

        public float MaxY =>
            this.maxY;

        public float MinX =>
            this.minX;

        public float MinY =>
            this.minY;
    }
}

