namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptMulOperator : PdfPostScriptOperator
    {
        public const string Token = "mul";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            object obj3 = stack.Pop(true);
            if ((obj2 is int) && (obj3 is int))
            {
                stack.Push(((int) obj2) * ((int) obj3));
            }
            else
            {
                stack.Push(PdfDocumentReader.ConvertToDouble(obj2) * PdfDocumentReader.ConvertToDouble(obj3));
            }
        }
    }
}

