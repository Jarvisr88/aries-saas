namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptArrayOperator : PdfPostScriptOperator
    {
        public const string Token = "array";

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
            stack.Push(new object[num]);
        }
    }
}

