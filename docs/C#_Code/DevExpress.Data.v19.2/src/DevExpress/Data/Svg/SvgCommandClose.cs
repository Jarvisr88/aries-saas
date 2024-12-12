namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("Z")]
    public class SvgCommandClose : SvgCommandBase
    {
        private SvgPoint closedPoint;

        protected override SvgPointCollection ParsePoints(string[] commandsElementsList, int index, SvgPoint startPoint);

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }

        public override int InitialPointsCount { get; }

        public override SvgCommandAction CommandAction { get; }

        public override SvgPoint GeneralPoint { get; }
    }
}

