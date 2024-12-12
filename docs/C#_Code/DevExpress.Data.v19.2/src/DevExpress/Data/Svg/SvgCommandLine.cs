namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("L")]
    public class SvgCommandLine : SvgCommandBase
    {
        public SvgCommandLine();
        public SvgCommandLine(SvgPoint point);

        public override char ExportCommandName { get; }
    }
}

