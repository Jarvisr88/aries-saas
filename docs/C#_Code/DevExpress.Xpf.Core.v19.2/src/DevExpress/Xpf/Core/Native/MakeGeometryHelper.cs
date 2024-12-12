namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Svg;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    internal static class MakeGeometryHelper
    {
        private static TextDecoration CreateTextDecoration(SvgTextDecoration svgTextDecoration);
        private static FillRule GetFillRule(SvgElement svgElement);
        private static PointCollection GetPointCollection(SvgPolygon polyElement);
        private static T GetSvgElementById<T>(string stringId, SvgElement findFrom) where T: SvgElement;
        private static SvgRoot GetSvgRoot(SvgElement findFrom);
        private static Point GetTextOrigin(SvgText svgTextParent, double baseline, double textWidth);
        private static Typeface GetTypeface(SvgText svgTextParent);
        private static EllipseGeometry MakeCircleGeometry(SvgCircle svgCircle);
        private static Geometry MakeClipElementGeometry<TSvgElement>(TSvgElement svgElement) where TSvgElement: SvgElement;
        private static Geometry MakeClipElementGeometryWithTransform<TSvgElement>(TSvgElement svgElement) where TSvgElement: SvgElement;
        private static PathGeometry MakeClipGeometry(SvgClipPath clip);
        private static GeometryCollection MakeClipGeometryCollection(SvgClipPath clip);
        private static EllipseGeometry MakeEllipseGeometry(SvgEllipse svgEllipse);
        private static Geometry MakeGeometryWithTransform(Geometry geometry, SvgTransformCollection transforms);
        public static string MakeIdFromString(string id);
        public static string MakeIdFromUri(Uri uriId);
        private static LineGeometry MakeLineGeometry(SvgLine svgLine);
        private static StreamGeometry MakePathGeometry(SvgPath svgPath);
        private static StreamGeometry MakePolyElementGeometry(SvgPolygon polyElement);
        private static StreamGeometry MakePolygonGeometry(SvgPolygon svgPolygon);
        private static StreamGeometry MakePolylineGeometry(SvgPolyline svgPolyline);
        private static RectangleGeometry MakeRectangleGeometry(SvgRectangle svgRectangle);
        public static Geometry MakeSvgElementGeometryCore<TSvgElement>(TSvgElement svgElement, bool makeFrozen = true) where TSvgElement: SvgElement;
        private static void MakeSvgPolygonGeometry(StreamGeometry streamGeometry, PointCollection pointsCollection);
        private static void MakeSvgPolylineGeometry(StreamGeometry streamGeometry, PointCollection pointsCollection);
        private static Geometry MakeTextGeometry(SvgContent svgContent);
        private static string NormalizeContent(this string input);
        private static FontStyle ToFontStyle(this SvgFontStyle fontStyle);
        private static FontWeight ToFontWeight(this SvgFontWeight fontWeight);
    }
}

