namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Extensions;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media;

    public static class WpfDrawingShapeHelper
    {
        internal static System.Windows.Point CalcBezier(Vector[] p, double t)
        {
            double num = t * t;
            double num2 = 1.0 - t;
            double num3 = num2 * num2;
            Vector vector = (Vector) (((((num2 * num3) * p[0]) + (((3.0 * t) * num3) * p[1])) + (((3.0 * num) * num2) * p[2])) + ((t * num) * p[3]));
            return new System.Windows.Point(vector.X, vector.Y);
        }

        internal static double CalcBezierLength(Vector[] p)
        {
            Vector vector;
            System.Windows.Point point;
            double num = 0.0;
            System.Windows.Point point2 = new System.Windows.Point(p[0].X, p[0].Y);
            for (int i = 0; i < 9; i++)
            {
                point = CalcBezier(p, (i + 1) * 0.1);
                vector = (Vector) (point - point2);
                num += vector.Length;
                point2 = point;
            }
            point = new System.Windows.Point(p[3].X, p[3].Y);
            vector = (Vector) (point - point2);
            return (num + vector.Length);
        }

        public static System.Windows.Media.Brush CalculateFillBrush(ShapePropertiesBase shapeProperties, ShapePreset shapeType, ShapeStyle shapeStyle, Rect bounds, float shapeRotationAngle) => 
            CalculateFillBrush(shapeProperties, shapeType, shapeStyle, bounds, shapeRotationAngle, null);

        public static System.Windows.Media.Brush CalculateFillBrush(ShapePropertiesBase shapeProperties, ShapePreset shapeType, ShapeStyle shapeStyle, Rect bounds, float shapeRotationAngle, PathGeometry geometry)
        {
            IDrawingFill fill = shapeProperties.Fill;
            if (fill == null)
            {
                return null;
            }
            DrawingFillType fillType = fill.FillType;
            if (fillType == DrawingFillType.Group)
            {
                return null;
            }
            System.Windows.Media.Color? defaultColor = null;
            if (!shapeStyle.FillColor.IsEmpty && (shapeStyle.FillReferenceIdx != 0))
            {
                defaultColor = new System.Windows.Media.Color?(shapeStyle.FillColor.FinalColor.ToWpfColor());
            }
            System.Windows.Media.Color styleColor = System.Drawing.Color.Empty.ToWpfColor();
            if ((fillType == DrawingFillType.Automatic) && (shapeStyle.FillReferenceIdx > 0))
            {
                fill = shapeProperties.DocumentModel.OfficeTheme.FormatScheme.GetFill((StyleMatrixElementType) Math.Min(3, shapeStyle.FillReferenceIdx));
                styleColor = shapeStyle.FillColor.FinalColor.ToWpfColor();
            }
            WPFShapeFillRenderVisitor visitor = new WPFShapeFillRenderVisitor(styleColor, defaultColor, bounds, shapeType, shapeRotationAngle, geometry);
            fill.Visit(visitor);
            return visitor.Result;
        }

        private static PathGeometry CalculateGraphicsPaths(ModelShapeCustomGeometry geometry, Rect bounds, float defaultScale)
        {
            WpfCustomGeometryToPathGeometryConverter converter = new WpfCustomGeometryToPathGeometryConverter(bounds, defaultScale);
            foreach (ModelShapePath path in geometry.Paths)
            {
                converter.Walk(path);
            }
            return converter.PathGeometry;
        }

        public static System.Windows.Media.Brush CalculateLineBrush(Outline outline, ShapeStyle shapeStyle, Rect bounds, ShapePreset shapeType)
        {
            System.Windows.Media.Color color;
            IDrawingFill fill = outline.Fill;
            System.Windows.Media.Color? defaultColor = null;
            if (fill.FillType != DrawingFillType.Automatic)
            {
                color = System.Drawing.Color.Empty.ToWpfColor();
            }
            else
            {
                color = shapeStyle.LineColor.FinalColor.ToWpfColor();
                defaultColor = new System.Windows.Media.Color?(color);
            }
            if (outline.IsDefault || (outline.Width == 0))
            {
                if ((shapeStyle.LineReferenceIdx == 0) && outline.IsDefault)
                {
                    return null;
                }
                if (fill.FillType == DrawingFillType.Automatic)
                {
                    fill = GetStyleOutline(shapeStyle).Fill;
                }
            }
            WPFShapeFillRenderVisitor visitor = new WPFShapeFillRenderVisitor(color, defaultColor, bounds, shapeType, 0f) {
                OutlineFill = true
            };
            fill.Visit(visitor);
            return visitor.Result;
        }

        public static System.Windows.Media.Pen CalculateLinePen(Outline outline, ShapeStyle shapeStyle, Rect bounds, ShapePreset shapeType) => 
            CalculateLinePen(outline, shapeStyle, bounds, shapeType, false, false);

        public static System.Windows.Media.Pen CalculateLinePen(Outline outline, ShapeStyle shapeStyle, Rect bounds, ShapePreset shapeType, bool hasHeadArrow, bool hasTailArrow)
        {
            System.Windows.Media.Brush brush = CalculateLineBrush(outline, shapeStyle, bounds, shapeType);
            if (brush == null)
            {
                return null;
            }
            System.Windows.Media.Pen pen = new System.Windows.Media.Pen(brush, CalculatePenWidth(outline, shapeStyle)) {
                DashStyle = GetPenDashStyle(outline.Dashing),
                StartLineCap = GetPenLineCap(outline.EndCapStyle, hasHeadArrow),
                EndLineCap = GetPenLineCap(outline.EndCapStyle, hasTailArrow)
            };
            pen.DashCap = (pen.EndLineCap == PenLineCap.Round) ? PenLineCap.Round : PenLineCap.Square;
            SetupLineJoinStyle(pen, outline.JoinStyle, outline.MiterLimit, shapeType);
            if (pen.CanFreeze)
            {
                pen.Freeze();
            }
            return pen;
        }

        public static double CalculatePenWidth(Outline outline, ShapeStyle shapeStyle)
        {
            float width = outline.Width;
            if (outline.IsDefault || (outline.Width == 0))
            {
                if ((shapeStyle.LineReferenceIdx == 0) && outline.IsDefault)
                {
                    return 0.0;
                }
                width = GetStyleOutline(shapeStyle).Width;
            }
            return Math.Round((double) Math.Max(1f, shapeStyle.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(width)));
        }

        public static PathGeometry CalculateShapeLayoutInfo(ModelShapeCustomGeometry geometry, Rect bounds, float scale) => 
            CalculateGraphicsPaths(geometry, bounds, scale);

        public static PathGeometry CalculateShapeLayoutInfo(ShapeProperties shapeProperties, ModelShapeCustomGeometry geometry, Rect boundsInLayoutUnits)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            float scale = documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(documentModel.UnitConverter.EmuToModelUnitsF(1));
            return CalculateShapeLayoutInfo(geometry, boundsInLayoutUnits, scale);
        }

        public static List<PathGeometry> CalculateShapeLayoutInfoWithOpenGeometries(ShapeProperties shapeProperties, ModelShapeCustomGeometry geometry, Rect boundsInLayoutUnits)
        {
            IDocumentModel documentModel = shapeProperties.DocumentModel;
            float defaultScale = documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(documentModel.UnitConverter.EmuToModelUnitsF(1));
            WpfCustomGeometryToOpenPathGeometriesConverter converter = new WpfCustomGeometryToOpenPathGeometriesConverter(boundsInLayoutUnits, defaultScale);
            foreach (ModelShapePath path in geometry.Paths)
            {
                if (path.Stroke)
                {
                    converter.Walk(path);
                }
            }
            return converter.PathGeometries;
        }

        internal static Vector[] GetBezierPoints(System.Windows.Point p0, System.Windows.Point p1, System.Windows.Point p2, System.Windows.Point p3) => 
            new Vector[] { new Vector(p0.X, p0.Y), new Vector(p1.X, p1.Y), new Vector(p2.X, p2.Y), new Vector(p3.X, p3.Y) };

        private static DashStyle GetPenDashStyle(OutlineDashing dashing)
        {
            switch (dashing)
            {
                case OutlineDashing.Solid:
                    return DashStyles.Solid;

                case OutlineDashing.SystemDash:
                    return DashStyles.Dash;

                case OutlineDashing.SystemDot:
                    return DashStyles.Dot;

                case OutlineDashing.SystemDashDot:
                    return DashStyles.DashDot;

                case OutlineDashing.SystemDashDotDot:
                    return DashStyles.DashDotDot;

                case OutlineDashing.Dot:
                {
                    double[] dashes = new double[] { 1.0, 3.0 };
                    return new DashStyle(dashes, 0.0);
                }
                case OutlineDashing.Dash:
                {
                    double[] dashes = new double[] { 3.0, 4.0 };
                    return new DashStyle(dashes, 0.0);
                }
                case OutlineDashing.LongDash:
                {
                    double[] dashes = new double[] { 7.0, 4.0 };
                    return new DashStyle(dashes, 0.0);
                }
                case OutlineDashing.DashDot:
                    return new DashStyle(new double[] { 3.0, 4.0, 0.20000000298023224, 4.0 }, 0.0);

                case OutlineDashing.LongDashDot:
                    return new DashStyle(new double[] { 7.0, 4.0, 0.20000000298023224, 4.0 }, 0.0);

                case OutlineDashing.LongDashDotDot:
                    return new DashStyle(new double[] { 7.0, 4.0, 0.20000000298023224, 4.0, 0.20000000298023224, 4.0 }, 0.0);
            }
            Exceptions.ThrowInternalException();
            return DashStyles.Solid;
        }

        private static PenLineCap GetPenLineCap(OutlineEndCapStyle endCapStyle, bool hasArrow)
        {
            if (hasArrow)
            {
                return PenLineCap.Round;
            }
            switch (endCapStyle)
            {
                case OutlineEndCapStyle.Round:
                    return PenLineCap.Round;

                case OutlineEndCapStyle.Square:
                    return PenLineCap.Square;

                case OutlineEndCapStyle.Flat:
                    return PenLineCap.Flat;
            }
            Exceptions.ThrowInternalException();
            return PenLineCap.Triangle;
        }

        private static Outline GetStyleOutline(ShapeStyle shapeStyle)
        {
            int num = Math.Min(3, Math.Max(1, shapeStyle.LineReferenceIdx));
            return shapeStyle.DocumentModel.OfficeTheme.FormatScheme.GetOutline((StyleMatrixElementType) num);
        }

        public static bool HasArrows(ShapeProperties shapeProperties) => 
            (shapeProperties.IsConnectionShape() || NonConnectorWithArrows(shapeProperties.ShapeType)) ? ((shapeProperties.Outline.HeadType != OutlineHeadTailType.None) || (shapeProperties.Outline.TailType != OutlineHeadTailType.None)) : false;

        private static bool NonConnectorWithArrows(ShapePreset shapeType)
        {
            if (shapeType != ShapePreset.None)
            {
                switch (shapeType)
                {
                    case ShapePreset.Arc:
                    case ShapePreset.LeftBracket:
                    case ShapePreset.RightBracket:
                    case ShapePreset.LeftBrace:
                    case ShapePreset.RightBrace:
                    case ShapePreset.BracketPair:
                    case ShapePreset.BracePair:
                        break;

                    default:
                        return false;
                }
            }
            return true;
        }

        private static void SetupLineJoinStyle(System.Windows.Media.Pen pen, LineJoinStyle joinStyle, int miterLimit, ShapePreset shapeType)
        {
            if ((joinStyle == LineJoinStyle.Round) && ShapeRenderHelper.IsMiteredLineJoinShape(shapeType))
            {
                joinStyle = LineJoinStyle.Miter;
            }
            switch (joinStyle)
            {
                case LineJoinStyle.Bevel:
                    pen.LineJoin = PenLineJoin.Bevel;
                    return;

                case LineJoinStyle.Miter:
                    pen.LineJoin = PenLineJoin.Miter;
                    pen.MiterLimit = DrawingValueConverter.FromPercentage(miterLimit);
                    return;

                case LineJoinStyle.Round:
                    pen.LineJoin = PenLineJoin.Round;
                    return;
            }
        }
    }
}

