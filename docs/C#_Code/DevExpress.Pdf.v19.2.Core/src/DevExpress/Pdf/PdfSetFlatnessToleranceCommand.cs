namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetFlatnessToleranceCommand : PdfCommand
    {
        internal const string Name = "i";
        private readonly double flatnessTolerance;

        internal PdfSetFlatnessToleranceCommand(PdfStack operands)
        {
            this.flatnessTolerance = operands.PopDouble();
            if (!ValidateFlatnessTolerance(this.flatnessTolerance))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public PdfSetFlatnessToleranceCommand(double flatnessTolerance)
        {
            if (!ValidateFlatnessTolerance(flatnessTolerance))
            {
                throw new ArgumentOutOfRangeException("flatnessTolerance", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectFlatnessTolerance));
            }
            this.flatnessTolerance = flatnessTolerance;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetFlatnessTolerance(this.flatnessTolerance);
        }

        private static bool ValidateFlatnessTolerance(double flatnessTolerance) => 
            (flatnessTolerance >= 0.0) && (flatnessTolerance <= 100.0);

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.flatnessTolerance);
            writer.WriteSpace();
            writer.WriteString("i");
        }

        public double FlatnessTolerance =>
            this.flatnessTolerance;
    }
}

