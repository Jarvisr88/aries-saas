namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("v")]
    public class SvgCommandVerticalRelative : SvgCommandVertical
    {
        protected override SvgPointCollection ParsePoints(string[] commandsElementsList, int i, SvgPoint prevPoint);

        public override bool IsRelative { get; }
    }
}

