namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPostScriptFindFontOperator : PdfPostScriptOperator
    {
        public const string Token = "findfont";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            PdfName name = stack.Pop(true) as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfType1FontClassicFontProgram program = interpreter.FontDirectory[name.Name] as PdfType1FontClassicFontProgram;
            if (program == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            stack.Push(program);
        }
    }
}

