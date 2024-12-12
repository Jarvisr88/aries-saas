namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("S")]
    public class SvgCommandShortCubicBezier : SvgCommandBase
    {
        public SvgCommandShortCubicBezier();
        public SvgCommandShortCubicBezier(SvgPoint point1, SvgPoint point2);

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }

        public override int InitialPointsCount { get; }
    }
}

