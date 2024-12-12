namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("q")]
    public class SvgCommandQuadraticBezierRelative : SvgCommandQuadraticBezier
    {
        public override bool IsRelative { get; }
    }
}

