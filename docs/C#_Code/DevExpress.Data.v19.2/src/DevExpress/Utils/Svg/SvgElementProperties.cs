namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgElementProperties
    {
        public SvgElementProperties()
        {
            this.StyleName = string.Empty;
            this.FillRule = SvgFillRule.NonZero;
            this.Fill = string.Empty;
            this.Stroke = string.Empty;
            this.StrokeLineCap = SvgStrokeLineCap.Butt;
            this.StrokeLineJoin = SvgStrokeLineJoin.Miter;
            this.FillRule = SvgFillRule.NonZero;
            this.Transformations = new SvgTransformCollection();
            this.UsePalette = true;
        }

        public string Id { get; set; }

        public string StyleName { get; set; }

        public SvgTransformCollection Transformations { get; private set; }

        public SvgStyle Style { get; set; }

        public string Display { get; set; }

        public string Fill { get; set; }

        public double? Opacity { get; set; }

        public double? FillOpacity { get; set; }

        public string Stroke { get; set; }

        public SvgUnit StrokeWidth { get; set; }

        public SvgStrokeLineCap StrokeLineCap { get; set; }

        public SvgStrokeLineJoin StrokeLineJoin { get; set; }

        public double? StrokeMiterLimit { get; set; }

        public SvgUnitCollection StrokeDashArray { get; set; }

        public SvgUnit StrokeDashOffset { get; set; }

        public double? StrokeOpacity { get; set; }

        public SvgFillRule FillRule { get; set; }

        public double? Brightness { get; set; }

        public bool UsePalette { get; set; }
    }
}

