namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    [FormatElement("scale")]
    public class SvgScale : SvgTransform
    {
        public SvgScale();
        public SvgScale(double x, double y);
        public override string GetTransform(IFormatProvider provider);
        public override Matrix GetTransformMatrix();

        public override bool IsIdentity { get; }
    }
}

