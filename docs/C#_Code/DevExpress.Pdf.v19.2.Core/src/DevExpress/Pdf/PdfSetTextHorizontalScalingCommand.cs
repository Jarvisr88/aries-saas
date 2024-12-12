namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetTextHorizontalScalingCommand : PdfCommand
    {
        internal const string Name = "Tz";
        private readonly double horizontalScaling;

        internal PdfSetTextHorizontalScalingCommand(PdfStack operands)
        {
            this.horizontalScaling = operands.PopDouble();
        }

        public PdfSetTextHorizontalScalingCommand(double horizontalScaling)
        {
            if (horizontalScaling <= 0.0)
            {
                throw new ArgumentOutOfRangeException("horizontalScaling", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectTextHorizontalScaling));
            }
            this.horizontalScaling = horizontalScaling;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextHorizontalScaling(this.horizontalScaling);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.horizontalScaling);
            writer.WriteSpace();
            writer.WriteString("Tz");
        }

        public double HorizontalScaling =>
            this.horizontalScaling;
    }
}

