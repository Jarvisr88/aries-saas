namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public interface IShapeCommandVisitor
    {
        void VisitShapeBezierCommand(ShapeBezierCommand command);
        void VisitShapeDrawPathCommand(ShapeDrawPathCommand command);
        void VisitShapeFillPathCommand(ShapeFillPathCommand command);
        void VisitShapeLineCommand(ShapeLineCommand command);
    }
}

