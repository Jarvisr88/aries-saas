namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptIndexOperator : PdfPostScriptOperator
    {
        public const string Token = "index";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            stack.Push(stack.PeekAtIndex((int) obj2));
        }
    }
}

