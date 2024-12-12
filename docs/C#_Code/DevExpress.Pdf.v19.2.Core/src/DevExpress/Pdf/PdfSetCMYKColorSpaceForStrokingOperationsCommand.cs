namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetCMYKColorSpaceForStrokingOperationsCommand : PdfSetCMYKColorSpaceCommand
    {
        internal const string Name = "K";

        internal PdfSetCMYKColorSpaceForStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        public PdfSetCMYKColorSpaceForStrokingOperationsCommand(double c, double m, double y, double k) : base(c, m, y, k)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorSpaceForStrokingOperations(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK));
            double[] components = new double[] { base.C, base.M, base.Y, base.K };
            interpreter.SetColorForStrokingOperations(new PdfColor(components));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("K");
        }
    }
}

