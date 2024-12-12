namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorForNonStrokingOperationsCommand : PdfSetColorCommand
    {
        internal const string Name = "sc";

        internal PdfSetColorForNonStrokingOperationsCommand(PdfStack operands) : base(operands)
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetColorForNonStrokingOperations(base.Color);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("sc");
        }
    }
}

