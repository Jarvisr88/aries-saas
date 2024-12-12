namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptStringOperator : PdfPostScriptOperator
    {
        public const string Token = "string";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = (int) obj2;
            if (num < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            stack.Push(new byte[num]);
        }
    }
}

