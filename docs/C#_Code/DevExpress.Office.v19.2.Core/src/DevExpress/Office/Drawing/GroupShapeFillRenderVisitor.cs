namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class GroupShapeFillRenderVisitor : ShapeFillRenderVisitor
    {
        public GroupShapeFillRenderVisitor(ShapePreset shapeType, Color styleColor, Brush defaultBrush, List<GraphicsPath> graphicsPaths, Matrix shapeTransform) : base(shapeType, styleColor, defaultBrush, graphicsPaths, shapeTransform)
        {
        }

        private void ApplyGroupTransform(GraphicsPath result, ref Point centerPoint)
        {
            if (!base.HasPermanentFill && (base.ShapeTransform != null))
            {
                result.Transform(base.ShapeTransform);
                centerPoint = base.ShapeTransform.TransformPoint(centerPoint);
            }
        }

        protected override void ApplyTransformForTextureBrush(TextureBrush brush)
        {
            if (!base.HasPermanentFill && (base.ShapeTransform != null))
            {
                brush.MultiplyTransform(base.ShapeTransform, MatrixOrder.Append);
                if (brush.WrapMode != WrapMode.Clamp)
                {
                    brush.ScaleTransform(1f / base.ScaleFactor, 1f / base.ScaleFactor);
                }
            }
        }

        protected override GraphicsPath CreateRadialGradientPath(Rectangle bounds, ref Point centerPoint)
        {
            GraphicsPath result = base.CreateRadialGradientPath(bounds, ref centerPoint);
            this.ApplyGroupTransform(result, ref centerPoint);
            return result;
        }

        protected override GraphicsPath CreateRectangularGradientPath(Rectangle bounds, ref Point centerPoint)
        {
            GraphicsPath result = base.CreateRectangularGradientPath(bounds, ref centerPoint);
            this.ApplyGroupTransform(result, ref centerPoint);
            return result;
        }

        protected override RectangleOffset GetTileRect(DrawingGradientFill fill) => 
            RectangleOffset.Empty;

        protected override void TransformLinearGradientBrush(LinearGradientBrush brush)
        {
            if (!base.HasPermanentFill && (base.ShapeTransform != null))
            {
                brush.MultiplyTransform(base.ShapeTransform, MatrixOrder.Append);
            }
        }
    }
}

