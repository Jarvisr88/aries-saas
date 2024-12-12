namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetMiterLimitCommand : PdfCommand
    {
        internal const string Name = "M";
        private readonly double miterLimit;

        internal PdfSetMiterLimitCommand(PdfStack operands)
        {
            this.miterLimit = operands.PopDouble();
            if (this.miterLimit <= 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public PdfSetMiterLimitCommand(double miterLimit)
        {
            if (miterLimit <= 0.0)
            {
                throw new ArgumentOutOfRangeException("miterLimit", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectMiterLimit));
            }
            this.miterLimit = miterLimit;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetMiterLimit(this.miterLimit);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.miterLimit);
            writer.WriteSpace();
            writer.WriteString("M");
        }

        public double MiterLimit =>
            this.miterLimit;
    }
}

