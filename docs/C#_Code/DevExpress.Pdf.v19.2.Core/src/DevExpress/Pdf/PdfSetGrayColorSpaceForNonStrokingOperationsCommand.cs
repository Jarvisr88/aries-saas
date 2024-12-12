namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetGrayColorSpaceForNonStrokingOperationsCommand : PdfSetGrayColorSpaceCommand
    {
        internal const string Name = "g";

        internal PdfSetGrayColorSpaceForNonStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetGrayColorSpaceForNonStrokingOperationsCommand(double gray) : base(gray)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForNonStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray));
            double[] components = new double[] { base.Gray };
            interpreter.SetColorForNonStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(base.Gray);
            writer.WriteSpace();
            writer.WriteString("g");
        }
    }
}

