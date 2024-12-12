namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("V")]
    public class SvgCommandVertical : SvgCommandBase
    {
        protected SvgPointCollection Parse(string[] commandsElementsList, int i, SvgPoint point);
        protected override SvgPointCollection ParsePoints(string[] commandsElementsList, int i, SvgPoint prevPoint);

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }
    }
}

