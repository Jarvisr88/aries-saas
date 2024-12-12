namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("tspan")]
    public class SvgTextSpan : SvgText
    {
        public SvgTextSpan();
        public SvgTextSpan(SvgPoint location, SvgSize offset, SvgTextAnchor textAnchor, string text, SvgRect bounds);
    }
}

