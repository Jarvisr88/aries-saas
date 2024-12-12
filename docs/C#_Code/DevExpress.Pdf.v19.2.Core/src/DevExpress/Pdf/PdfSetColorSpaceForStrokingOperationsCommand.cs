namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorSpaceForStrokingOperationsCommand : PdfSetColorSpaceCommand
    {
        internal const string Name = "CS";

        public PdfSetColorSpaceForStrokingOperationsCommand(PdfColorSpace colorSpace) : base(colorSpace)
        {
        }

        internal PdfSetColorSpaceForStrokingOperationsCommand(PdfResources resources, PdfStack operands) : base(resources, operands)
        {
        }

        protected override void Execute(PdfCommandInterpreter interpreter, PdfColorSpace colorSpace)
        {
            interpreter.SetColorSpaceForStrokingOperations(colorSpace);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("CS");
        }
    }
}

