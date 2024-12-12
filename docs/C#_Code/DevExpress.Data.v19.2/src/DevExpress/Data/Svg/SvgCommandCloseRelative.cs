namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("z")]
    public class SvgCommandCloseRelative : SvgCommandClose
    {
        public override bool IsRelative { get; }
    }
}

