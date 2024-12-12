namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetRGBColorSpaceForNonStrokingOperationsCommand : PdfSetRGBColorSpaceCommand
    {
        internal const string Name = "rg";

        internal PdfSetRGBColorSpaceForNonStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetRGBColorSpaceForNonStrokingOperationsCommand(double r, double g, double b) : base(r, g, b)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForNonStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB));
            double[] components = new double[] { base.R, base.G, base.B };
            interpreter.SetColorForNonStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(base.R);
            writer.WriteSpace();
            writer.WriteDouble(base.G);
            writer.WriteSpace();
            writer.WriteDouble(base.B);
            writer.WriteSpace();
            writer.WriteString("rg");
        }
    }
}

