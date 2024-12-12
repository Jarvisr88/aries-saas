namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SvgElementWrapper
    {
        private GraphicsPathCache pathCacheCore;
        private List<SvgElementWrapper> childsCore;
        private SvgBitmap ownerCore;
        private Type[] indirectElementTypes = new Type[] { typeof(SvgDefinitionsWrapper), typeof(SvgClipPathWrapper) };
        protected ISvgGraphics graphics;

        public SvgElementWrapper(SvgElement element)
        {
            this.Element = element;
            this.pathCacheCore = new GraphicsPathCache();
            this.childsCore = new List<SvgElementWrapper>();
        }

        protected virtual void AddPathToCache(double scale, GraphicsPath path)
        {
            if (!this.PathCache.ContainsKey(scale))
            {
                this.PathCache.Add(scale, path);
            }
        }

        public static Color ChangeColorBrightness(Color baseColor, double brightness)
        {
            brightness = Math.Min(1.0, Math.Max(-1.0, brightness));
            Func<byte, byte> func = (brightness <= 0.0) ? x => ((byte) (x * (1.0 + brightness))) : x => ((byte) (x + ((0xff - x) * brightness)));
            return Color.FromArgb(baseColor.A, func(baseColor.R), func(baseColor.G), func(baseColor.B));
        }

        internal bool CheckPathData(GraphicsPath path)
        {
            if (path.PathData.Points.Length == 0)
            {
                return true;
            }
            PointF tf = path.PathData.Points.First<PointF>();
            for (int i = 1; i < path.PathData.Points.Length; i++)
            {
                if (tf != path.PathData.Points[i])
                {
                    return true;
                }
            }
            return false;
        }

        private SvgElementWrapper FindBrush(string name) => 
            this.Owner.GetElementWrapperById(name);

        public RectangleF GetBounds(Matrix transformMatrix = null)
        {
            bool flag = ReferenceEquals(transformMatrix, null);
            transformMatrix ??= new Matrix();
            double scale = 1.0;
            transformMatrix = this.GetTransform(transformMatrix, scale, true);
            if (this is SvgUseWrapper)
            {
                RectangleF bounds;
                SvgElementWrapper referencedElement = (this as SvgUseWrapper).ReferencedElement;
                if (referencedElement != null)
                {
                    bounds = referencedElement.GetBounds(transformMatrix);
                }
                else
                {
                    SvgElementWrapper local1 = referencedElement;
                    bounds = new RectangleF();
                }
                RectangleF ef2 = bounds;
                if (flag)
                {
                    transformMatrix.Dispose();
                }
                return ef2;
            }
            List<float> xs = new List<float>();
            List<float> ys = new List<float>();
            RectangleF empty = RectangleF.Empty;
            using (Pen pen = this.GetPen(scale))
            {
                using (GraphicsPath path = (GraphicsPath) this.GetPath(scale).Clone())
                {
                    using (Matrix matrix = new Matrix())
                    {
                        if ((pen != null) && this.CheckPathData(path))
                        {
                            path.Widen(pen);
                        }
                        path.Transform(transformMatrix);
                        path.Flatten(matrix, 0.01f);
                        empty = path.GetBounds();
                    }
                }
            }
            this.PopulateCoordinates(empty, xs, ys);
            foreach (SvgElementWrapper child in this.Childs)
            {
                if (!this.indirectElementTypes.Any<Type>(x => x.IsAssignableFrom(child.GetType())))
                {
                    RectangleF bounds = child.GetBounds(transformMatrix);
                    this.PopulateCoordinates(bounds, xs, ys);
                }
            }
            if (flag)
            {
                transformMatrix.Dispose();
            }
            if ((xs.Count == 0) || (ys.Count == 0))
            {
                return RectangleF.Empty;
            }
            float num2 = ((IEnumerable<float>) xs).Min();
            float y = ((IEnumerable<float>) ys).Min();
            return new RectangleF(num2, y, ((IEnumerable<float>) xs).Max() - num2, ((IEnumerable<float>) ys).Max() - y);
        }

        public virtual Brush GetBrush(ISvgGraphics g, SvgElement element, double scale, string colorValue, double? opacity, bool useStyleName, GraphicsPath path)
        {
            opacity = this.GetOpacity(element.Opacity, opacity);
            if (colorValue.StartsWith("url"))
            {
                SvgElementWrapper wrapper = this.FindBrush(colorValue);
                if (wrapper != null)
                {
                    wrapper.PaletteProvider = this.PaletteProvider;
                    return wrapper.GetBrush(g, element, scale, colorValue, opacity, useStyleName, path);
                }
            }
            SvgGradient svgGradient = null;
            Color color = this.GetColor(colorValue, opacity, element.Brightness, element.UsePalette, out svgGradient, true);
            return ((svgGradient == null) ? new SolidBrush(color) : new SvgLinearGradientWrapper(svgGradient).GetBrush(g, element, scale, colorValue, opacity, useStyleName, path));
        }

        public Color GetColor(ISvgPaletteProvider provider, string colorValue, double? opacity, double? brightness, bool usePalette)
        {
            this.PaletteProvider = provider;
            return this.GetColor(colorValue, opacity, brightness, usePalette, true);
        }

        protected virtual Color GetColor(string colorValue, double? opacity, double? brightness, bool usePalette, bool useStyleName = true)
        {
            SvgGradient svgGradient = null;
            return this.GetColor(colorValue, opacity, brightness, usePalette, out svgGradient, useStyleName);
        }

        protected virtual Color GetColor(string colorValue, double? opacity, double? brightness, bool usePalette, out SvgGradient svgGradient, bool useStyleName = true)
        {
            svgGradient = null;
            Color black = Color.Black;
            if (string.IsNullOrEmpty(colorValue))
            {
                colorValue = "#000000";
            }
            if (colorValue.StartsWith("rgb"))
            {
                colorValue = this.GetStandardColorValue(colorValue, ref opacity);
            }
            if ((colorValue.Length == 4) && (colorValue[0] == '#'))
            {
                colorValue = string.Format("#{0}{0}{1}{1}{2}{2}", colorValue[1], colorValue[2], colorValue[3]);
            }
            if (colorValue == SvgUnit.None)
            {
                return Color.Transparent;
            }
            if ((this.PaletteProvider != null) & usePalette)
            {
                black = !(this.PaletteProvider is ISvgPaletteProviderExt) ? this.PaletteProvider.GetColorByStyleName(useStyleName ? this.Element.StyleName : string.Empty, colorValue, this.Element.Tag) : (this.PaletteProvider as ISvgPaletteProviderExt).GetColorByStyleName(useStyleName ? this.Element.StyleName : string.Empty, colorValue, out svgGradient, this.Element.Tag);
            }
            else
            {
                if (colorValue.StartsWith("url"))
                {
                    return Color.Black;
                }
                if (string.Equals(colorValue, "currentColor", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Color.Black;
                }
                try
                {
                    black = ColorTranslator.FromHtml(colorValue);
                }
                catch
                {
                    black = Color.Black;
                }
            }
            if (opacity != null)
            {
                black = Color.FromArgb((int) (opacity.Value * black.A), black);
            }
            if (brightness != null)
            {
                black = ChangeColorBrightness(black, brightness.Value);
            }
            return black;
        }

        protected internal Pen GetHitTestPen(double scale, double strokeWidth)
        {
            Pen pen = new Pen(Brushes.Transparent, (float) strokeWidth) {
                MiterLimit = (this.Element.StrokeMiterLimit != null) ? ((float) this.Element.StrokeMiterLimit.Value) : 4f,
                LineJoin = (LineJoin) this.Element.StrokeLineJoin
            };
            SvgUnitCollection strokeDashArray = this.Element.StrokeDashArray;
            if (<>c.<>9__48_0 == null)
            {
                SvgUnitCollection local1 = this.Element.StrokeDashArray;
                strokeDashArray = (SvgUnitCollection) (<>c.<>9__48_0 = x => x.Count > 0);
            }
            if (((SvgUnitCollection) <>c.<>9__48_0).Get<SvgUnitCollection, bool>((Func<SvgUnitCollection, bool>) strokeDashArray, false))
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = this.Element.StrokeDashArray.Get<SvgUnitCollection, float[]>(delegate (SvgUnitCollection x) {
                    Func<SvgUnit, float> <>9__2;
                    Func<SvgUnit, float> selector = <>9__2;
                    if (<>9__2 == null)
                    {
                        Func<SvgUnit, float> local1 = <>9__2;
                        selector = <>9__2 = delegate (SvgUnit c) {
                            float num = (float) ((c.Value * scale) / strokeWidth);
                            return (num <= 0f) ? 1f : num;
                        };
                    }
                    return x.Select<SvgUnit, float>(selector).ToArray<float>();
                }, pen.DashPattern);
                if (pen.DashPattern.Count<float>() == 1)
                {
                    pen.DashPattern = new float[] { pen.DashPattern[0], pen.DashPattern[0] };
                }
                pen.DashOffset = this.Element.StrokeDashOffset.Get<SvgUnit, float>(x => (float) (x.Value * scale), pen.DashOffset);
            }
            SvgStrokeLineCap strokeLineCap = this.Element.StrokeLineCap;
            if (strokeLineCap == SvgStrokeLineCap.Round)
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
            }
            else if (strokeLineCap == SvgStrokeLineCap.Square)
            {
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
            }
            return pen;
        }

        protected double? GetOpacity(double? elementOpacity, double? additionOpacity)
        {
            if ((elementOpacity != null) || (additionOpacity != null))
            {
                return new double?(Math.Min(Math.Max((double) (elementOpacity.GetValueOrDefault(1.0) * additionOpacity.GetValueOrDefault(1.0)), (double) 0.0), 1.0));
            }
            return null;
        }

        public GraphicsPath GetPath(double scale)
        {
            GraphicsPath pathCore;
            if (!this.PathCache.TryGetValue(scale, out pathCore))
            {
                pathCore = this.GetPathCore(scale);
                this.AddPathToCache(scale, pathCore);
            }
            return pathCore;
        }

        protected virtual GraphicsPath GetPathCore(double scale) => 
            new GraphicsPath();

        public Pen GetPen(double scale)
        {
            if (string.IsNullOrEmpty(this.Element.Stroke) || string.Equals(this.Element.Stroke.ToLower(), SvgUnit.None))
            {
                return null;
            }
            double? brightness = null;
            using (SolidBrush brush = new SolidBrush(this.GetColor(this.Element.Stroke, this.GetOpacity(this.Element.Opacity, this.Element.StrokeOpacity), brightness, this.Element.UsePalette, true)))
            {
                return this.GetPenCore(brush, scale);
            }
        }

        protected virtual Pen GetPenCore(Brush brush, double scale)
        {
            Func<SvgUnit, double> get = <>c.<>9__50_0;
            if (<>c.<>9__50_0 == null)
            {
                Func<SvgUnit, double> local1 = <>c.<>9__50_0;
                get = <>c.<>9__50_0 = x => x.Value;
            }
            double strokeWidth = this.Element.StrokeWidth.Get<SvgUnit, double>(get, 1.0) * scale;
            if (strokeWidth == 0.0)
            {
                return new Pen(Color.Transparent);
            }
            Pen pen = new Pen(brush, (float) strokeWidth) {
                MiterLimit = (this.Element.StrokeMiterLimit != null) ? ((float) this.Element.StrokeMiterLimit.Value) : 4f,
                LineJoin = (LineJoin) this.Element.StrokeLineJoin
            };
            SvgUnitCollection strokeDashArray = this.Element.StrokeDashArray;
            if (<>c.<>9__50_1 == null)
            {
                SvgUnitCollection local2 = this.Element.StrokeDashArray;
                strokeDashArray = (SvgUnitCollection) (<>c.<>9__50_1 = x => x.Count > 0);
            }
            if (((SvgUnitCollection) <>c.<>9__50_1).Get<SvgUnitCollection, bool>((Func<SvgUnitCollection, bool>) strokeDashArray, false))
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = this.Element.StrokeDashArray.Get<SvgUnitCollection, float[]>(delegate (SvgUnitCollection x) {
                    Func<SvgUnit, float> <>9__3;
                    Func<SvgUnit, float> selector = <>9__3;
                    if (<>9__3 == null)
                    {
                        Func<SvgUnit, float> local1 = <>9__3;
                        selector = <>9__3 = delegate (SvgUnit c) {
                            float num = (float) ((c.Value * scale) / strokeWidth);
                            return (num <= 0f) ? 1f : num;
                        };
                    }
                    return x.Select<SvgUnit, float>(selector).ToArray<float>();
                }, pen.DashPattern);
                if (pen.DashPattern.Count<float>() == 1)
                {
                    pen.DashPattern = new float[] { pen.DashPattern[0], pen.DashPattern[0] };
                }
                pen.DashOffset = this.Element.StrokeDashOffset.Get<SvgUnit, float>(x => (float) (x.Value * scale), pen.DashOffset);
            }
            SvgStrokeLineCap strokeLineCap = this.Element.StrokeLineCap;
            if (strokeLineCap == SvgStrokeLineCap.Round)
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
            }
            else if (strokeLineCap == SvgStrokeLineCap.Square)
            {
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
            }
            return pen;
        }

        protected virtual SmoothingMode GetSmoothingMode(SmoothingMode defaultValue, double scale) => 
            ((scale < 16.0) || (SvgBitmap.SvgImageRenderingMode == SvgImageRenderingMode.HighSpeed)) ? this.GetSmoothingModeCore(defaultValue) : defaultValue;

        protected virtual SmoothingMode GetSmoothingModeCore(SmoothingMode defaultValue) => 
            defaultValue;

        protected virtual string GetStandardColorValue(string colorValue, ref double? opacity)
        {
            Color color;
            int startIndex = colorValue.IndexOf("(") + 1;
            char[] separator = new char[] { ',', ' ' };
            string[] strArray = colorValue.Substring(startIndex, colorValue.IndexOf(")") - startIndex).Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length > 3)
            {
                string s = strArray[3];
                if (s.StartsWith("."))
                {
                    s = "0" + s;
                }
                double num2 = double.Parse(s, CultureInfo.InvariantCulture);
                opacity = (num2 > 1.0) ? new double?(num2 / 255.0) : new double?(num2);
            }
            if (!strArray[0].Trim().EndsWith("%"))
            {
                color = Color.FromArgb(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]));
            }
            else
            {
                char[] trimChars = new char[] { '%' };
                char[] chArray3 = new char[] { '%' };
                char[] chArray4 = new char[] { '%' };
                color = Color.FromArgb((int) ((255.0 * double.Parse(strArray[0].Trim().TrimEnd(trimChars), CultureInfo.InvariantCulture)) / 100.0), (int) ((255.0 * double.Parse(strArray[1].Trim().TrimEnd(chArray3), CultureInfo.InvariantCulture)) / 100.0), (int) ((255.0 * double.Parse(strArray[2].Trim().TrimEnd(chArray4), CultureInfo.InvariantCulture)) / 100.0));
            }
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public virtual Brush GetStrokeBrush(ISvgGraphics g, SvgElement element, double scale, string strokeValue, double? opacity, GraphicsPath path)
        {
            bool useStyleName = false;
            if (strokeValue.StartsWith("url"))
            {
                SvgElementWrapper wrapper = this.FindBrush(strokeValue);
                if (wrapper != null)
                {
                    wrapper.PaletteProvider = this.PaletteProvider;
                    return wrapper.GetBrush(g, element, scale, strokeValue, opacity, useStyleName, path);
                }
            }
            SvgGradient svgGradient = null;
            double? brightness = null;
            Color color = this.GetColor(strokeValue, opacity, brightness, element.UsePalette, out svgGradient, useStyleName);
            return ((svgGradient == null) ? new SolidBrush(color) : new SvgLinearGradientWrapper(svgGradient).GetBrush(g, element, scale, strokeValue, opacity, useStyleName, path));
        }

        protected internal virtual Matrix GetTransform(Matrix m, double scale, bool clone = false)
        {
            Matrix matrix = m;
            if (clone)
            {
                matrix = m.Clone();
            }
            if (this.Element.Transformations != null)
            {
                foreach (SvgTransform transform in this.Element.Transformations)
                {
                    matrix.Multiply(transform.GetMatrix(scale));
                }
            }
            return matrix;
        }

        private void PopulateCoordinates(RectangleF rect, List<float> xs, List<float> ys)
        {
            if (!rect.IsEmpty)
            {
                float[] collection = new float[] { rect.X, rect.Right };
                xs.AddRange(collection);
                float[] singleArray2 = new float[] { rect.Y, rect.Bottom };
                ys.AddRange(singleArray2);
            }
        }

        public virtual void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
            if (this.IsDisplay)
            {
                this.PaletteProvider = paletteProvider;
                this.Scale = scale;
                object graphicsState = g.Save();
                Matrix matrix = this.GetTransform(g.Transform, scale, false);
                g.Transform = matrix;
                this.SetClip(g, scale);
                g.SmoothingMode = this.GetSmoothingMode(g.SmoothingMode, scale);
                this.RenderCore(g, scale);
                this.RenderChild(g, paletteProvider, scale, this);
                matrix.Dispose();
                g.Restore(graphicsState);
            }
        }

        protected virtual void RenderChild(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale, SvgElementWrapper elementWrapper)
        {
            foreach (SvgElementWrapper wrapper in elementWrapper.Childs)
            {
                wrapper.Render(g, paletteProvider, scale);
            }
        }

        protected virtual void RenderCore(ISvgGraphics g, double scale)
        {
            this.RenderElement(g, scale, this.Element);
            this.RenderStroke(g, scale, this.Element);
        }

        protected virtual void RenderElement(ISvgGraphics g, double scale, SvgElement element)
        {
            this.graphics = g;
            GraphicsPath path = this.GetPath(scale);
            using (Brush brush = this.GetBrush(g, element, scale, element.Fill, element.FillOpacity, true, path))
            {
                g.FillPath(brush, path);
            }
            this.graphics = null;
        }

        protected virtual void RenderStroke(ISvgGraphics g, double scale, SvgElement element)
        {
            if (!string.IsNullOrEmpty(element.Stroke) && !string.Equals(element.Stroke.ToLower(), SvgUnit.None))
            {
                GraphicsPath path = this.GetPath(scale);
                using (Brush brush = this.GetStrokeBrush(g, element, scale, element.Stroke, this.GetOpacity(element.Opacity, element.StrokeOpacity), path))
                {
                    using (Pen pen = this.GetPenCore(brush, scale))
                    {
                        g.DrawPath(pen, this.GetPath(scale));
                    }
                }
            }
        }

        public float ScaleValue(double v, double scale) => 
            (scale != 1.0) ? ((float) (v * scale)) : ((float) v);

        protected virtual void SetClip(ISvgGraphics g, double scale)
        {
            if (this.Element.ClipPath != null)
            {
                SvgClipPathWrapper elementWrapperById = this.Owner.GetElementWrapperById(this.Element.ClipPath.ToString()) as SvgClipPathWrapper;
                if (elementWrapperById != null)
                {
                    g.SetClip(elementWrapperById.GetPath(scale), CombineMode.Intersect);
                }
            }
        }

        protected void SetElement(SvgElement value)
        {
            this.Element = value;
        }

        protected GraphicsPathCache PathCache =>
            this.pathCacheCore;

        protected internal SvgBitmap Owner
        {
            get => 
                this.ownerCore;
            set => 
                this.ownerCore = value;
        }

        protected ISvgPaletteProvider PaletteProvider { get; set; }

        public double Scale { get; internal set; }

        public SvgElement Element { get; private set; }

        public List<SvgElementWrapper> Childs =>
            this.childsCore;

        public bool IsDisplay =>
            string.IsNullOrEmpty(this.Element.Display) || !string.Equals(this.Element.Display.ToLower(), SvgUnit.None);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgElementWrapper.<>c <>9 = new SvgElementWrapper.<>c();
            public static Func<SvgUnitCollection, bool> <>9__48_0;
            public static Func<SvgUnit, double> <>9__50_0;
            public static Func<SvgUnitCollection, bool> <>9__50_1;

            internal bool <GetHitTestPen>b__48_0(SvgUnitCollection x) => 
                x.Count > 0;

            internal double <GetPenCore>b__50_0(SvgUnit x) => 
                x.Value;

            internal bool <GetPenCore>b__50_1(SvgUnitCollection x) => 
                x.Count > 0;
        }
    }
}

