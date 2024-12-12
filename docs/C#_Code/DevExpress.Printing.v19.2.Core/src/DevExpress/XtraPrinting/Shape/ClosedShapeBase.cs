namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.Drawing;

    public abstract class ClosedShapeBase : ShapeBase
    {
        protected ClosedShapeBase()
        {
        }

        protected override RectangleF AdjustClientRectangle(RectangleF clientBounds, float lineWidth) => 
            DeflateLineWidth(clientBounds, lineWidth);

        protected internal override ShapeCommandCollection CreateCommands(RectangleF bounds, int angle)
        {
            ShapePathCommandCollection commands = new ShapePathCommandCollection();
            commands.Add(new ShapeFillPathCommand(this.CreatePointsCommands(bounds, angle)));
            commands.Add(new ShapeDrawPathCommand(this.CreatePointsCommands(bounds, angle)));
            return commands;
        }

        protected internal abstract PointF[] CreatePoints(RectangleF bounds, int angle);
        private ShapePointsCommandCollection CreatePointsCommands(RectangleF bounds, int angle) => 
            ConsecutiveLineCommandsRounder.CreateRoundedConsecutiveLinesCommands(ShapeHelper.CreateConsecutiveLinesFromPoints(this.CreatePoints(bounds, angle)), (float) this.GetFilletValueInPercents(), this.GetLinesAdjuster());

        protected virtual int GetFilletValueInPercents() => 
            100;

        protected abstract ILinesAdjuster GetLinesAdjuster();
    }
}

