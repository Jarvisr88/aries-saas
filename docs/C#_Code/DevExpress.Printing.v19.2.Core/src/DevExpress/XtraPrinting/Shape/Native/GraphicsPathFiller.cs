namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing.Drawing2D;

    public class GraphicsPathFiller : IShapeCommandVisitor, IDisposable
    {
        private GraphicsPath path = new GraphicsPath();

        void IShapeCommandVisitor.VisitShapeBezierCommand(ShapeBezierCommand command)
        {
            this.path.AddBezier(command.StartPoint, command.StartControlPoint, command.EndControlPoint, command.EndPoint);
        }

        void IShapeCommandVisitor.VisitShapeDrawPathCommand(ShapeDrawPathCommand command)
        {
            InvalidOperation();
        }

        void IShapeCommandVisitor.VisitShapeFillPathCommand(ShapeFillPathCommand command)
        {
            InvalidOperation();
        }

        void IShapeCommandVisitor.VisitShapeLineCommand(ShapeLineCommand command)
        {
            this.path.AddLine(command.StartPoint, command.EndPoint);
        }

        private static void InvalidOperation()
        {
            ExceptionHelper.ThrowInvalidOperationException();
        }

        void IDisposable.Dispose()
        {
            if (this.path != null)
            {
                this.path.Dispose();
                this.path = null;
            }
        }

        public GraphicsPath Path =>
            this.path;
    }
}

