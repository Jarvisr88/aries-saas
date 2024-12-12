namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptAbsOperator : PdfPostScriptOperator
    {
        public const string Token = "abs";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            if (!(obj2 is int))
            {
                stack.Push(Math.Abs(PdfDocumentReader.ConvertToDouble(obj2)));
            }
            else
            {
                int num = (int) obj2;
                stack.Push((num == -2147483648) ? -((double) num) : Math.Abs(num));
            }
        }
    }
}

