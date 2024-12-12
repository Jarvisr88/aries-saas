namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ShapeCommandPainter : IShapeCommandVisitor
    {
        private IGraphics graphics;
        private Pen pen;
        private Brush brush;

        public ShapeCommandPainter(IGraphics graphics, Pen pen, Brush brush)
        {
            this.graphics = graphics;
            this.pen = pen;
            this.brush = brush;
        }

        void IShapeCommandVisitor.VisitShapeBezierCommand(ShapeBezierCommand command)
        {
        }

        void IShapeCommandVisitor.VisitShapeDrawPathCommand(ShapeDrawPathCommand command)
        {
            if (this.pen.Width > 0f)
            {
                this.DoActionOnPath(command, DrawPathAction.Instance);
            }
        }

        void IShapeCommandVisitor.VisitShapeFillPathCommand(ShapeFillPathCommand command)
        {
            this.DoActionOnPath(command, FillPathAction.Instance);
        }

        void IShapeCommandVisitor.VisitShapeLineCommand(ShapeLineCommand command)
        {
            if (this.pen.Width > 0f)
            {
                this.graphics.DrawLine(this.pen, command.StartPoint, command.EndPoint);
            }
        }

        private void DoActionOnPath(ShapePathCommand command, IPathAction action)
        {
            using (GraphicsPathFiller filler = new GraphicsPathFiller())
            {
                command.Commands.Iterate(filler);
                if (command.IsClosed)
                {
                    filler.Path.CloseFigure();
                }
                action.Action(this.graphics, filler.Path, this.brush, this.pen);
            }
        }

        private class DrawPathAction : ShapeCommandPainter.IPathAction
        {
            public static readonly ShapeCommandPainter.DrawPathAction Instance = new ShapeCommandPainter.DrawPathAction();

            private DrawPathAction()
            {
            }

            void ShapeCommandPainter.IPathAction.Action(IGraphics gr, GraphicsPath path, Brush brush, Pen pen)
            {
                gr.DrawPath(pen, path);
            }
        }

        private class FillPathAction : ShapeCommandPainter.IPathAction
        {
            public static readonly ShapeCommandPainter.FillPathAction Instance = new ShapeCommandPainter.FillPathAction();

            private FillPathAction()
            {
            }

            void ShapeCommandPainter.IPathAction.Action(IGraphics gr, GraphicsPath path, Brush brush, Pen pen)
            {
                gr.FillPath(brush, path);
            }
        }

        private interface IPathAction
        {
            void Action(IGraphics gr, GraphicsPath path, Brush brush, Pen pen);
        }
    }
}

