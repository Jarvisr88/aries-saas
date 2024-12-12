namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("T")]
    public class SvgCommandShortQuadraticBezier : SvgCommandBase
    {
        public SvgCommandShortQuadraticBezier();
        public SvgCommandShortQuadraticBezier(SvgPoint point);

        public override char ExportCommandName { get; }
    }
}

