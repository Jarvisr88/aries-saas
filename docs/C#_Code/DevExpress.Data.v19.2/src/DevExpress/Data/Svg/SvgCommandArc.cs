namespace DevExpress.Data.Svg
{
    using System;
    using System.Text;

    [FormatElement("A")]
    public class SvgCommandArc : SvgCommandBase
    {
        private SvgPoint radius;
        private double rotation;
        private bool isLargeArc;
        private bool isSwap;

        public SvgCommandArc();
        public SvgCommandArc(double rx, double ry, double rotationAngle, bool isLargeArc, bool isSwap, SvgPoint point);
        protected override void ExportCommandParameters(StringBuilder stringBuilder);
        protected override SvgPointCollection ParsePoints(string[] commandsElementsList, int index, SvgPoint prevPoint);

        public SvgPoint Radius { get; protected internal set; }

        public double Rotation { get; protected internal set; }

        public bool IsLargeArc { get; protected internal set; }

        public bool IsSwap { get; protected internal set; }

        public override char ExportCommandName { get; }

        public override int ParametersCount { get; }

        public override int InitialPointsCount { get; }
    }
}

