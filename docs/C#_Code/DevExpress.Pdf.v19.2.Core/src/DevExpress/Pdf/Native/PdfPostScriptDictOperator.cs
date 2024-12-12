namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptDictOperator : PdfPostScriptOperator
    {
        public const string Token = "dict";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int capacity = (int) obj2;
            if (capacity < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            stack.Push(new PdfPostScriptDictionary(capacity));
        }
    }
}

