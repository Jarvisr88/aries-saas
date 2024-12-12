namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetColorAdvancedForStrokingOperationsCommand : PdfSetColorAdvancedCommand
    {
        internal const string Name = "SCN";

        internal PdfSetColorAdvancedForStrokingOperationsCommand(PdfResources resources, PdfStack operands) : base(resources, operands)
        {
        }

        protected override void Execute(PdfCommandInterpreter interpreter, PdfColor color)
        {
            interpreter.SetColorForStrokingOperations(color);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            writer.WriteString("SCN");
        }
    }
}

