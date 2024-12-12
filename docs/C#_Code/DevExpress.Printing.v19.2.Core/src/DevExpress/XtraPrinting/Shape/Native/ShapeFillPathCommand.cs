namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapeFillPathCommand : ShapePathCommand
    {
        public ShapeFillPathCommand(ShapePointsCommandCollection commands) : base(commands)
        {
        }

        public override void Accept(IShapeCommandVisitor visitor)
        {
            visitor.VisitShapeFillPathCommand(this);
        }
    }
}

