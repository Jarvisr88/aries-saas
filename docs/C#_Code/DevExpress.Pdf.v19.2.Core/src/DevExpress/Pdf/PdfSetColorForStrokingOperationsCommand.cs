namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorForStrokingOperationsCommand : PdfSetColorCommand
    {
        internal const string Name = "SC";

        internal PdfSetColorForStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorForStrokingOperations(base.Color);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("SC");
        }
    }
}

