namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;

    public abstract class CompositeCommandsVisitor : IShapeCommandVisitor
    {
        protected CompositeCommandsVisitor()
        {
        }

        void IShapeCommandVisitor.VisitShapeDrawPathCommand(ShapeDrawPathCommand command)
        {
            this.HandlePathCommand(command);
        }

        void IShapeCommandVisitor.VisitShapeFillPathCommand(ShapeFillPathCommand command)
        {
            this.HandlePathCommand(command);
        }

        private void HandlePathCommand(ShapePathCommand command)
        {
            command.Commands.Iterate(this);
        }

        public abstract void VisitShapeBezierCommand(ShapeBezierCommand command);
        public abstract void VisitShapeLineCommand(ShapeLineCommand command);
    }
}

