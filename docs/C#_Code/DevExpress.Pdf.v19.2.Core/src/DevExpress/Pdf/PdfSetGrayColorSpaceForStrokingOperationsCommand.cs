namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetGrayColorSpaceForStrokingOperationsCommand : PdfSetGrayColorSpaceCommand
    {
        internal const string Name = "G";

        internal PdfSetGrayColorSpaceForStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetGrayColorSpaceForStrokingOperationsCommand(double gray) : base(gray)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray));
            double[] components = new double[] { base.Gray };
            interpreter.SetColorForStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(base.Gray);
            writer.WriteSpace();
            writer.WriteString("G");
        }
    }
}

