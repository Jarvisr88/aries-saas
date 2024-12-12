namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("t")]
    public class SvgCommandShortQuadraticBezierRelative : SvgCommandShortQuadraticBezier
    {
        public override bool IsRelative { get; }
    }
}

