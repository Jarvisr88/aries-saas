namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("skewX")]
    public class SvgSkewX : SvgSkew
    {
        public SvgSkewX();
        public SvgSkewX(double x);
        public override void FillTransform(string[] values);
        public override string GetTransform(IFormatProvider provider);
    }
}

