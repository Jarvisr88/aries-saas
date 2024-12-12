namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorAdvancedForNonStrokingOperationsCommand : PdfSetColorAdvancedCommand
    {
        internal const string Name = "scn";

        internal PdfSetColorAdvancedForNonStrokingOperationsCommand(PdfResources resources, PdfStack operands) : base(resources, operands)
        {
        }

        protected override void Execute(PdfCommandInterpreter interpreter, PdfColor color)
        {
            interpreter.SetColorForNonStrokingOperations(color);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("scn");
        }
    }
}

