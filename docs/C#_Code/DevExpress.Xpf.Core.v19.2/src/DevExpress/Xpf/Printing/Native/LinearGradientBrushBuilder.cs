namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class LinearGradientBrushBuilder : BrushBuilder<System.Windows.Media.LinearGradientBrush>
    {
        public LinearGradientBrushBuilder(System.Windows.Media.LinearGradientBrush brush) : base(brush)
        {
        }

        public override DXBrush CreateDxBrush(Rect brushBounds)
        {
            System.Windows.Point startPoint = GetPoint(brushBounds, base.Brush.StartPoint, base.Brush);
            System.Windows.Point endPoint = GetPoint(brushBounds, base.Brush.EndPoint, base.Brush);
            UpdateBrushPoints(base.Brush.Transform, ref startPoint, ref endPoint);
            DXWrapMode mode = base.Brush.SpreadMethod.ToWrapMode();
            List<GradientStop> source = new List<GradientStop>(base.Brush.GradientStops);
            Comparison<GradientStop> comparison = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Comparison<GradientStop> local1 = <>c.<>9__2_0;
                comparison = <>c.<>9__2_0 = (x1, x2) => x1.Offset.CompareTo(x2.Offset);
            }
            source.Sort(comparison);
            Func<GradientStop, double> selector = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<GradientStop, double> local2 = <>c.<>9__2_1;
                selector = <>c.<>9__2_1 = x => x.Offset;
            }
            List<double> list2 = source.Select<GradientStop, double>(selector).ToList<double>();
            Func<GradientStop, ARGBColor> func2 = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<GradientStop, ARGBColor> local3 = <>c.<>9__2_2;
                func2 = <>c.<>9__2_2 = x => x.Color.ToColor();
            }
            List<ARGBColor> list3 = source.Select<GradientStop, ARGBColor>(func2).ToList<ARGBColor>();
            if (list2.First<double>() != 0.0)
            {
                list2.Insert(0, 0.0);
                list3.Insert(0, list3.First<ARGBColor>());
            }
            if (list2.Last<double>() != 1.0)
            {
                list2.Add(1.0);
                list3.Add(list3.Last<ARGBColor>());
            }
            if (mode == DXWrapMode.Clamp)
            {
                UpdateBrushParameters(brushBounds, ref startPoint, ref endPoint, list2, list3);
            }
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(startPoint.ToPointF(), endPoint.ToPointF(), System.Drawing.Color.Red, System.Drawing.Color.Blue);
            DXLinearGradientBrush brush1 = new DXLinearGradientBrush(brush.Rectangle.ToDxRectangleF(), list3.First<ARGBColor>(), list3.Last<ARGBColor>());
            brush1.Transform = brush.Transform.ToDXMatrix();
            DXLinearGradientBrush brush2 = brush1;
            brush2.WrapMode = mode;
            DXColorBlend blend1 = new DXColorBlend();
            blend1.Colors = list3.ToArray();
            blend1.Positions = list2.ToArray();
            brush2.InterpolationColors = blend1;
            return brush2;
        }

        private static System.Windows.Point GetPoint(Rect rect, System.Windows.Point offset, System.Windows.Media.LinearGradientBrush brush)
        {
            bool flag = brush.MappingMode == BrushMappingMode.RelativeToBoundingBox;
            return new System.Windows.Point(rect.X + (flag ? (rect.Width * offset.X) : offset.X), rect.Y + (flag ? (rect.Height * offset.Y) : offset.Y));
        }

        private static void UpdateBrushParameters(Rect brushBounds, ref System.Windows.Point startPoint, ref System.Windows.Point endPoint, List<double> positions, List<ARGBColor> colors)
        {
            Tuple<System.Windows.Point, System.Windows.Point> segmentPoints = MathHelper.GetVectorPoints(startPoint, endPoint, brushBounds);
            double colorsDistance = MathHelper.GetDistance(startPoint, endPoint);
            List<double> distances = new List<double>();
            Enumerable.Range(1, positions.Count - 1).ForEach<int>(i => distances.Add(colorsDistance * (positions[i] - positions[i - 1])));
            Tuple<bool, bool> tuple2 = MathHelper.AreInsideSegment(startPoint, endPoint, segmentPoints);
            if (tuple2.Item1 || tuple2.Item2)
            {
                if (tuple2.Item1)
                {
                    distances.Insert(0, MathHelper.GetDistance(segmentPoints.Item1, startPoint));
                    startPoint = segmentPoints.Item1;
                    ARGBColor item = colors.First<ARGBColor>();
                    colors.Insert(0, item);
                }
                if (tuple2.Item2)
                {
                    distances.Add(MathHelper.GetDistance(segmentPoints.Item2, endPoint));
                    endPoint = segmentPoints.Item2;
                    ARGBColor item = colors.Last<ARGBColor>();
                    colors.Add(item);
                }
                colorsDistance = MathHelper.GetDistance(startPoint, endPoint);
                double currentDistance = 0.0;
                positions.Clear();
                distances.ForEach(delegate (double d) {
                    positions.Add(currentDistance / colorsDistance);
                    currentDistance += d;
                });
                positions.Add(1.0);
            }
        }

        private static void UpdateBrushPoints(Transform transform, ref System.Windows.Point startPoint, ref System.Windows.Point endPoint)
        {
            if (transform != null)
            {
                if ((startPoint.X == endPoint.X) || (startPoint.Y == endPoint.Y))
                {
                    startPoint = transform.Transform(startPoint);
                    endPoint = transform.Transform(endPoint);
                }
                else
                {
                    Tuple<System.Windows.Point, System.Windows.Point> boundPointsToTransform = MathHelper.GetBoundPointsToTransform(startPoint, endPoint);
                    startPoint = transform.Transform(startPoint);
                    endPoint = MathHelper.GetNormalIntersectionPoint(transform.Transform(boundPointsToTransform.Item1), transform.Transform(boundPointsToTransform.Item2), startPoint);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinearGradientBrushBuilder.<>c <>9 = new LinearGradientBrushBuilder.<>c();
            public static Comparison<GradientStop> <>9__2_0;
            public static Func<GradientStop, double> <>9__2_1;
            public static Func<GradientStop, ARGBColor> <>9__2_2;

            internal int <CreateDxBrush>b__2_0(GradientStop x1, GradientStop x2) => 
                x1.Offset.CompareTo(x2.Offset);

            internal double <CreateDxBrush>b__2_1(GradientStop x) => 
                x.Offset;

            internal ARGBColor <CreateDxBrush>b__2_2(GradientStop x) => 
                x.Color.ToColor();
        }
    }
}

