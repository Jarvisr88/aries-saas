namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("skewY")]
    public class SvgSkewY : SvgSkew
    {
        public SvgSkewY();
        public SvgSkewY(double y);
        public override void FillTransform(string[] values);
        public override string GetTransform(IFormatProvider provider);
    }
}

