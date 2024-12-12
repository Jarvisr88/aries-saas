namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public abstract class GroupFillEntriesCalculatorBase<TGroup>
    {
        private readonly Stack<Matrix> groupTransforms;
        private readonly Stack<Rectangle> parentBounds;
        private readonly Stack<TGroup> parentGroupShapes;

        protected GroupFillEntriesCalculatorBase()
        {
            this.groupTransforms = new Stack<Matrix>();
            this.parentBounds = new Stack<Rectangle>();
            this.parentGroupShapes = new Stack<TGroup>();
        }

        private static GraphicsPath BuildFigure(List<GraphicsPath> graphicsPaths)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Alternate);
            if (graphicsPaths != null)
            {
                if (graphicsPaths.Count > 0)
                {
                    GraphicsPath addingPath = graphicsPaths[0];
                    path.AddPath(addingPath, false);
                    addingPath.Dispose();
                }
                for (int i = 1; i < graphicsPaths.Count; i++)
                {
                    GraphicsPath graphicsPath = graphicsPaths[i];
                    if (graphicsPath.IsClosed())
                    {
                        path.AddPath(graphicsPath, false);
                    }
                    graphicsPath.Dispose();
                }
            }
            return path;
        }

        private void CalculateBounds(TGroup groupShape, Matrix transform)
        {
            this.groupTransforms.Push(transform);
            this.parentGroupShapes.Push(groupShape);
            this.EnumerateDrawings(groupShape);
            this.parentGroupShapes.Pop();
            this.groupTransforms.Pop();
        }

        private float CalculateChildScaleX(GroupShapeProperties groupShapeProperties, int currentParentWidth)
        {
            float f = ((float) currentParentWidth) / groupShapeProperties.ChildTransform2D.Cx;
            if (float.IsNaN(f))
            {
                f = 0f;
            }
            return f;
        }

        private float CalculateChildScaleY(GroupShapeProperties groupShapeProperties, int currentParentHeight)
        {
            float f = ((float) currentParentHeight) / groupShapeProperties.ChildTransform2D.Cy;
            if (float.IsNaN(f))
            {
                f = 0f;
            }
            return f;
        }

        public static Matrix CreateGroupShapeTransform(GroupShapeProperties shapeProperties, Rectangle bounds, Matrix parentTransform)
        {
            Transform2D transformd = shapeProperties.Transform2D;
            Matrix matrix = parentTransform?.Clone();
            Matrix matrix2 = TransformMatrixExtensions.CreateFlipTransformUnsafe(bounds, transformd.FlipH, transformd.FlipV, transformd.GetRotationAngleInDegrees());
            if (matrix == null)
            {
                matrix = matrix2;
            }
            else if (matrix2 != null)
            {
                matrix.Multiply(matrix2);
                matrix2.Dispose();
            }
            return matrix;
        }

        protected abstract void EnumerateDrawings(TGroup groupShape);
        private static float GetChildHeight(Transform2D innerDrawingTransform2D) => 
            innerDrawingTransform2D.Cy;

        private static float GetChildWidth(Transform2D innerDrawingTransform2D) => 
            innerDrawingTransform2D.Cx;

        private static float GetChildX(Transform2D innerDrawingTransform2D, Transform2D groupChildTransform2D) => 
            innerDrawingTransform2D.OffsetX - groupChildTransform2D.OffsetX;

        private static float GetChildY(Transform2D innerDrawingTransform2D, Transform2D groupChildTransform2D) => 
            innerDrawingTransform2D.OffsetY - groupChildTransform2D.OffsetY;

        protected Rectangle GetDrawingObjectBoundsInLayoutUnits(Transform2D innerDrawingTransform2D, Rectangle currentParentBounds)
        {
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = innerDrawingTransform2D.DocumentModel.ToDocumentLayoutUnitConverter;
            GroupShapeProperties groupShapeProperties = this.GetGroupShapeProperties(this.CurrentGroup);
            float num3 = this.CalculateChildScaleX(groupShapeProperties, toDocumentLayoutUnitConverter.ToModelUnits(currentParentBounds.Width));
            float num4 = this.CalculateChildScaleY(groupShapeProperties, toDocumentLayoutUnitConverter.ToModelUnits(currentParentBounds.Height));
            Transform2D transformd = groupShapeProperties.ChildTransform2D;
            RectangleF rectangle = new RectangleF(GroupFillEntriesCalculatorBase<TGroup>.GetChildX(innerDrawingTransform2D, transformd), GroupFillEntriesCalculatorBase<TGroup>.GetChildY(innerDrawingTransform2D, transformd), GroupFillEntriesCalculatorBase<TGroup>.GetChildWidth(innerDrawingTransform2D), GroupFillEntriesCalculatorBase<TGroup>.GetChildHeight(innerDrawingTransform2D));
            bool flag = GroupFillEntriesCalculatorBase<TGroup>.NeedToSwap(innerDrawingTransform2D.GetRotationAngleInDegrees());
            if (flag)
            {
                rectangle = RectangleUtils.SwapWidthAndHeight(rectangle);
            }
            rectangle = new RectangleF(rectangle.X * num3, rectangle.Y * num4, rectangle.Width * num3, rectangle.Height * num4);
            if (flag)
            {
                rectangle = RectangleUtils.SwapWidthAndHeight(rectangle);
            }
            return new Rectangle(currentParentBounds.Left + ((int) Math.Round((double) toDocumentLayoutUnitConverter.ToLayoutUnits(rectangle.X))), currentParentBounds.Top + ((int) Math.Round((double) toDocumentLayoutUnitConverter.ToLayoutUnits(rectangle.Y))), (int) Math.Round((double) toDocumentLayoutUnitConverter.ToLayoutUnits(rectangle.Width)), (int) Math.Round((double) toDocumentLayoutUnitConverter.ToLayoutUnits(rectangle.Height)));
        }

        public List<GraphicsPath> GetEntries(TGroup groupShape, Rectangle bounds)
        {
            this.Result = new List<GraphicsPath>();
            this.parentBounds.Push(bounds);
            this.CalculateBounds(groupShape, null);
            this.parentBounds.Pop();
            return this.Result;
        }

        protected abstract GroupShapeProperties GetGroupShapeProperties(TGroup groupShape);
        private static bool NeedToSwap(float angleInDegrees)
        {
            float num = angleInDegrees % 180f;
            if (num < 0f)
            {
                num += 180f;
            }
            return ((num >= 45f) && (num < 135f));
        }

        protected void VisitCore(ShapeProperties shapeProperties, ModelShapeCustomGeometry geometry, Rectangle bounds)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            float defaultScale = documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(documentModel.UnitConverter.EmuToModelUnitsF(1));
            using (Matrix matrix = ShapeRenderHelper.CreateShapeTransform(shapeProperties, bounds, this.CurrentGroupTransform))
            {
                GraphicsPath item = GroupFillEntriesCalculatorBase<TGroup>.BuildFigure(ShapeRenderHelper.CalculateGraphicsPaths(geometry, bounds, defaultScale));
                if (matrix != null)
                {
                    item.Transform(matrix);
                }
                this.Result.Add(item);
            }
        }

        protected void VisitGroup(TGroup groupShape)
        {
            Rectangle drawingObjectBoundsInLayoutUnits = this.GetDrawingObjectBoundsInLayoutUnits(this.GetGroupShapeProperties(groupShape).Transform2D, this.CurrentBounds);
            this.parentBounds.Push(drawingObjectBoundsInLayoutUnits);
            Matrix transform = GroupFillEntriesCalculatorBase<TGroup>.CreateGroupShapeTransform(this.GetGroupShapeProperties(groupShape), drawingObjectBoundsInLayoutUnits, this.CurrentGroupTransform);
            this.CalculateBounds(groupShape, transform);
            if (transform != null)
            {
                transform.Dispose();
            }
            this.parentBounds.Pop();
        }

        protected List<GraphicsPath> Result { get; set; }

        protected Matrix CurrentGroupTransform =>
            this.groupTransforms.Peek();

        protected Rectangle CurrentBounds =>
            this.parentBounds.Peek();

        protected TGroup CurrentGroup =>
            this.parentGroupShapes.Peek();
    }
}

