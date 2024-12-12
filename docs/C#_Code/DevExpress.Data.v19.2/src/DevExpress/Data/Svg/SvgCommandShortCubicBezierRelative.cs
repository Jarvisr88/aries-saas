namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("s")]
    public class SvgCommandShortCubicBezierRelative : SvgCommandShortCubicBezier
    {
        public override bool IsRelative { get; }
    }
}

