namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("M")]
    public class SvgCommandMove : SvgCommandBase
    {
        public SvgCommandMove();
        public SvgCommandMove(SvgPoint point);

        public override SvgCommandAction CommandAction { get; }

        public override char ExportCommandName { get; }
    }
}

