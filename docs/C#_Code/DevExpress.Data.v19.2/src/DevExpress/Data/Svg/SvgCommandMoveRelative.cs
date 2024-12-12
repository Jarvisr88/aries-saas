namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("m")]
    public class SvgCommandMoveRelative : SvgCommandMove
    {
        public override bool IsRelative { get; }
    }
}

