namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetCMYKColorSpaceForNonStrokingOperationsCommand : PdfSetCMYKColorSpaceCommand
    {
        internal const string Name = "k";

        internal PdfSetCMYKColorSpaceForNonStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetCMYKColorSpaceForNonStrokingOperationsCommand(double c, double m, double y, double k) : base(c, m, y, k)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForNonStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK));
            double[] components = new double[] { base.C, base.M, base.Y, base.K };
            interpreter.SetColorForNonStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("k");
        }
    }
}

