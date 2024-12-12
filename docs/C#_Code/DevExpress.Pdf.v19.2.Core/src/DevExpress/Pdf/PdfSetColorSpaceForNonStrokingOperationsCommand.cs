namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorSpaceForNonStrokingOperationsCommand : PdfSetColorSpaceCommand
    {
        internal const string Name = "cs";

        public PdfSetColorSpaceForNonStrokingOperationsCommand(PdfColorSpace colorSpace) : base(colorSpace)
        {
        }

        internal PdfSetColorSpaceForNonStrokingOperationsCommand(PdfResources resources, PdfStack operands) : base(resources, operands)
        {
        }

        protected override void Execute(PdfCommandInterpreter interpreter, PdfColorSpace colorSpace)
        {
            interpreter.SetColorSpaceForNonStrokingOperations(colorSpace);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("cs");
        }
    }
}

