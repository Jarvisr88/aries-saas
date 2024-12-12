namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetRGBColorSpaceForStrokingOperationsCommand : PdfSetRGBColorSpaceCommand
    {
        internal const string Name = "RG";

        internal PdfSetRGBColorSpaceForStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetRGBColorSpaceForStrokingOperationsCommand(double r, double g, double b) : base(r, g, b)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB));
            double[] components = new double[] { base.R, base.G, base.B };
            interpreter.SetColorForStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("RG");
        }
    }
}

