namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("Q")]
    public class SvgCommandQuadraticBezier : SvgCommandBase
    {
        public SvgCommandQuadraticBezier();
        public SvgCommandQuadraticBezier(SvgPoint point1, SvgPoint point2);

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }

        public override int InitialPointsCount { get; }
    }
}

