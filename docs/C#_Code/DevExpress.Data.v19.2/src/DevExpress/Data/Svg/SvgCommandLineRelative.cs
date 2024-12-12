namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("l")]
    public class SvgCommandLineRelative : SvgCommandLine
    {
        public override bool IsRelative { get; }
    }
}

