namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetLineWidthCommand : PdfCommand
    {
        internal const string Name = "w";
        private readonly double lineWidth;

        internal PdfSetLineWidthCommand(PdfStack operands)
        {
            this.lineWidth = Math.Max(0.0, operands.PopDouble());
        }

        public PdfSetLineWidthCommand(double lineWidth)
        {
            if (lineWidth < 0.0)
            {
                throw new ArgumentOutOfRangeException("lineWidth", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectLineWidth));
            }
            this.lineWidth = lineWidth;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetLineWidth(this.LineWidth);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.lineWidth);
            writer.WriteSpace();
            writer.WriteString("w");
        }

        public double LineWidth =>
            this.lineWidth;
    }
}

