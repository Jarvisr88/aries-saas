namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    [FormatElement("translate")]
    public class SvgTranslate : SvgTransform
    {
        public SvgTranslate();
        public SvgTranslate(double x, double y);
        public override double GetDefaultY(double x);
        public override string GetTransform(IFormatProvider provider);
        public override Matrix GetTransformMatrix();
    }
}

