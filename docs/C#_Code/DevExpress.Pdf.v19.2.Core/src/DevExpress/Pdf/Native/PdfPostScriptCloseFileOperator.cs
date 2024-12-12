namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptCloseFileOperator : PdfPostScriptOperator
    {
        public const string Token = "closefile";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfPostScriptFileParser objA = interpreter.Stack.Pop(true) as PdfPostScriptFileParser;
            if ((objA == null) || !ReferenceEquals(objA, interpreter.Parser))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            interpreter.CloseFile();
        }
    }
}

