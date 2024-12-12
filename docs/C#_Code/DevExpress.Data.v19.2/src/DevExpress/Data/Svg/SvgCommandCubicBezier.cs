namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("C")]
    public class SvgCommandCubicBezier : SvgCommandBase
    {
        public SvgCommandCubicBezier();
        public SvgCommandCubicBezier(SvgPoint point1, SvgPoint point2, SvgPoint point3);

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }

        public override int InitialPointsCount { get; }
    }
}

