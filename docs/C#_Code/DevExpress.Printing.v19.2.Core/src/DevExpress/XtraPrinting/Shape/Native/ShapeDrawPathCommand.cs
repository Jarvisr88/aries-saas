namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public class ShapeDrawPathCommand : ShapePathCommand
    {
        public ShapeDrawPathCommand(ShapePointsCommandCollection commands) : base(commands)
        {
        }

        public override void Accept(IShapeCommandVisitor visitor)
        {
            visitor.VisitShapeDrawPathCommand(this);
        }
    }
}

