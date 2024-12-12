namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Svg;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class WpfSvgRenderer
    {
        private static readonly Brush frozenTransparentBrush;
        private readonly SvgImage image;
        private readonly SvgBitmap bitmap;
        private readonly bool isSvgImageHasOnlySvgTransformGroup;
        private readonly List<WpfSvgPalette> palettes;
        private readonly string actualState;
        private readonly List<SvgGradient> gradients;

        static WpfSvgRenderer();
        public WpfSvgRenderer(SvgImage image, WpfSvgPalette basePalette, string actualState);
        public WpfSvgRenderer(SvgImage image, WpfSvgPalette currentPalette, WpfSvgPalette attachedPalette, WpfSvgPalette basePalette, string actualState);
        private static void ApplyMiterLimit(SvgElement svgElement, Pen pen);
        private static void ApplyStrokeDash(SvgElement svgElement, Pen pen);
        private static void ApplyStrokeLineCap(SvgElement svgElement, Pen pen);
        private static void ApplyStrokeLineJoin(SvgElement svgElement, Pen pen);
        private bool ApplySvgElementTransformations(SvgElement svgElement, DrawingContext drawingContext);
        private bool ApplySvgViewboxTransformations(DrawingContext drawingContext);
        private WpfSvgStartEndPoints CalcStartEndPoints(SvgLinearGradient linearGradient, BrushMappingMode mappingMode);
        private static bool CheckSvgRootHasOnlySvgTransformGroup(SvgImage svgImage);
        private static bool CheckUnsupportedElements(SvgElement svgElement);
        private Point CoercePointValues(Point start, SvgUnit xSvgUnit, SvgUnit ySvgUnit);
        private static double CoerceValue(SvgUnit svgUnit, double distance, double scale);
        internal static Transform ConvertToFrozenTransform(SvgTransformCollection transformations);
        private Drawing CreateDrawing(WpfSvgScale targetScale);
        public static Drawing CreateDrawing(SvgImage image, Size targetSize, WpfSvgPalette palette, string state);
        public static Drawing CreateDrawing(SvgImage image, Size targetSize, WpfSvgPalette palette, WpfSvgPalette basePalette, string state);
        private static Drawing CreateDrawingCore(SvgImage image, Size targetSize, WpfSvgPalette currentPalette, WpfSvgPalette attachedPalette, WpfSvgPalette basePalette, string state);
        public static ImageSource CreateImageSource(SvgImage image, double scale, WpfSvgPalette palette, string state, bool autoSize);
        public static ImageSource CreateImageSource(SvgImage image, Size targetSize, WpfSvgPalette palette, string state, bool autoSize);
        public static ImageSource CreateImageSource(Stream stream, double scale, WpfSvgPalette palette, string state, bool autoSize);
        public static ImageSource CreateImageSource(Stream stream, Size targetSize, WpfSvgPalette palette, string state, bool autoSize);
        public static ImageSource CreateImageSource(SvgImage image, double scale, WpfSvgPalette palette, WpfSvgPalette basePalette, string state, bool autoSize);
        public static ImageSource CreateImageSource(SvgImage image, Size targetSize, WpfSvgPalette palette, WpfSvgPalette basePalette, string state, bool autoSize);
        public static ImageSource CreateImageSource(Stream stream, double scale, WpfSvgPalette palette, WpfSvgPalette basePalette = null, string state = null, bool autoSize = true);
        public static ImageSource CreateImageSource(Stream stream, Size targetSize, WpfSvgPalette palette, WpfSvgPalette basePalette, string state, bool autoSize);
        public static ImageSource CreateImageSource(Uri imageUri, Uri baseUri = null, Size? targetSize = new Size?(), WpfSvgPalette palette = null, string state = null, bool? autoSize = new bool?());
        public static ImageSource CreateImageSource(Uri imageUri, Uri baseUri, Size? targetSize, WpfSvgPalette palette, WpfSvgPalette basePalette, string state, bool? autoSize);
        internal static ImageSource CreateImageSourceCore(SvgImage image, Size? targetSize, WpfSvgPalette currentPalette, WpfSvgPalette attachedPalette, WpfSvgPalette basePalette, string state, bool? autoSize, Action<DrawingImage> actionBeforeFreeze = null, Uri imageUri = null, Uri baseUri = null);
        public static void DrawToDrawingContext(SvgImage image, WpfSvgPalette palette, DrawingContext drawingContext, string state = null);
        public static void DrawToDrawingContext(SvgImage image, WpfSvgPalette palette, WpfSvgPalette basePalette, DrawingContext drawingContext, string state = null);
        private void DrawWithDrawingContext(DrawingContext drawingContext);
        private void DrawWithDrawingContext(Point offset, WpfSvgScale targetScale, DrawingContext drawingContext);
        internal void DrawWithDrawingContext(Point offset, Size targetSize, DrawingContext drawingContext);
        internal static TFreezable Freeze<TFreezable>(TFreezable freezable) where TFreezable: Freezable;
        private Brush GenerateFill(SvgElement element);
        private Pen GenerateStroke(SvgElement element);
        private static bool GetBrushFromPalette(WpfSvgPalette palette, string color, string state, string styleName, ref Brush result);
        private Color GetColor(SvgElement svgElement, string color, string styleName);
        private static bool GetColorFromPalette(WpfSvgPalette palette, string color, string state, string styleName, ref Color result);
        private Color GetColorOrDefault(string colorKey);
        private GradientBrush GetGradientBrush(SvgGradient gradient, SvgElement svgElement, string styleName);
        private GradientStopCollection GetGradientStopCollection(SvgElement svgElement, string styleName, SvgGradient linearGradient);
        private GradientBrush GetLinearGradientBrush(SvgGradient gradient, SvgElement svgElement, string styleName);
        private GradientBrush GetRadialGradientBrush(SvgGradient gradient, SvgElement svgElement, string styleName);
        private Brush GetSolidBrush(SvgElement svgElement, string color, string styleName);
        private static double GetStrokeWidth(SvgElement svgElement);
        private T GetSvgElementById<T>(string stringId) where T: SvgElement;
        private static SvgGradient GetSvgGradientById(IEnumerable<SvgGradient> gradients, string stringId);
        private static SvgImage GetSvgImageFromUri(Uri imageUri, Uri baseUri);
        private static WpfSvgScale GetViewboxScale(SvgImage svgImage);
        private void PopSvgTransformations(SvgElement svgElement, DrawingContext drawingContext, AppliedSvgTransforms appliedSvgTransforms);
        private void RenderElement(SvgElement svgElement, DrawingContext drawingContext);
        private void RenderSvgCircle(SvgCircle svgCircle, DrawingContext drawingContext);
        private void RenderSvgElement(SvgElement svgElement, DrawingContext drawingContext);
        private void RenderSvgElementCore(SvgElement svgElement, DrawingContext drawingContext);
        private void RenderSvgEllipse(SvgEllipse svgEllipse, DrawingContext drawingContext);
        private void RenderSvgLine(SvgLine svgLine, DrawingContext drawingContext);
        private void RenderSvgPath(SvgPath svgPath, DrawingContext drawingContext);
        private void RenderSvgPolygon(SvgPolygon svgPolygon, DrawingContext drawingContext);
        private void RenderSvgPolyline(SvgPolyline svgPolyline, DrawingContext drawingContext);
        private void RenderSvgRectangle(SvgRectangle svgRectangle, DrawingContext drawingContext);
        private void RenderSvgText(SvgText svgText, DrawingContext drawingContext);
        private void RenderSvgUse(SvgUse svgUse, DrawingContext drawingContext);
        private bool SetClipPath(SvgElement svgElement, DrawingContext drawingContext);
        private static void SetElementOpacity(SvgElement svgElement, DrawingContext drawingContext);
        private AppliedSvgTransforms SetSvgTransformations(SvgElement svgElement, DrawingContext drawingContext);
        private Brush StringColorToBrush(SvgElement svgElement, string color, string styleName, double? opacity, double? brightness);
        private Color StringColorToColor(SvgElement svgElement, string color, string styleName);
        private static bool ValidateStringColor(ref string color, string styleName, SvgElement element);
        private static Matrix WinformMatrixToWpfMatrixConverter(Matrix winformMatrix);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WpfSvgRenderer.<>c <>9;
            public static Func<SvgUnit, double> <>9__33_0;
            public static Func<SvgUnit, double> <>9__33_1;
            public static Func<SvgGradientWrapper, SvgGradient> <>9__41_0;

            static <>c();
            internal SvgGradient <.ctor>b__41_0(SvgGradientWrapper x);
            internal double <ApplyStrokeDash>b__33_0(SvgUnit x);
            internal double <ApplyStrokeDash>b__33_1(SvgUnit x);
        }
    }
}

