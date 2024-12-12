namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class WpfPatternFillProvider
    {
        private static GeometryDrawing CreateBackDrawing(Color backColor, int length)
        {
            GeometryGroup geometry = new GeometryGroup {
                Children = { new RectangleGeometry(new Rect(0.0, 0.0, (double) length, (double) length)) }
            };
            SolidColorBrush brush = new SolidColorBrush(backColor);
            brush.Freeze();
            return new GeometryDrawing(brush, null, geometry);
        }

        private static LineGeometry CreateLine(int x1, int y1, int x2, int y2)
        {
            if (x1 != x2)
            {
                x2++;
            }
            if (y1 != y2)
            {
                y2++;
            }
            return new LineGeometry(new Point((double) x1, (double) y1), new Point((double) x2, (double) y2));
        }

        private static LineGeometry CreateLineUpward(int x1, int y1, int x2, int y2) => 
            new LineGeometry(new Point((double) x1, (double) y1), new Point((double) x2, (double) y2));

        private static GeometryDrawing CreatePatternDrawing(Color foreColor, GeometryGroup group, bool isNeedDrawLine, bool dottedPen)
        {
            SolidColorBrush brush = new SolidColorBrush(foreColor);
            brush.Freeze();
            if (!isNeedDrawLine)
            {
                return new GeometryDrawing(brush, null, group);
            }
            Pen pen = new Pen(brush, 1.0);
            if (dottedPen)
            {
                pen.DashStyle = DashStyles.Dot;
            }
            pen.Freeze();
            return new GeometryDrawing(null, pen, group);
        }

        private static LineGeometry CreatePoint(int x, int y) => 
            new LineGeometry(new Point((double) x, (double) y), new Point((double) (x + 1), (double) (y + 1)));

        private static RectangleGeometry CreateRectangle(int x, int y, int width, int height) => 
            new RectangleGeometry(new Rect((double) x, (double) y, (double) width, (double) height));

        public static Brush GetPatternFillBrush(DrawingPatternType type, Color foreColor, Color backColor)
        {
            DrawingBrush brush = new DrawingBrush();
            GeometryGroup group = new GeometryGroup();
            TileInfo info = PopulateTile(type, group);
            if (info.InvertColors)
            {
                Color color = foreColor;
                foreColor = backColor;
                backColor = color;
            }
            GeometryDrawing drawing = CreateBackDrawing(backColor, info.Length);
            GeometryDrawing drawing2 = CreatePatternDrawing(foreColor, group, info.ContainsLine, info.DottedPen);
            DrawingGroup group2 = new DrawingGroup();
            group2.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            group2.Children.Add(drawing);
            group2.Children.Add(drawing2);
            brush.Drawing = group2;
            brush.Stretch = Stretch.None;
            brush.TileMode = TileMode.Tile;
            Rect rect = new Rect(0.0, 0.0, (double) info.Length, (double) info.Length);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.Viewport = rect;
            if (info.AbsoluteViewBoxMapping)
            {
                brush.ViewboxUnits = BrushMappingMode.Absolute;
                brush.Viewbox = rect;
            }
            brush.Freeze();
            return brush;
        }

        private static TileInfo PopulateTile(DrawingPatternType type, GeometryGroup group)
        {
            switch (type)
            {
                case DrawingPatternType.DashedDownwardDiagonal:
                    return PopulateTile_DashedDownwardDiagonal(group);

                case DrawingPatternType.DashedHorizontal:
                    return PopulateTile_DashedHorizontal(group);

                case DrawingPatternType.DashedUpwardDiagonal:
                    return PopulateTile_DashedUpwardDiagonal(group);

                case DrawingPatternType.DashedVertical:
                    return PopulateTile_DashedVertical(group);

                case DrawingPatternType.DiagonalBrick:
                    return PopulateTile_DiagonalBrick(group);

                case DrawingPatternType.Divot:
                    return PopulateTile_Divot(group);

                case DrawingPatternType.DarkDownwardDiagonal:
                    return PopulateTile_DarkDownwardDiagonal(group);

                case DrawingPatternType.DarkHorizontal:
                    return PopulateTile_DarkHorizontal(group);

                case DrawingPatternType.DarkUpwardDiagonal:
                    return PopulateTile_DarkUpwardDiagonal(group);

                case DrawingPatternType.DarkVertical:
                    return PopulateTile_DarkVertical(group);

                case DrawingPatternType.DottedDiamond:
                    return PopulateTile_DottedDiamond(group);

                case DrawingPatternType.DottedGrid:
                    return PopulateTile_DottedGrid(group);

                case DrawingPatternType.HorizontalBrick:
                    return PopulateTile_HorizontalBrick(group);

                case DrawingPatternType.LargeCheckerBoard:
                    return PopulateTile_LargeCheckerBoard(group);

                case DrawingPatternType.LargeConfetti:
                    return PopulateTile_LargeConfetti(group);

                case DrawingPatternType.LargeGrid:
                    return PopulateTile_LargeGrid(group);

                case DrawingPatternType.LightDownwardDiagonal:
                    return PopulateTile_LightDownwardDiagonal(group);

                case DrawingPatternType.LightHorizontal:
                    return PopulateTile_LightHorizontal(group);

                case DrawingPatternType.LightUpwardDiagonal:
                    return PopulateTile_LightUpwardDiagonal(group);

                case DrawingPatternType.LightVertical:
                    return PopulateTile_LightVertical(group);

                case DrawingPatternType.NarrowHorizontal:
                    return PopulateTile_NarrowHorizontal(group);

                case DrawingPatternType.NarrowVertical:
                    return PopulateTile_NarrowVertical(group);

                case DrawingPatternType.OpenDiamond:
                    return PopulateTile_OpenDiamond(group);

                case DrawingPatternType.Percent10:
                    return PopulateTile_Percent10(group);

                case DrawingPatternType.Percent20:
                    return PopulateTile_Percent20(group);

                case DrawingPatternType.Percent25:
                    return PopulateTile_Percent25(group);

                case DrawingPatternType.Percent30:
                    return PopulateTile_Percent30(group);

                case DrawingPatternType.Percent40:
                    return PopulateTile_Percent40(group);

                case DrawingPatternType.Percent5:
                    return PopulateTile_Percent5(group);

                case DrawingPatternType.Percent50:
                    return PopulateTile_Percent50(group);

                case DrawingPatternType.Percent60:
                    return PopulateTile_Percent60(group);

                case DrawingPatternType.Percent70:
                    return PopulateTile_Percent70(group);

                case DrawingPatternType.Percent75:
                    return PopulateTile_Percent75(group);

                case DrawingPatternType.Percent80:
                    return PopulateTile_Percent80(group);

                case DrawingPatternType.Percent90:
                    return PopulateTile_Percent90(group);

                case DrawingPatternType.Plaid:
                    return PopulateTile_Plaid(group);

                case DrawingPatternType.Shingle:
                    return PopulateTile_Shingle(group);

                case DrawingPatternType.SmallCheckerBoard:
                    return PopulateTile_SmallCheckerBoard(group);

                case DrawingPatternType.SmallConfetti:
                    return PopulateTile_SmallConfetti(group);

                case DrawingPatternType.SmallGrid:
                    return PopulateTile_SmallGrid(group);

                case DrawingPatternType.SolidDiamond:
                    return PopulateTile_SolidDiamond(group);

                case DrawingPatternType.Sphere:
                    return PopulateTile_Sphere(group);

                case DrawingPatternType.Trellis:
                    return PopulateTile_Trellis(group);

                case DrawingPatternType.Wave:
                    return PopulateTile_Wave(group);

                case DrawingPatternType.WideDownwardDiagonal:
                    return PopulateTile_WideDownwardDiagonal(group);

                case DrawingPatternType.WideUpwardDiagonal:
                    return PopulateTile_WideUpwardDiagonal(group);

                case DrawingPatternType.Weave:
                    return PopulateTile_Weave(group);

                case DrawingPatternType.ZigZag:
                    return PopulateTile_ZigZag(group);
            }
            Exceptions.ThrowInternalException();
            return new TileInfo();
        }

        private static TileInfo PopulateTile_DarkDownwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 3));
            group.Children.Add(CreatePoint(1, 0));
            group.Children.Add(CreatePoint(2, 1));
            group.Children.Add(CreatePoint(3, 2));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_DarkHorizontal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            group.Children.Add(CreateLine(0, 1, 3, 1));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_DarkUpwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(0, 1));
            group.Children.Add(CreatePoint(1, 0));
            group.Children.Add(CreatePoint(1, 3));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 1));
            group.Children.Add(CreatePoint(2, 3));
            group.Children.Add(CreatePoint(3, 2));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_DarkVertical(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 0, 3));
            group.Children.Add(CreateLine(1, 0, 1, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_DashedDownwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 3));
            group.Children.Add(CreateLine(4, 0, 7, 3));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DashedHorizontal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            group.Children.Add(CreateLine(4, 4, 7, 4));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DashedUpwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreatePoint(1, 2));
            group.Children.Add(CreatePoint(2, 1));
            group.Children.Add(CreatePoint(3, 0));
            group.Children.Add(CreatePoint(4, 3));
            group.Children.Add(CreatePoint(5, 2));
            group.Children.Add(CreatePoint(6, 1));
            group.Children.Add(CreatePoint(7, 0));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DashedVertical(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 0, 3));
            group.Children.Add(CreateLine(4, 4, 4, 7));
            group.Children.Add(CreateLine(0, 1, 0, 3));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DiagonalBrick(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 3));
            group.Children.Add(CreatePoint(0, 7));
            group.Children.Add(CreatePoint(1, 6));
            group.Children.Add(CreatePoint(2, 5));
            group.Children.Add(CreatePoint(3, 4));
            group.Children.Add(CreatePoint(4, 3));
            group.Children.Add(CreatePoint(5, 2));
            group.Children.Add(CreatePoint(6, 1));
            group.Children.Add(CreatePoint(7, 0));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Divot(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(4, 0));
            group.Children.Add(CreatePoint(5, 1));
            group.Children.Add(CreatePoint(4, 2));
            group.Children.Add(CreatePoint(1, 4));
            group.Children.Add(CreatePoint(0, 5));
            group.Children.Add(CreatePoint(1, 6));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DottedDiamond(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(6, 2));
            group.Children.Add(CreatePoint(4, 4));
            group.Children.Add(CreatePoint(2, 6));
            group.Children.Add(CreatePoint(6, 6));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_DottedGrid(GeometryGroup group)
        {
            TileInfo info = PopulateTile_LargeGrid(group);
            info.DottedPen = true;
            return info;
        }

        private static TileInfo PopulateTile_HorizontalBrick(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 7, 0));
            group.Children.Add(CreateLine(0, 4, 7, 4));
            group.Children.Add(CreateLine(0, 1, 0, 3));
            group.Children.Add(CreateLine(4, 5, 4, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_LargeCheckerBoard(GeometryGroup group)
        {
            group.Children.Add(CreateRectangle(0, 0, 4, 4));
            group.Children.Add(CreateRectangle(4, 4, 4, 4));
            return new TileInfo(8, false);
        }

        private static TileInfo PopulateTile_LargeConfetti(GeometryGroup group)
        {
            group.Children.Add(CreateRectangle(0, 2, 2, 2));
            group.Children.Add(CreateRectangle(1, 5, 2, 2));
            group.Children.Add(CreateRectangle(2, 0, 2, 2));
            group.Children.Add(CreateRectangle(4, 4, 2, 2));
            group.Children.Add(CreateRectangle(5, 1, 2, 2));
            group.Children.Add(CreateRectangle(6, 6, 2, 2));
            return new TileInfo(8, false);
        }

        private static TileInfo PopulateTile_LargeGrid(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 7, 0));
            group.Children.Add(CreateLine(0, 0, 0, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_LightDownwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_LightHorizontal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_LightUpwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreatePoint(1, 2));
            group.Children.Add(CreatePoint(2, 1));
            group.Children.Add(CreatePoint(3, 0));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_LightVertical(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 0, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_NarrowHorizontal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 1, 0));
            return new TileInfo(2, true);
        }

        private static TileInfo PopulateTile_NarrowVertical(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 0, 1));
            return new TileInfo(2, true);
        }

        private static TileInfo PopulateTile_OpenDiamond(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 7, 7));
            group.Children.Add(CreateLineUpward(0, 7, 7, 0));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Percent10(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 2));
            group.Children.Add(CreatePoint(4, 0));
            group.Children.Add(CreatePoint(0, 6));
            group.Children.Add(CreatePoint(4, 4));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Percent20(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(2, 2));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_Percent25(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(0, 2));
            group.Children.Add(CreatePoint(2, 1));
            group.Children.Add(CreatePoint(2, 3));
            return new TileInfo(4, true) { AbsoluteViewBoxMapping = true };
        }

        private static TileInfo PopulateTile_Percent30(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 3));
            group.Children.Add(CreatePoint(2, 0));
            group.Children.Add(CreatePoint(0, 2));
            return new TileInfo(4, true) { AbsoluteViewBoxMapping = true };
        }

        private static TileInfo PopulateTile_Percent40(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreateLine(0, 6, 1, 7));
            group.Children.Add(CreateLine(0, 4, 3, 7));
            group.Children.Add(CreateLine(0, 2, 5, 7));
            group.Children.Add(CreateLine(2, 2, 4, 4));
            group.Children.Add(CreateLine(6, 6, 7, 7));
            group.Children.Add(CreateLine(2, 0, 7, 5));
            group.Children.Add(CreateLine(4, 0, 7, 3));
            group.Children.Add(CreateLine(6, 0, 7, 1));
            group.Children.Add(CreatePoint(5, 7));
            group.Children.Add(CreatePoint(7, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Percent5(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(4, 4));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Percent50(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(1, 1));
            return new TileInfo(2, true);
        }

        private static TileInfo PopulateTile_Percent60(GeometryGroup group)
        {
            TileInfo info = PopulateTile_Percent30(group);
            info.InvertColors = true;
            return info;
        }

        private static TileInfo PopulateTile_Percent70(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(1, 0));
            group.Children.Add(CreatePoint(2, 0));
            group.Children.Add(CreatePoint(3, 0));
            group.Children.Add(CreatePoint(0, 1));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(3, 1));
            group.Children.Add(CreatePoint(1, 2));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 2));
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreatePoint(1, 3));
            group.Children.Add(CreatePoint(3, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_Percent75(GeometryGroup group)
        {
            TileInfo info = PopulateTile_Percent20(group);
            info.InvertColors = true;
            return info;
        }

        private static TileInfo PopulateTile_Percent80(GeometryGroup group)
        {
            TileInfo info = PopulateTile_Percent10(group);
            info.InvertColors = true;
            return info;
        }

        private static TileInfo PopulateTile_Percent90(GeometryGroup group)
        {
            TileInfo info = PopulateTile_Percent5(group);
            info.InvertColors = true;
            return info;
        }

        private static TileInfo PopulateTile_Plaid(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            group.Children.Add(CreateLine(0, 1, 3, 1));
            group.Children.Add(CreateLine(0, 2, 3, 2));
            group.Children.Add(CreateLine(0, 3, 3, 3));
            group.Children.Add(CreateLine(0, 6, 1, 7));
            group.Children.Add(CreateLine(0, 4, 3, 7));
            group.Children.Add(CreateLine(2, 4, 5, 7));
            group.Children.Add(CreateLine(4, 4, 7, 7));
            group.Children.Add(CreateLine(6, 4, 7, 5));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Shingle(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 2));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(2, 0));
            group.Children.Add(CreateLine(3, 0, 6, 3));
            group.Children.Add(CreatePoint(7, 3));
            group.Children.Add(CreatePoint(0, 4));
            group.Children.Add(CreateLine(1, 4, 3, 6));
            group.Children.Add(CreatePoint(3, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_SmallCheckerBoard(GeometryGroup group)
        {
            group.Children.Add(CreateRectangle(0, 0, 2, 2));
            group.Children.Add(CreateRectangle(2, 2, 2, 2));
            return new TileInfo(4, false);
        }

        private static TileInfo PopulateTile_SmallConfetti(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 4));
            group.Children.Add(CreatePoint(1, 0));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 5));
            group.Children.Add(CreatePoint(4, 7));
            group.Children.Add(CreatePoint(5, 3));
            group.Children.Add(CreatePoint(6, 1));
            group.Children.Add(CreatePoint(7, 6));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_SmallGrid(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            group.Children.Add(CreateLine(0, 0, 0, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_SolidDiamond(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreateLine(1, 2, 1, 4));
            group.Children.Add(CreateLine(2, 1, 2, 5));
            group.Children.Add(CreateLine(3, 0, 3, 6));
            group.Children.Add(CreateLine(4, 1, 4, 5));
            group.Children.Add(CreateLine(5, 2, 5, 4));
            group.Children.Add(CreatePoint(6, 3));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Sphere(GeometryGroup group)
        {
            group.Children.Add(CreateLine(1, 0, 3, 0));
            group.Children.Add(CreateLine(5, 0, 7, 0));
            group.Children.Add(CreatePoint(0, 1));
            group.Children.Add(CreateLine(3, 1, 4, 1));
            group.Children.Add(CreateLine(0, 2, 4, 2));
            group.Children.Add(CreateLine(0, 3, 4, 3));
            group.Children.Add(CreateLine(1, 4, 3, 4));
            group.Children.Add(CreateLine(5, 4, 7, 4));
            group.Children.Add(CreatePoint(4, 5));
            group.Children.Add(CreatePoint(7, 5));
            group.Children.Add(CreateLine(4, 6, 7, 6));
            group.Children.Add(CreateLine(4, 7, 7, 7));
            group.Children.Add(CreateLine(0, 5, 0, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Trellis(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 3, 0));
            group.Children.Add(CreateLine(1, 1, 2, 1));
            group.Children.Add(CreateLine(0, 2, 3, 2));
            group.Children.Add(CreatePoint(0, 3));
            group.Children.Add(CreatePoint(3, 3));
            return new TileInfo(4, true);
        }

        private static TileInfo PopulateTile_Wave(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 1));
            group.Children.Add(CreatePoint(1, 2));
            group.Children.Add(CreatePoint(2, 2));
            group.Children.Add(CreatePoint(3, 1));
            group.Children.Add(CreatePoint(4, 0));
            group.Children.Add(CreatePoint(5, 0));
            group.Children.Add(CreatePoint(6, 1));
            group.Children.Add(CreatePoint(0, 5));
            group.Children.Add(CreatePoint(1, 6));
            group.Children.Add(CreatePoint(2, 6));
            group.Children.Add(CreatePoint(3, 5));
            group.Children.Add(CreatePoint(4, 4));
            group.Children.Add(CreatePoint(5, 4));
            group.Children.Add(CreatePoint(6, 5));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_Weave(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 0, 6, 6));
            group.Children.Add(CreateLine(0, 4, 3, 7));
            group.Children.Add(CreatePoint(1, 7));
            group.Children.Add(CreatePoint(4, 0));
            group.Children.Add(CreatePoint(5, 3));
            group.Children.Add(CreatePoint(5, 7));
            group.Children.Add(CreatePoint(6, 2));
            group.Children.Add(CreatePoint(7, 1));
            group.Children.Add(CreatePoint(7, 3));
            group.Children.Add(CreatePoint(7, 5));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_WideDownwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreateLine(0, 3, 4, 7));
            group.Children.Add(CreateLine(0, 2, 5, 7));
            group.Children.Add(CreateLine(0, 1, 6, 7));
            group.Children.Add(CreateLine(5, 0, 7, 2));
            group.Children.Add(CreateLine(6, 0, 7, 1));
            group.Children.Add(CreatePoint(7, 0));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_WideUpwardDiagonal(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreatePoint(1, 0));
            group.Children.Add(CreatePoint(2, 0));
            group.Children.Add(CreatePoint(0, 1));
            group.Children.Add(CreatePoint(1, 1));
            group.Children.Add(CreatePoint(0, 2));
            group.Children.Add(CreatePoint(7, 1));
            group.Children.Add(CreatePoint(6, 2));
            group.Children.Add(CreatePoint(7, 2));
            group.Children.Add(CreatePoint(5, 3));
            group.Children.Add(CreatePoint(6, 3));
            group.Children.Add(CreatePoint(7, 3));
            group.Children.Add(CreatePoint(4, 4));
            group.Children.Add(CreatePoint(5, 4));
            group.Children.Add(CreatePoint(6, 4));
            group.Children.Add(CreatePoint(3, 5));
            group.Children.Add(CreatePoint(4, 5));
            group.Children.Add(CreatePoint(5, 5));
            group.Children.Add(CreatePoint(2, 6));
            group.Children.Add(CreatePoint(3, 6));
            group.Children.Add(CreatePoint(4, 6));
            group.Children.Add(CreatePoint(1, 7));
            group.Children.Add(CreatePoint(2, 7));
            group.Children.Add(CreatePoint(3, 7));
            return new TileInfo(8, true);
        }

        private static TileInfo PopulateTile_ZigZag(GeometryGroup group)
        {
            group.Children.Add(CreatePoint(0, 0));
            group.Children.Add(CreateLine(1, 0, 4, 3));
            group.Children.Add(CreatePoint(5, 3));
            group.Children.Add(CreatePoint(6, 2));
            group.Children.Add(CreatePoint(7, 1));
            group.Children.Add(CreatePoint(0, 4));
            group.Children.Add(CreateLine(1, 4, 4, 7));
            group.Children.Add(CreatePoint(5, 7));
            group.Children.Add(CreatePoint(6, 6));
            group.Children.Add(CreatePoint(7, 5));
            return new TileInfo(8, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TileInfo
        {
            private int length;
            private bool containsLine;
            private bool absoluteViewBoxMapping;
            private bool invertColors;
            private bool dottedPen;
            internal TileInfo(int length, bool containsLine)
            {
                this.length = length;
                this.containsLine = containsLine;
                this.absoluteViewBoxMapping = false;
                this.invertColors = false;
                this.dottedPen = false;
            }

            internal int Length
            {
                get => 
                    this.length;
                set => 
                    this.length = value;
            }
            internal bool ContainsLine
            {
                get => 
                    this.containsLine;
                set => 
                    this.containsLine = value;
            }
            internal bool AbsoluteViewBoxMapping
            {
                get => 
                    this.absoluteViewBoxMapping;
                set => 
                    this.absoluteViewBoxMapping = value;
            }
            internal bool InvertColors
            {
                get => 
                    this.invertColors;
                set => 
                    this.invertColors = value;
            }
            internal bool DottedPen
            {
                get => 
                    this.dottedPen;
                set => 
                    this.dottedPen = value;
            }
        }
    }
}

