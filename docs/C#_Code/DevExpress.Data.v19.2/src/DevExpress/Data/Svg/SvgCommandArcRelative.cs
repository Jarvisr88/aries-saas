namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("a")]
    public class SvgCommandArcRelative : SvgCommandArc
    {
        public override bool IsRelative { get; }
    }
}

