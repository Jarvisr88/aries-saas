namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("c")]
    public class SvgCommandCubicBezierRelative : SvgCommandCubicBezier
    {
        public override bool IsRelative { get; }
    }
}

