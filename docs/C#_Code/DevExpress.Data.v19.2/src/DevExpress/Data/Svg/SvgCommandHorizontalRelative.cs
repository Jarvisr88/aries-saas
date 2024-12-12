namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("h")]
    public class SvgCommandHorizontalRelative : SvgCommandHorizontal
    {
        protected override SvgPointCollection ParsePoints(string[] commandsElementsList, int i, SvgPoint prevPoint);

        public override bool IsRelative { get; }
    }
}

