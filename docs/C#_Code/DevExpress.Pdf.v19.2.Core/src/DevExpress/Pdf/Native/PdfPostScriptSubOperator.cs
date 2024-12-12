namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptSubOperator : PdfPostScriptOperator
    {
        public const string Token = "sub";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            object obj3 = stack.Pop(true);
            if ((obj3 is int) && (obj2 is int))
            {
                stack.Push(((int) obj3) - ((int) obj2));
            }
            else
            {
                stack.Push(PdfDocumentReader.ConvertToDouble(obj3) - PdfDocumentReader.ConvertToDouble(obj2));
            }
        }
    }
}

